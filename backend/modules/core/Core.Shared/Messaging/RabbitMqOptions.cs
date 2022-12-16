using Microsoft.Extensions.Configuration;

namespace Core.Shared.Messaging;

public class RabbitMqOptions
{
    public const string SectionName = "RabbitMqOptions";
    public string HostName { get; set; }
    public string VirtualHost { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public static RabbitMqOptions GetOptions(IConfiguration configuration)
    {
        var appSettingsSection = configuration.GetSection(SectionName);
        var options = appSettingsSection.Get<RabbitMqOptions>();

        var hostName = Environment.GetEnvironmentVariable("RabbitMqOptions__HostName");
        var virtualHost = Environment.GetEnvironmentVariable("RabbitMqOptions__VirtualHost");
        var userName = Environment.GetEnvironmentVariable("RabbitMqOptions__UserName");
        var password = Environment.GetEnvironmentVariable("RabbitMqOptions__Password");

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

        return options;
    }
}
