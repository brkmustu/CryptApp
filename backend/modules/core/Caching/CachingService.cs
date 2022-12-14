using Newtonsoft.Json;
using StackExchange.Redis;

namespace Caching;

public class CachingService : ICachingService
{
    private readonly IConnectionMultiplexer _redis;

    public CachingService(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var db = _redis.GetDatabase();

        var jsonValue = await db.StringGetAsync(key);

        if (jsonValue.IsNullOrEmpty)
            return default;

        return JsonConvert.DeserializeObject<T>(jsonValue.ToString());
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var db = _redis.GetDatabase();

        await db.StringSetAsync(key, JsonConvert.SerializeObject(value), expiry);
    }

    public async Task<T> SetGetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var db = _redis.GetDatabase();

        var existingValue = await GetAsync<T>(key);

        if (existingValue != null)
        {
            return existingValue;
        }

        var result = await db.StringSetAndGetAsync(key, JsonConvert.SerializeObject(value), expiry);

        return JsonConvert.DeserializeObject<T>(result.ToString());
    }
}
