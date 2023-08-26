namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses
{
    public class GetFavoriteExchangeRateResponse
    {

        /// <summary>
        /// Курс валюты
        /// </summary>
        /// <example>95.9903962843</example>
        public decimal ExchangeRate { get; init; }
    }
}
