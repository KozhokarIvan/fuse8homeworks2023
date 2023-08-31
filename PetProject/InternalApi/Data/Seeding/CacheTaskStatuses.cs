using Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Seeding
{
    public class CacheTaskStatuses
    {
        public CacheTaskStatus[] Values { get; set; }= Array.Empty<CacheTaskStatus>();
        public CacheTaskStatuses()
        {
            Values = new CacheTaskStatus[] 
            {
                new CacheTaskStatus() {Id = 1, Name = "Задача создана"},
                new CacheTaskStatus() {Id = 2, Name = "Задача в обработке"},
                new CacheTaskStatus() {Id = 3, Name = "Задача завершена успешно"},
                new CacheTaskStatus() {Id = 4, Name = "Задача завершена с ошибкой"},
                new CacheTaskStatus() {Id = 5, Name = "Задача отменена"}
            };
        }
    }
}
