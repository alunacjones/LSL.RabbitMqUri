using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace LSL.RabbitMqUri
{
    /// <summary>
    /// RabbitMqUriExtensions
    /// </summary>
    public static class RabbitMqUriExtensions
    {
        /// <summary>
        /// Convert a <c>Uri</c> to a <c ref="RabbitMqSettings">RabbitMqSettings</c> instance
        /// </summary>
        /// <param name="value">The URI to convert to a RabbitMqSettings instance</param>
        /// <returns></returns>
        public static RabbitMqSettings ToRabbitMqSettings(this Uri value)
        {
            if (!_schemeMatcher.IsMatch(value.Scheme))
            {
                throw new ArgumentException(
                    $"The scheme '{value.Scheme}' is unsupported. Only URI schemes of 'amqp' or 'amqps' are supported by RabbitMQ URIs", 
                    nameof(value)
                );
            }

            var splitValues = value.UserInfo.Split(':');

            return new RabbitMqSettings
            {
                Host = value.Host,
                Port = value.Port == -1 ? 5672 : value.Port,
                Password = splitValues.ElementAtOrDefault(1) ?? string.Empty,
                Username = splitValues.ElementAtOrDefault(0) ?? string.Empty,
                VirtualHost = value.LocalPath.TrimStart('/'),
                UseSsl = value.Scheme.Equals("amqps", StringComparison.InvariantCultureIgnoreCase)
            };
        }

        private static readonly Regex _schemeMatcher = new Regex("^amqps?$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    }
}