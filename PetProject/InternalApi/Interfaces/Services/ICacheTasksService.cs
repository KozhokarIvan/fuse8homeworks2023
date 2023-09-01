namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Interfaces.Services
{
    public interface ICacheTasksService
    {
        Task<Guid> AddTaskAsync(string newBaseCurrency, CancellationToken cancellationToken);
    }
}
