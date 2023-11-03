﻿namespace Fuse8_ByteMinds.SummerSchool.PublicApi.Contracts.Responses
{

    /// <summary>
    /// Результат проверки работоспособности API
    /// </summary>
    public record HealthCheckResponse
    {
        /// <summary>
        /// Дата проверки
        /// </summary>
        public DateTimeOffset CheckedOn { get; init; }

        /// <summary>
        /// Статус работоспособности API
        /// </summary>
        public CheckStatus Status { get; init; }

        /// <summary>
        /// Статус API
        /// </summary>
        public enum CheckStatus
        {
            /// <summary>
            /// API работает
            /// </summary>
            Ok = 1,

            /// <summary>
            /// Ошибка в работе API
            /// </summary>
            Failed = 2,
        }
    }
}
