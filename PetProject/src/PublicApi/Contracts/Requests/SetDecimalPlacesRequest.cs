namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Requests
{
    /// <summary>
    /// Модель запроса для установки точности округления
    /// </summary>
    public class SetDecimalPlacesRequest
    {
        /// <summary>
        /// Точность округления (количество разрядов после запятой)
        /// </summary>
        /// <example>2</example>
        public int DecimalPlaces { get; init; }
    }
}
