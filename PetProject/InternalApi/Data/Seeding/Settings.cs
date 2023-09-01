using Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Entities;
using Fuse8_ByteMinds.SummerSchool.InternalApi.Options;
using Microsoft.Extensions.Options;

namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Data.Seeding
{
    public class Settings
    {
        public Setting[] Values { get; set; } = Array.Empty<Setting>();
        public Settings()
        {
            Values = new Setting[]
            {
                new Setting
                {
                    Id = 1,
                    Name = "baseCurrency",
                    Value = "USD"
                }
            };
        }
    }
}
