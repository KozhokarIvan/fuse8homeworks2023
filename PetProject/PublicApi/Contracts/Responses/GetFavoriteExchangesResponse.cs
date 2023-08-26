namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses
{
    public class GetFavoriteExchangesResponse
    {
        /// <summary>
        /// Список избранных пар валют и их названий
        /// </summary>
        public FavoriteExchangeEntry[] Favorites { get; init; } = Array.Empty<FavoriteExchangeEntry>();
    }

    /// <summary>
    /// Параметры избранной пары валют
    /// </summary>
    /// <param name="FavoriteName">Название избранной пары валют</param>
    /// <param name="CurrencyCode">Код валюты</param>
    /// <param name="BaseCurrencyCode">Код базовой валюты</param>
    public record FavoriteExchangeEntry(string FavoriteName, string CurrencyCode, string BaseCurrencyCode);
}
