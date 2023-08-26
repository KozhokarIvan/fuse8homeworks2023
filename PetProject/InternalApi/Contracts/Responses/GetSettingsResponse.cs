namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts.Responses
{
    public class GetSettingsResponse
    {
        /// <summary>
        /// Валюта для которой нужно получить курс относительно базовой
        /// </summary>
        /// <example>RUB</example>
        public string BaseCurrency { get; init; } = string.Empty;

        /// <summary>
        /// Доступны ли еще запросы
        /// </summary>
        /// <example>true</example>
        public bool RequestsAvailable { get; init; }
    }
}
