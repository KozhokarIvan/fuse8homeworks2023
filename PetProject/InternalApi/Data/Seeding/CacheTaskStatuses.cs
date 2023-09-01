using Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Seeding
{
    public class CacheTaskStatuses
    {
        public CacheTaskStatus[] Values { get; set; } = Array.Empty<CacheTaskStatus>();
        public CacheTaskStatuses()
        {
            Values = new CacheTaskStatus[]
            {
                new CacheTaskStatus() {Id = (int)Models.CacheTaskStatuses.Created, Name = "Задача создана"},
                new CacheTaskStatus() {Id = (int)Models.CacheTaskStatuses.InProgress, Name = "Задача в обработке"},
                new CacheTaskStatus() {Id = (int)Models.CacheTaskStatuses.Succeeded, Name = "Задача завершена успешно"},
                new CacheTaskStatus() {Id = (int)Models.CacheTaskStatuses.Failed, Name = "Задача завершена с ошибкой"},
                new CacheTaskStatus() {Id = (int)Models.CacheTaskStatuses.Cancelled, Name = "Задача отменена"}
            };
        }
    }
}
