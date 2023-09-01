﻿using Fuse8_ByteMinds.SummerSchool.InternalApi.Data;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Repositories;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static Grpc.Core.Metadata;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly InternalApiDbContext _context;
        private readonly IOptionsSnapshot<InternalApiSettings> _options;
        public CurrencyRepository(InternalApiDbContext context, IOptionsSnapshot<InternalApiSettings> options)
        {
            _context = context;
            _options = options;
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
        public async Task UpdateCache(string newBaseCurrency, CancellationToken cancellationToken)
        {
            var cacheValues = await _context.Currencies
                .OrderByDescending(c => c.DateTime)
                .ToArrayAsync(cancellationToken);
            var baseCurrencyCode = await _context.CurrencyCodes.FirstAsync(c
                        => c.Name.Equals(newBaseCurrency, StringComparison.InvariantCultureIgnoreCase), cancellationToken);
            var newBaseCurrencyValue = await GetLatestCurrencyInfo(newBaseCurrency, cancellationToken);
            foreach (var currency in cacheValues)
            {
                currency.BaseCurrencyCodeId = baseCurrencyCode.Id;
                currency.Value /= newBaseCurrencyValue!.Value;
            }
            var baseCurrencySetting = await _context.Settings
                .FirstAsync(s => s.Name == _options.Value.BaseCurrencySettingName, cancellationToken);
            baseCurrencySetting.Value = newBaseCurrency;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<string> GetBaseCurrency(CancellationToken cancellationToken)
        {
            var currencyCode = (await _context.Settings
                .AsNoTracking()
                .FirstAsync(s
                => s.Name.Equals(_options.Value.BaseCurrencySettingName, StringComparison.InvariantCultureIgnoreCase), cancellationToken)).Value;
            return currencyCode;
        }
    }
}
