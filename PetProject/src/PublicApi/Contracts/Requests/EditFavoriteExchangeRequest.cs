﻿namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Requests
{
    /// <summary>
    /// Модель запроса для редактирования избранной именованной пары валют
    /// </summary>
    public class EditFavoriteExchangeRequest
    {
        /// <summary>
        /// Код валюты
        /// </summary>
        /// <example>RUB</example>
        public string Currency { get; init; } = string.Empty;

        /// <summary>
        /// Код валюты
        /// </summary>
        /// <example>KZT</example>
        public string BaseCurrency { get; init; } = string.Empty;
    }
}
