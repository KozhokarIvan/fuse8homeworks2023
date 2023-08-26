using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Repositories;
using Fuse8_ByteMinds.SummerSchool.PublicApi.Interfaces.Services;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _settingsRepository;
        public SettingsService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }
        public async Task SetDefaultCurrency(string currencyCode, CancellationToken cancellationToken)
            => await _settingsRepository.SetDefaultCurrency(currencyCode, cancellationToken);
        public async Task SetDecimalPlaces(int decimalPlaces, CancellationToken cancellationToken)
            => await _settingsRepository.SetDecimalPlaces(decimalPlaces, cancellationToken);
        public async Task<(string defaultCurrency, int decimalPlaces)> GetSettings(CancellationToken cancellationToken)
        {
            string defaultCurrency = await _settingsRepository.GetDefaultCurrency(cancellationToken);
            int decimalPlaces = await _settingsRepository.GetDecimalPlaces(cancellationToken);
            return (defaultCurrency, decimalPlaces);
        }
    }
}
