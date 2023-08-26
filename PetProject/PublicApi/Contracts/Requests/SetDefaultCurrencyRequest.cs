using Fuse8_ByteMinds.SummerSchool.InternalApi.Contracts.Requests;

namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Requests
{
    public class SetDefaultCurrencyRequest
    {
        /// <summary>
        /// Валюта по умолчанию
        /// </summary>
        /// <example>RUB</example>
        public CurrencyCode CurrencyCode { get; init; }
    }
}
