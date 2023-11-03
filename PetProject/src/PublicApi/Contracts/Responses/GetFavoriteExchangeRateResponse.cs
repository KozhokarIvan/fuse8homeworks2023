namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses
{
    /// <summary>
    /// Модель ответа для получения курса по названию избранной пары валют
    /// </summary>
    public class GetFavoriteExchangeRateResponse
    {

        /// <summary>
        /// Курс валюты
        /// </summary>
        /// <example>95.9903962843</example>
        public decimal ExchangeRate { get; init; }
    }
}
