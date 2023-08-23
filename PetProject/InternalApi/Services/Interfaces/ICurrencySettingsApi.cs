namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Interfaces
{
    public interface ICurrencySettingsApi
    {
        public Task<(int requestCount, int requestLimit)> GetRequestQuotas();
    }
}
