using System;
using FluentAssertions;
using NUnit.Framework;

namespace LSL.RabbitMqUri.Tests
{
    public class RabbitMqUriExtensionsTests
    {
        [TestCase(
            "amqp://user:pass@host:1234/",
            "host",
            1234,
            "",
            "user",
            "pass",
            false
        )]
        [TestCase(
            "amqp://user:pass@host",
            "host",
            5672,
            "",
            "user",
            "pass",
            false
        )]
        [TestCase(
            "amqp://user:pass@host/vhost",
            "host",
            5672,
            "vhost",
            "user",
            "pass",
            false
        )]
        [TestCase(
            "amqp://host/vhost",
            "host",
            5672,
            "vhost",
            "",
            "",
            false
        )]
        [TestCase(
            "amqp://asd@host/vhost",
            "host",
            5672,
            "vhost",
            "asd",
            "",
            false
        )]
        [TestCase(
            "amqp://asd@host/vhost/other",
            "host",
            5672,
            "vhost/other",
            "asd",
            "",
            false
        )]

        [TestCase(
            "amqps://user:pass@host:1234/",
            "host",
            1234,
            "",
            "user",
            "pass",
            true
        )]
        [TestCase(
            "amqps://user:pass@host",
            "host",
            5671,
            "",
            "user",
            "pass",
            true
        )]
        [TestCase(
            "amqps://user:pass@host/vhost",
            "host",
            5671,
            "vhost",
            "user",
            "pass",
            true
        )]
        [TestCase(
            "amqps://host/vhost",
            "host",
            5671,
            "vhost",
            "",
            "",
            true
        )]
        [TestCase(
            "amqps://asd@host/vhost",
            "host",
            5671,
            "vhost",
            "asd",
            "",
            true
        )]
        [TestCase(
            "amqps://asd@host/vhost/other",
            "host",
            5671,
            "vhost/other",
            "asd",
            "",
            true
        )]        
        public void GiveAValidUri_ItShouldReturnTheExpectedSettings(
            string uri,
            string expectedHost,
            int expectedPort,
            string expectedVirtualHost,
            string expectedUsername,
            string expectedPassword,
            bool expectedUseSsl)
        {
            var result = new Uri(uri).ToRabbitMqSettings();

            result.Should().BeEquivalentTo(new RabbitMqSettings
            {
                Host = expectedHost,
                Port = expectedPort,
                VirtualHost = expectedVirtualHost,
                Username = expectedUsername,
                Password = expectedPassword,
                UseSsl = expectedUseSsl
            });
        }


        [Test]
        public void GivenAndInvalidScheme_ItShouldThrowTheExpectedException()
        {
            var codeToRun = () => new Uri("nonsense://user:pass@host").ToRabbitMqSettings();
            codeToRun.Should().Throw<ArgumentException>()
                .WithMessage(
                    "The scheme 'nonsense' is unsupported. Only URI schemes of 'amqp' or 'amqps' are supported by RabbitMQ URIs (Parameter 'value')"
                );
        } 
    }
}