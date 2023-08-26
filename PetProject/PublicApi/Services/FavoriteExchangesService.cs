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
        public async Task<FavoriteExchange?> GetFavoriteExchangeByName(string favoriteExchangeName, CancellationToken cancellationToken)
            => await _favoriteExchangesRepository.GetFavoriteExchangeByName(favoriteExchangeName, cancellationToken);
        public async Task<FavoriteExchange[]> GetFavoriteExchanges(CancellationToken cancellationToken)
            => await _favoriteExchangesRepository.GetFavoriteExchanges(cancellationToken);
        public async Task<bool> AddExchangeToFavorites(string favoriteExchangeName, string currency, string baseCurrency, CancellationToken cancellationToken)
            => await _favoriteExchangesRepository.AddExchangeToFavorites(favoriteExchangeName, currency, baseCurrency, cancellationToken);
        public async Task<bool?> EditFavoriteExchange(string favoriteExchangeName, string currency, string baseCurrency, CancellationToken cancellationToken)
            => await _favoriteExchangesRepository.EditFavoriteExchange(favoriteExchangeName, currency, baseCurrency, cancellationToken);
        public async Task<bool> DeleteFavoriteExchangeByName(string favoriteExchangeName, CancellationToken cancellationToken)
            => await _favoriteExchangesRepository.DeleteFavoriteExchangeByName(favoriteExchangeName, cancellationToken);
    }
}
