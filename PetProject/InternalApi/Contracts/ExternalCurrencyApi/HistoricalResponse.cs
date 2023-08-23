namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts.ExternalCurrencyApi
{
    public class HistoricalResponse
    {
        public Meta Meta { get; init; }
        public Dictionary<string, Currency> Data { get; init; }
    }
}
