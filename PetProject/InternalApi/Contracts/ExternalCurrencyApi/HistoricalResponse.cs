namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts.ExternalCurrencyApi
{
    public class HistoricalResponse
    {
        public Meta Meta { get; init; } = null!;
        public Dictionary<string, Currency> Data { get; init; } = null!;
    }
}
