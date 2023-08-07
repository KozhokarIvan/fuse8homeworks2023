namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Options
{
    public class CurrencyApiSettings
    {
        public const string CurrencyApiName = "CurrencyApi";
        public string Uri { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string BaseCurrency { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public int DecimalPlaces { get; set; }
    }
}