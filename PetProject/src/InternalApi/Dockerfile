FROM mcr.microsoft.com/dotnet/sdk:7.0 AS prepare_restore
ENV PATH = "${PATH}:/root/.dotnet/tools"
RUN dotnet tool install --global --no-cache dotnet-subset --version 0.3.2
WORKDIR /src
COPY . .
RUN dotnet subset restore InternalApi/InternalApi.csproj --root-directory /src --output restore_subset/

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src
COPY --from=prepare_restore /src/restore_subset .
RUN dotnet restore InternalApi/InternalApi.csproj

COPY . .
RUN dotnet publish InternalApi/InternalApi.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "InternalApi.dll"]