namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Requests
{
    /// <summary>
    /// Модель запроса для установки новой валюты по умолчанию
    /// </summary>
    public class SetDefaultCurrencyRequest
    {
        /// <summary>
        /// Валюта по умолчанию
        /// </summary>
        /// <example>RUB</example>
        public CurrencyCode CurrencyCode { get; init; }
    }
}
