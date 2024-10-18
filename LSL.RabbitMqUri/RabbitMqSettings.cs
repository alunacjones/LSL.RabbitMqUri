using System;

namespace LSL.RabbitMqUri
{
    /// <summary>
    /// The RabbitMq connection settings
    /// </summary>
    public class RabbitMqSettings
    {
        /// <summary>
        /// The RabbitMQ Host
        /// </summary>
        /// <value></value>
        public string Host { get; set; }

        /// <summary>
        /// The RabbitMQ Port
        /// </summary>
        /// <value></value>
        public int Port { get; set; }

        /// <summary>
        /// The username to log into the RabbitMq instance
        /// </summary>
        /// <value></value>
        public string Username { get; set; }

        /// <summary>
        /// The password to lolg into the RabbitMq instance
        /// </summary>
        /// <value></value>
        public string Password { get; set; }

        /// <summary>
        /// The virtual host to connect to on the RabbitMq Host
        /// </summary>
        /// <value></value>
        public string VirtualHost { get; set; }

        /// <summary>
        /// Indicates as to whether the connection should use SSL
        /// </summary>
        /// <value></value>
        public bool UseSsl { get; set; }

        /// <summary>
        /// Clones this instance
        /// </summary>
        /// <returns></returns>
        public RabbitMqSettings Clone()
        {
            return new RabbitMqSettings
            {
                Host = Host,
                Port = Port,
                Username = Username,
                Password = Password,
                UseSsl = UseSsl,
                VirtualHost = VirtualHost                
            };
        }

        /// <summary>
        /// Convert a <c>RabbitMqSettings</c> instance to a Uri
        /// </summary>
        /// <remarks>A clone of the original settings is created</remarks>
        /// <param name="configurator">Optional configurator to setup the the cloned settings prior to converting to a <c>Uri</c></param>
        /// <returns></returns>
        public Uri ToUri(Action<RabbitMqSettings> configurator = null)
        {
            var newInstance = Clone();

            configurator?.Invoke(newInstance);

            var builder = new UriBuilder
            {
                Path = newInstance.VirtualHost,
                Password = newInstance.Password,
                UserName = newInstance.Username,
                Port = newInstance.Port == 0 
                    ? newInstance.UseSsl ? 5671 : 5672
                    : newInstance.Port,
                Scheme = newInstance.UseSsl ? "amqps" : "amqp",
                Host = newInstance.Host
            };

            return builder.Uri;
        }

        /// <summary>
        /// Converts a <c>RabbitMqSettings</c> instance to a string
        /// </summary>
        /// <remarks>A clone of the original settings is created</remarks>
        /// <returns></returns>
        public override string ToString() => ToUri().AbsoluteUri;

        /// <summary>
        /// Convert a <c>RabbitMqSettings</c> instance to a Uri
        /// </summary>
        /// <remarks>A clone of the original settings is created</remarks>
        /// <param name="configurator">Configurator to setup the the cloned settings prior to converting to a <c>string</c></param>
        /// <returns></returns>
        public string ToString(Action<RabbitMqSettings> configurator) => ToUri(configurator).AbsoluteUri;
    }
}