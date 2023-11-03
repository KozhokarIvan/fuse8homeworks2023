namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts.Requests
{
    /// <summary>
    /// Модель запроса для установки новой базовой валюты
    /// </summary>
    public class SetNewBaseCurrencyRequest
    {
        /// <summary>
        /// Код валюты которая станет базовой
        /// </summary>
        /// <example>jpy</example>
        public CurrencyCode NewBaseCurrency { get; set; }
    }
}
