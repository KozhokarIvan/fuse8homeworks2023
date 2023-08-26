namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Requests
{
    public class SetDecimalPlacesRequest
    {
        /// <summary>
        /// Точность округления (количество разрядов после запятой)
        /// </summary>
        /// <example>2</example>
        public int DecimalPlaces { get; init; }
    }
}
