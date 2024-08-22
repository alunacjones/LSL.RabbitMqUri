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
    }
}