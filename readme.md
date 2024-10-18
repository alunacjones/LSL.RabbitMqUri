[![Build status](https://img.shields.io/appveyor/ci/alunacjones/lsl-rabbitmquri.svg)](https://ci.appveyor.com/project/alunacjones/lsl-rabbitmquri)
[![Coveralls branch](https://img.shields.io/coverallsCoverage/github/alunacjones/LSL.RabbitMqUri)](https://coveralls.io/github/alunacjones/LSL.RabbitMqUri)
[![NuGet](https://img.shields.io/nuget/v/LSL.RabbitMqUri.svg)](https://www.nuget.org/packages/LSL.RabbitMqUri/)

# LSL.RabbitMqUri

A very basic package to parse a `Uri` or `string` into a `RabbitMqSettings` instance that exposes all the properties from an `amqp` or `amqps` RabbitMq `Uri`

# Usage

Converting a `URI` to `RabbitMqSettings`:

```csharp
using LSL.RabbitMqUri

var settings = new Uri("amqp://user:pass@host:1234/my-vhost").ToRabbitMqSettings();

/*
settings will be:
{
    "Host": "host",
    "Port": 1234,
    "Username": "user",
    "Password": "pass",
    "VirtualHost": "my-vhost",
    "UseSsl": false
}
*/
```

Converting a `string` to `RabbitMqSettings`:

```csharp
using LSL.RabbitMqUri

var settings = "amqps://user:pass@host:1234/my-vhost".ToRabbitMqSettings();

/*
settings will be:
{
    "Host": "host",
    "Port": 1234,
    "Username": "user",
    "Password": "pass",
    "VirtualHost": "my-vhost",
    "UseSsl": true
}
*/
```

# Defaults

The extension methods for both `Uri` and `string` will opt for the following defaults in the returned `RabbitMqSettings` instance

* If a port is omitted then the default RabbitMq port of `5672` will be returned for the `amqp` scheme otherwise `5671` is returned.
* A missing username or password in the uri/string will return an empty string for the relevant value.
* A scheme that is not `amqp` nor `amqps` will result in an `ArgumentException`

# Converting RabbitMqSettings back

## ToString

Calling `ToString()` on a `RabbitMqSettings` instance will convert it back to the string representation of the `Uri`.

## ToString(Action<TSettings> configurator)

Calling `ToString(Action<TSettings> configurator)` on a `RabbitMqSettings` instance will convert it back to the `string` representation of the `Uri`.

The configurator can be used to modify a cloned version of the settings prior to converting it to a `string`.

## ToUri(Action<TSettings> configurator = null)

Calling `ToString(Action<TSettings> configurator)` on a `RabbitMqSettings` instance will convert it back to a `Uri`.

If a configurator is specified then it can be used to modify a cloned version of the settings prior to converting it to a `Uri`.