namespace Caching.Distributed;

public interface ICachingService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
    Task<T> SetGetAsync<T>(string key, T value, TimeSpan? expiry = null);
}