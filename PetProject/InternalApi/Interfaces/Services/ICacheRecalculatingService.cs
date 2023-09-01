namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services
{
    public interface ICacheRecalculatingService
    {
        Task RecalculateCacheAsync(Guid taskId, CancellationToken cancellationToken);
        Task SetTaskFailed(Guid taskId, CancellationToken cancellationToken);
        Task<Guid?> AcceptLastUnfinishedTask(CancellationToken cancellationToken);
    }
}
