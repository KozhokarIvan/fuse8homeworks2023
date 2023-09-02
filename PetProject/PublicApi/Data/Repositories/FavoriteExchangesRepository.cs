using Fuse8_ByteMinds.SummerSchool.PublicApi.Data.Entities;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Data.Repositories
{
    public class FavoriteExchangesRepository : IFavoriteExchangesRepository
    {
        private readonly PublicApiDbContext _context;
        public FavoriteExchangesRepository(PublicApiDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddExchangeToFavorites(string favoriteExchangeName, string currency, string baseCurrency, CancellationToken cancellationToken)
        {
            bool recordIsNotUnique = await _context.FavoriteExchanges.AnyAsync(e =>
                e.Name == favoriteExchangeName || EF.Functions.ILike(e.Currency, currency)
                && EF.Functions.ILike(e.BaseCurrency, baseCurrency),
                cancellationToken);
            if (recordIsNotUnique)
                return false;
            _context.FavoriteExchanges.Add(new FavoriteExchange
            {
                Name = favoriteExchangeName,
                Currency = currency.ToLower(),
                BaseCurrency = baseCurrency.ToLower()
            });
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteFavoriteExchangeByName(string favoriteExchangeName, CancellationToken cancellationToken)
        {
            bool doesRecordExist = await _context.FavoriteExchanges.AnyAsync(e
                => e.Name == favoriteExchangeName,
                cancellationToken);
            if (!doesRecordExist)
                return false;
            var favoriteExchange = await _context.FavoriteExchanges.FirstAsync(e
                => e.Name == favoriteExchangeName,
                cancellationToken);
            _context.FavoriteExchanges.Remove(favoriteExchange);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool?> EditFavoriteExchange(string favoriteExchangeName, string currency, string baseCurrency, CancellationToken cancellationToken)
        {
            var favoriteExchange = await _context.FavoriteExchanges.FirstOrDefaultAsync(e
                => e.Name == favoriteExchangeName,
                cancellationToken);
            if (favoriteExchange is null)
                return null;
            bool recordIsNotUnique = await _context.FavoriteExchanges.AnyAsync(e =>
                EF.Functions.ILike(e.Currency, currency) && EF.Functions.ILike(e.BaseCurrency, baseCurrency),
                cancellationToken);
            if (recordIsNotUnique)
                return false;
            favoriteExchange.Currency = currency.ToLower();
            favoriteExchange.BaseCurrency = baseCurrency.ToLower();
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<FavoriteExchange?> GetFavoriteExchangeByName(string favoriteExchangeName, CancellationToken cancellationToken)
        {
            var favoriteExchange = await _context.FavoriteExchanges
                .AsNoTracking()
                .FirstOrDefaultAsync(e =>
                    e.Name == favoriteExchangeName,
                    cancellationToken);
            return favoriteExchange;
        }

        public async Task<FavoriteExchange[]> GetFavoriteExchanges(CancellationToken cancellationToken)
        {
            var favoriteExchanges = await _context.FavoriteExchanges
                .AsNoTracking()
                .ToArrayAsync(cancellationToken);
            return favoriteExchanges;
        }
    }
}
