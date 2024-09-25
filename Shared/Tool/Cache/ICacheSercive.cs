namespace Tool.Cache;

public interface ICacheSercive
{
    void Set<TSource>(string key, TSource source, DateTime? absoluteExpiration, TimeSpan? slidingExpiration);
    TOutput Get<TOutput>(string key);
    void Remove(string key);
}

public interface IDistributionCacheService : ICacheSercive
{
    void Refresh(string key);
}

public interface IDistributionFullRedisCache
{
    // متودهای مورد نیاز برای استفاده از انواع روشهای ذخیره سازی داده ها اینجا نوشته شود
}

public enum DistributedCacheFlag : byte
{
    DistributedRedis = 0,
    DistributedSqlServer = 1
}
