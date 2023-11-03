using Fuse8_ByteMinds.SummerSchool.PublicApi.Data.Entities;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Services
{
    public interface IFavoriteExchangesService
    {
        /// <summary>
        /// Получает избранную пару валют по названию
        /// </summary>
        /// <param name="favoriteExchangeName">Название избранной пары валют</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Избранная пара валют с названием <paramref name="favoriteExchangeName"/>, если такой не существует, то null</returns>
        Task<FavoriteExchange?> GetFavoriteExchangeByName(string favoriteExchangeName, CancellationToken cancellationToken);

        /// <summary>
        /// Получает все избранные пары валют и их названия
        /// </summary>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Массив с избранными парами валют и их названиями</returns>
        Task<FavoriteExchange[]> GetFavoriteExchanges(CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет избранную пару валют
        /// </summary>
        /// <param name="favoriteExchangeName">Название для избранной пары валют</param>
        /// <param name="currency">Код валюты</param>
        /// <param name="baseCurrency">Код базовой валюты</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Была ли добавлена избранная пара валют</returns>
        Task<bool> AddExchangeToFavorites(string favoriteExchangeName, string currency, string baseCurrency, CancellationToken cancellationToken);

        /// <summary>
        ///  Редактирует избранную пару валют
        /// </summary>
        /// <param name="favoriteExchangeName">Название избранной пары валют</param>
        /// <param name="currency">Код валюты</param>
        /// <param name="baseCurrency">Код базовой валюты</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Была ли отредактирована избранная пара валют, null если пары с таким названием не существует</returns>
        Task<bool?> EditFavoriteExchange(string favoriteExchangeName, string currency, string baseCurrency, CancellationToken cancellationToken);

        /// <summary>
        /// Удаляет избранную пару валют по названию
        /// </summary>
        /// <param name="favoriteExchangeName">Название избранной пары валют</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Была ли удалена пара валют</returns>
        Task<bool> DeleteFavoriteExchangeByName(string favoriteExchangeName, CancellationToken cancellationToken);
    }
}
