FROM mcr.microsoft.com/dotnet/sdk:7.0 AS prepare_restore
ENV PATH = "${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global --no-cache dotnet-subset --version 0.3.2
WORKDIR /src
COPY . .
RUN dotnet subset restore PublicApi/PublicApi.csproj --root-directory /src --output restore_subset/

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src
COPY --from=prepare_restore /src/restore_subset .
RUN dotnet restore PublicApi/PublicApi.csproj

COPY . .
RUN dotnet publish PublicApi/PublicApi.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5166
ENTRYPOINT ["dotnet", "PublicApi.dll"]