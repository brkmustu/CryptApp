using MessageQueue.Shared.RabbitMq;
using Microsoft.Extensions.Configuration;

namespace MessageQueue.Shared;

public static class MessageQueueSettings
{
    public static RabbitMqOptions GetRabbitMqSettings(IConfiguration configuration)
    {
        var appSettingsSection = configuration.GetSection(RabbitMqOptions.SectionName);
        var options = appSettingsSection.Get<RabbitMqOptions>();

        var hostName = Environment.GetEnvironmentVariable("RabbitMqOptions__HostName");
        var virtualHost = Environment.GetEnvironmentVariable("RabbitMqOptions__VirtualHost");
        var userName = Environment.GetEnvironmentVariable("RabbitMqOptions__UserName");
        var password = Environment.GetEnvironmentVariable("RabbitMqOptions__Password");
        var queueName = Environment.GetEnvironmentVariable("RabbitMqOptions__QueueName");

        if (!string.IsNullOrEmpty(hostName))
        {
            options.HostName = hostName;
        }
        if (!string.IsNullOrEmpty(virtualHost))
        {
            options.VirtualHost = virtualHost;
        }
        if (!string.IsNullOrEmpty(userName))
        {
            options.UserName = userName;
        }
        if (!string.IsNullOrEmpty(password))
        {
            options.Password = password;
        }
        if (!string.IsNullOrEmpty(queueName))
        {
            options.QueueName = queueName;
        }

        return options;
    }
}
