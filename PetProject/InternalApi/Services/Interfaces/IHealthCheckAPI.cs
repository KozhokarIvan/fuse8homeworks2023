namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Interfaces
{
    public interface IHealthCheckAPI
    {
        public Task<bool> CheckHealth();
    }
}
