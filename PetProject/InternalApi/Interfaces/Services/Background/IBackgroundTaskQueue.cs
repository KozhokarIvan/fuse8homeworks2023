using Fuse8_ByteMinds.SummerSchool.InternalApi.Models;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services.Background
{
    public interface IBackgroundTaskQueue
    {
        ValueTask QueueAsync(WorkItem command, CancellationToken cancellationToken);
        ValueTask<WorkItem> DequeueAsync(CancellationToken cancellationToken);
    }
}
