namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Options
{
    public class PublicApiSettings
    {
        public const string CurrencyApiName = "PublicApi";
        public string Uri { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string CurrencySettingName { get; set; } = string.Empty;
        public string DecimalPlacesSettingName { get; set; } = string.Empty;
    }
}