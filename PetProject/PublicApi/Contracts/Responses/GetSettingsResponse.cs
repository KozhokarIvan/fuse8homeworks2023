namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses
{
    /// <summary>
    /// Модель ответа для получения настроек API
    /// </summary>
    public class GetSettingsResponse
    {
        /// <summary>
        /// Валюта для которой нужно получить курс относительно базовой
        /// </summary>
        /// <example>RUB</example>
        public string DefaultCurrency { get; init; } = string.Empty;
        /// <summary>
        /// Базовая валюта
        /// </summary>
        /// <example>USD</example>
        public string BaseCurrency { get; init; } = string.Empty;
        /// <summary>
        /// Есть ли еще доступные запросы
        /// </summary>
        /// <example>true</example>
        public bool NewRequestsAvailable { get; init; }
        /// <summary>
        /// Точность округления курса
        /// </summary>
        /// <example>2</example>
        public int CurrencyRoundCount { get; init; }
    }
}
