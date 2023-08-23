namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Exceptions
{
    public class CurrencyNotFoundException : Exception
    {
        public string Currency { get; }
        public CurrencyNotFoundException(string currency) : base()
        {
            Currency = currency;
        }
    }
}
