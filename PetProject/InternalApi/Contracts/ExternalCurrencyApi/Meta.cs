using System.Text.Json.Serialization;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts.ExternalCurrencyApi
{
    public class Meta
    {
        [JsonPropertyNameAttribute("last_updated_at")]
        public DateTime LastUpdatedAt { get; init; }
    }
}
