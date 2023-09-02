namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses
{
    /// <summary>
    /// Модель ответа для получения курса валют в нужную дату
    /// </summary>
    public class GetCurrencyOnDateResponse
    {
        /// <summary>
        /// День для которого был получен курс
        /// </summary>
        /// <example>2023-03-03</example>
        public DateOnly Date { get; init; }

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
