namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Options
{
    public class CurrencyApiSettings
    {
        public const string CurrencyApiName = "InternalApi";
        public string RelativeCachePath { get; set; } = string.Empty;
        public string CacheExtension { get; set; } = string.Empty;
        public int CacheLifeTimeHours { get; set; }
        public string DateFormat { get; set; } = string.Empty;
        public string DateTimeFormat { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string BaseCurrency { get; set; } = string.Empty;
    }
}