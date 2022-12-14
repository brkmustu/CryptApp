namespace Core.Shared;

public static class CommonSettings
{
    public static string GetApiGatewayUrl()
    {
        string url = "http://gateway";
        var environmentValue = Environment.GetEnvironmentVariable("CommonSettings__ApiGatewayUrl");
        return environmentValue.IsNullOrEmpty() ? url : environmentValue;
    }

    public static string GetRedisServiceName(bool withPort = true, bool withHttpPrefix = false)
    {
        string value = withHttpPrefix ? "http://redis" : "redis";
        value = withPort ? value + ":6379" : value;
        var environmentValue = Environment.GetEnvironmentVariable("CommonSettings__RedisServiceName");
        return environmentValue.IsNullOrEmpty() ? value : environmentValue;
    }

    public static string GetTokenValidationApiUrl(string accessToken)
    {
        string url = "http://auth.service:5000/api/auth/validate";
        var environmentValue = Environment.GetEnvironmentVariable("CommonSettings__TokenValidationApiUrl");
        return (environmentValue.IsNullOrEmpty() ? url : environmentValue) + "/validate?token=" + accessToken;
    }

    public static string GetEnvironmentServiceName()
    {
        string value = "";
        var environmentValue = Environment.GetEnvironmentVariable("ServiceName");
        return environmentValue.IsNullOrEmpty() ? value : environmentValue;
    }
    public static string GetEnvironmentServicePort()
    {
        string value = "";
        var environmentValue = Environment.GetEnvironmentVariable("ServicePort");
        return environmentValue.IsNullOrEmpty() ? value : environmentValue;
    }

}
