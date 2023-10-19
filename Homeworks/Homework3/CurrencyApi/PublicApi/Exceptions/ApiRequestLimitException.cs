namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Exceptions
{
    public class ApiRequestLimitException : Exception
    {
        public int NumberOfRequests { get; }
        public ApiRequestLimitException(int numberOfRequests) : base()
        {
            NumberOfRequests = numberOfRequests;
        }
    }
}
