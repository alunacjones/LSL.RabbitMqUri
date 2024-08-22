using System;

namespace LSL.RabbitMqUri
{
    /// <summary>
    /// RabbitMqStringExtensions
    /// </summary>
    public static class RabbitMqStringExtensions
    {
        /// <summary>
        /// Convert a <c>string</c> to a <c ref="RabbitMqSettings">RabbitMqSettings</c> instance
        /// </summary>
        /// <param name="value">The string to convert to a RabbitMqSettings instance</param>
        /// <returns></returns>
        public static RabbitMqSettings ToRabbitMqSettings(this string value) => new Uri(value).ToRabbitMqSettings();
    }
}