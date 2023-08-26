using Fuse8_ByteMinds.SummerSchool.InternalApi.Data;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly InternalApiDbContext _context;
        public CurrencyRepository(InternalApiDbContext context)
        {
            _context = context;
        }

        public async Task<Currency?> GetLatestCurrencyInfo(string currencyCode, CancellationToken cancellationToken)
        {
            var currency = await _context.Currencies
                .AsNoTracking()
                .Where(c => EF.Functions.ILike(c.CurrencyCode.Name, currencyCode))
                .OrderByDescending(c => c.DateTime)
                .Include(c => c.BaseCurrencyCode)
                .Include(c => c.CurrencyCode)
                .FirstOrDefaultAsync(cancellationToken);
            return currency;
        }

        public async Task<Currency?> GetCurrencyInfoOnDate(string currencyCode, DateOnly date, CancellationToken cancellationToken)
        {
            var dateTime = DateTime.SpecifyKind(new DateTime(date.Year, date.Month, date.Day), DateTimeKind.Utc);
            var currency = await _context.Currencies
                .AsNoTracking()
                .Where(c
                    => c.DateTime.Date == dateTime.Date
                    && EF.Functions.ILike(c.CurrencyCode.Name, currencyCode))
                .OrderByDescending(c => c.DateTime)
                .Include(c => c.BaseCurrencyCode)
                .Include(c => c.CurrencyCode)
                .FirstOrDefaultAsync(cancellationToken);
            return currency;
        }

        public async Task AddCurrentCurrencies(Dictionary<string, decimal> currencies, string baseCurrency, CancellationToken cancellationToken)
        {
            var currentDateTime = DateTime.UtcNow;
            var currencyCodes = await _context.CurrencyCodes.ToArrayAsync(cancellationToken);
            foreach (var entry in currencies)
            {
                _context.Currencies.Add(new Currency
                {
                    CurrencyCodeId = currencyCodes.First(c
                        => c.Name.Equals(entry.Key, StringComparison.InvariantCultureIgnoreCase)).Id,
                    BaseCurrencyCodeId = currencyCodes.First(c
                        => c.Name.Equals(baseCurrency, StringComparison.InvariantCultureIgnoreCase)).Id,
                    Value = entry.Value,
                    DateTime = currentDateTime,
                });
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddCurrenciesOnDate(Dictionary<string, decimal> currencies, string baseCurrency, DateOnly date, CancellationToken cancellationToken)
        {
            var dateTime = DateTime.SpecifyKind(new DateTime(date.Year, date.Month, date.Day), DateTimeKind.Utc);
            var currencyCodes = await _context.CurrencyCodes.ToArrayAsync(cancellationToken);
            foreach (var entry in currencies)
            {
                _context.Currencies.Add(new Currency
                {
                    CurrencyCodeId = currencyCodes.First(c
                        => c.Name.Equals(entry.Key, StringComparison.InvariantCultureIgnoreCase)).Id,
                    BaseCurrencyCodeId = currencyCodes.First(c
                        => c.Name.Equals(baseCurrency, StringComparison.InvariantCultureIgnoreCase)).Id,
                    Value = entry.Value,
                    DateTime = dateTime,
                });
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
