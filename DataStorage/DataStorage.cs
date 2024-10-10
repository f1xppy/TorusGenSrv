using System.Runtime.Caching;

namespace TorusGenSrv.DataStorage;
public static class DataStorage
{
    private static MemoryCache cache = MemoryCache.Default;

    public static void SetData(string key, object value, TimeSpan? expiration = null)
    {
        CacheItemPolicy policy = new();
        if (expiration.HasValue)
        {
            policy.AbsoluteExpiration = DateTimeOffset.Now.Add(expiration.Value);
        }
        cache.Set(key, value, policy);
    }

    public static object GetData(string key)
    {
        return cache.Get(key);
    }

    public static void RemoveData(string key)
    {
        cache.Remove(key);
    }
}