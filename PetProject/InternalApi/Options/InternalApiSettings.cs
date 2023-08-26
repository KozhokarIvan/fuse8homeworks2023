namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Options
{
    public class InternalApiSettings
    {
        public const string CurrencyApiName = "InternalApi";
        public int CacheLifeTimeHours { get; set; }
        public string Uri { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string BaseCurrency { get; set; } = string.Empty;
    }
}