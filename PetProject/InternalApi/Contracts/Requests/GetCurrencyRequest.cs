namespace Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts.Requests
{
    public class GetCurrencyRequest
    {
        /// <summary>
        /// Валюта для которой нужно получить курс относительно базовой
        /// </summary>
        /// <example>RUB</example>
        public CurrencyCode CurrencyType { get; init; }
    }
}
