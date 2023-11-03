namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts.ExternalCurrencyApi
{
    public class Quota
    {
        public int Total { get; init; }
        public int Used { get; init; }
        public int Remaining { get; init; }
    }
}
