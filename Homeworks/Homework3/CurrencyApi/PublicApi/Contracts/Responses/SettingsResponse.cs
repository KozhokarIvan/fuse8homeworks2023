namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses
{
    public class SettingsResponse
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
        /// Максимальное количество запросов за месяц
        /// </summary>
        /// <example>300</example>
        public int RequestLimit { get; init; }
        /// <summary>
        /// Использованное количество запросов в этом месяце
        /// </summary>
        /// <example>111</example>
        public int RequestCount { get; init; }
        /// <summary>
        /// Точность округления курса
        /// </summary>
        /// <example>2</example>
        public int CurrencyRoundCount { get; init; }
    }
}
