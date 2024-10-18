using System;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace LSL.RabbitMqUri.Tests
{
    public class RabbitMqSettingsTests
    {
        [TestCase(
            """
            {
                "Host": "my-host",
                "Port": 5672,
                "UseSsl": true,
                "VirtualHost": "my-host"
            }
            """,
            "amqps://my-host:5672/my-host"
        )]
        [TestCase(
            """
            {
                "Host": "my-host",
                "UseSsl": true,
                "VirtualHost": "my-host"
            }
            """,
            "amqps://my-host:5671/my-host"
        )]
        [TestCase(
            """
            {
                "Host": "my-host",
                "UseSsl": false,
                "VirtualHost": "my-host"
            }
            """,
            "amqp://my-host:5672/my-host"
        )]
        [TestCase(
            """
            {
                "Host": "my-host",
                "UseSsl": false,
                "VirtualHost": "my-host",
                "Username": "my-user",
                "Password": "my-pass"
            }
            """,
            "amqp://my-user:my-pass@my-host:5672/my-host"
        )]
        [TestCase(
            """
            {
                "Host": "my-host",
                "UseSsl": true,
                "VirtualHost": "my-host",
                "Username": "my-user",
                "Password": "my-pass"
            }
            """,
            "amqps://my-user:my-pass@my-host:5671/my-host"
        )]
        [TestCase(
            """
            {
                "Host": "my-host",
                "UseSsl": false,
                "Username": "my-user",
                "Password": "my-pass"
            }
            """,
            "amqp://my-user:my-pass@my-host:5672/"
        )]
        [TestCase(
            """
            {
                "Host": "my-host",
                "UseSsl": true,
                "Username": "my-user",
                "Password": "my-pass"
            }
            """,
            "amqps://my-user:my-pass@my-host:5671/"
        )]                
        public void GivenAValue_ItShouldReturnTheExpectedResult(string settingsJson, string expectedValue)
        {
            var settings = JsonConvert.DeserializeObject<RabbitMqSettings>(settingsJson);

            settings.ToUri().Should().Be(new Uri(expectedValue));
            settings.ToString().Should().Be(expectedValue);
        }

        [Test]
        public void GivenAValue_AndWeConfigureIt_ItShouldProduceTheExpectedReulst()
        {
            var settings = new RabbitMqSettings
            {
                Host = "my-host",
                UseSsl = false,
                VirtualHost = "my-host",
                Username = "my-user",
                Password = "my-pass"
            };

            var expected = "amqp://my-user:my-pass@my-host:5672/new-vhost";

            settings.ToUri(c => c.VirtualHost = "new-vhost").Should().Be(new Uri(expected));
            settings.ToString(c => c.VirtualHost = "new-vhost").Should().Be(expected);
            settings.VirtualHost.Should().Be("my-host");
        }
    }    
}