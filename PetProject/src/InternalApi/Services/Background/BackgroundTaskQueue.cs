using System.Threading.Channels;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services.Background;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Models;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Options;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Services.Background
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<WorkItem> _channel;

        public BackgroundTaskQueue(IOptions<InternalApiSettings> apiOptions)
        {
            var options = new BoundedChannelOptions(apiOptions.Value.CacheChannelCapacity) { FullMode = BoundedChannelFullMode.Wait };
            _channel = Channel.CreateBounded<WorkItem>(options);
        }

        public ValueTask QueueAsync(WorkItem command, CancellationToken cancellationToken)
            => _channel.Writer.WriteAsync(command, cancellationToken);

        public ValueTask<WorkItem> DequeueAsync(CancellationToken cancellationToken)
            => _channel.Reader.ReadAsync(cancellationToken);
    }
}
