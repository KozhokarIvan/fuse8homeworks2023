using Fuse8_ByteMinds.SummerSchool.PublicApi.Data;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Data.Entities;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Repositories;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly PublicApiDbContext _context;
        private readonly IOptionsSnapshot<PublicApiSettings> _options;
        public SettingsRepository(PublicApiDbContext context, IOptionsSnapshot<PublicApiSettings> options)
        {
            _context = context;
            _options = options;
        }

        public async Task SetDefaultCurrency(string currencyCode, CancellationToken cancellationToken)
        {
            var baseCurrency = await _context.Settings.FirstAsync(s
                    => s.Name == _options.Value.CurrencySettingName,
                    cancellationToken);
            if (baseCurrency is null)
                _context.Add(new Setting
                {
                    Name = _options.Value.CurrencySettingName,
                    Value = currencyCode.ToLower()
                });
            else
                baseCurrency.Value = currencyCode.ToLower();
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SetDecimalPlaces(int decimalPlacesNumber, CancellationToken cancellationToken)
        {
            var decimalPlaces = await _context.Settings.FirstAsync(s
                => s.Name == _options.Value.DecimalPlacesSettingName,
                cancellationToken);
            if (decimalPlaces is null)
                _context.Add(new Setting
                {
                    Name = _options.Value.DecimalPlacesSettingName,
                    Value = decimalPlacesNumber.ToString()
                });
            else
                decimalPlaces.Value = decimalPlacesNumber.ToString();
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<string> GetDefaultCurrency(CancellationToken cancellationToken)
        {
            var currency = await _context.Settings
                .AsNoTracking()
                .FirstAsync(s => s.Name == _options.Value.CurrencySettingName,
                cancellationToken);
            return currency.Value;
        }

        public async Task<int> GetDecimalPlaces(CancellationToken cancellationToken)
        {
            var decimalPlaces = await _context.Settings
                .AsNoTracking()
                .FirstAsync(s => s.Name == _options.Value.DecimalPlacesSettingName,
                cancellationToken);
            return int.Parse(decimalPlaces.Value);
        }
    }
}
