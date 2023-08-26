namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses
{
    public class GetFavoriteExchangeByNameResponse
    {
        /// <summary>
        /// Название избранной пары валют
        /// </summary>
        /// <example>RubToKzt</example>
        public string FavoriteName { get; set; } = string.Empty;

        /// <summary>
        /// Код валюты
        /// </summary>
        /// <example>Rub</example>
        public string CurrencyCode { get; set; } = string.Empty;

        /// <summary>
        /// Код базовой валюты
        /// </summary>
        /// <example>Kzt</example>
        public string BaseCurrencyCode { get; set; } = string.Empty;
    }
}
