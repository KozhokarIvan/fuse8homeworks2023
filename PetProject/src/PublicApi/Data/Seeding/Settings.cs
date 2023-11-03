using Fuse8_ByteMinds.SummerSchool.PublicApi.Data.Entities;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Data.Seeding
{
    public class Settings
    {
        public Setting[] Values { get; set; } = Array.Empty<Setting>();
        public Settings()
        {
            Values = new Setting[]
            {
                new Setting { Id = 1, Name = "defaultCurrency", Value = "RUB" },
                new Setting { Id = 2, Name = "currencyRoundCount", Value = "2" }
            };
        }
    }
}
