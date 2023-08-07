namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses
{
    public class CurrencyResponse
    {
        /// <summary>
        /// Код валюты
        /// </summary>
        /// <example>RUB</example>
        public string Code { get; init; } = string.Empty;
        /// <summary>
        /// Курс валюты относительно базовой валюты
        /// </summary>
        /// <example>70.23</example>
        public decimal Value { get; init; }
    }
}
