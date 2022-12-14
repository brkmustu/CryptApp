namespace Caching.Distributed;

public static class CachingSettings
{
    public static string GetRedisServiceName(bool withPort = true, bool withHttpPrefix = false)
    {
        string value = withHttpPrefix ? "http://localhost" : "redis";
        value = withPort ? value + ":6379" : value;
        var environmentValue = Environment.GetEnvironmentVariable("CommonSettings__RedisServiceName");
        return string.IsNullOrEmpty(environmentValue) ? value : environmentValue;
    }
}
