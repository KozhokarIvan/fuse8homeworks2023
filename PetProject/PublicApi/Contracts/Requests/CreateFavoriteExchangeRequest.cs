namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Requests
{
    public class CreateFavoriteExchangeRequest
    {
        /// <summary>
        /// Название избранной пары валют для обмена
        /// </summary>
        /// <example>KztToRub</example>
        public string FavoriteName { get; init; } = string.Empty;

        /// <summary>
        /// Код валюты
        /// </summary>
        /// <example>Kzt</example>
        public string Currency { get; init; } = string.Empty;

        /// <summary>
        /// Код базовой валюты
        /// </summary>
        /// <example>Rub</example>
        public string BaseCurrency { get; init; } = string.Empty;
    }
}
