using Fuse8_ByteMinds.SummerSchool.PublicApi.Data.Entities;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Repositories;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Services;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Services
{
    public class FavoriteExchangesService : IFavoriteExchangesService
    {
        private readonly IFavoriteExchangesRepository _favoriteExchangesRepository;
        public FavoriteExchangesService(IFavoriteExchangesRepository favoriteExchangesRepository)
        {
            _favoriteExchangesRepository = favoriteExchangesRepository;
        }
        public Task<FavoriteExchange?> GetFavoriteExchangeByName(string favoriteExchangeName, CancellationToken cancellationToken)
            => _favoriteExchangesRepository.GetFavoriteExchangeByName(favoriteExchangeName, cancellationToken);
        public Task<FavoriteExchange[]> GetFavoriteExchanges(CancellationToken cancellationToken)
            => _favoriteExchangesRepository.GetFavoriteExchanges(cancellationToken);
        public Task<bool> AddExchangeToFavorites(string favoriteExchangeName, string currency, string baseCurrency, CancellationToken cancellationToken)
            => _favoriteExchangesRepository.AddExchangeToFavorites(favoriteExchangeName, currency, baseCurrency, cancellationToken);
        public Task<bool?> EditFavoriteExchange(string favoriteExchangeName, string currency, string baseCurrency, CancellationToken cancellationToken)
            => _favoriteExchangesRepository.EditFavoriteExchange(favoriteExchangeName, currency, baseCurrency, cancellationToken);
        public Task<bool> DeleteFavoriteExchangeByName(string favoriteExchangeName, CancellationToken cancellationToken)
            => _favoriteExchangesRepository.DeleteFavoriteExchangeByName(favoriteExchangeName, cancellationToken);
    }
}
