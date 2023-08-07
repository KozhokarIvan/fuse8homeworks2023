namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Exceptions
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
