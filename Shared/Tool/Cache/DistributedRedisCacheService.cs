namespace Tool.Cache;

using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Shared;
using Serialize;

public sealed class DistributedRedisCacheService : IDistributionCacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<DistributedRedisCacheService> _logger;
    private readonly ISerializeService _serializer;

    public DistributedRedisCacheService(IDistributedCache cache, ILogger<DistributedRedisCacheService> logger, ISerializeService serialize)
    {
        _cache = cache;
        _logger = logger;
        _serializer = serialize;
        _logger.LogInformation("Distributed Redis Cache Start working");
    }

    public void Set<TSource>(string key, TSource source, DateTime? absoluteExpiration, TimeSpan? slidingExpiration)
    {
        _logger.LogTrace("Distributed Redis Cache {obj} with key : {key} " +
                      ", with data : {data} " +
                      ", with absoluteExpiration : {absoluteExpiration} " +
                      ", with slidingExpiration : {slidingExpiration}",
                      typeof(TSource),
                      key,
                      _serializer.Serialize(source),
                      absoluteExpiration.ToString(),
                      slidingExpiration.ToString());

        var bytes = Encoding.UTF8.GetBytes(_serializer.Serialize(source));
        _cache.Set(key, bytes, new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = absoluteExpiration,
            SlidingExpiration = slidingExpiration
        });
    }

    public TOutput Get<TOutput>(string key)
    {
        var result = default(TOutput);
        var output = _cache.GetString(key);

        output.IsNull(() =>
        {
            _logger.LogTrace("Distributed Redis Cache Failed Get Cache with key : {key}", key);
        },
        () =>
        {
            _logger.LogTrace("Distributed Redis Cache Successful Get Cache with key : {key} and data : {data}",
                         key, output);

            result = _serializer.Deserialize<TOutput>(output);
        });
        return result;
    }

    public void Remove(string key)
    {
        _logger.LogTrace("Distributed Redis Cache Remove Cache with key : {key}", key);
        _cache.Remove(key);
    }

    public void Refresh(string key)
    {
        _logger.LogTrace("Distributed Redis Cache Reset Sliding Expirtaion with key : {key}", key);
        _cache.Refresh(key);
    }
}

public sealed class DistributedFullRedisCache : IDistributionFullRedisCache
{
    private readonly IConnectionMultiplexer _cache;
    private readonly ILogger<DistributedRedisCacheService> _logger;
    private readonly ISerializeService _serializer;

    public DistributedFullRedisCache(IConnectionMultiplexer cache, ILogger<DistributedRedisCacheService> logger, ISerializeService serialize)
    {
        _cache = cache;
        _logger = logger;
        _serializer = serialize;
        _logger.LogInformation("Distributed Redis Cache Start working");
    }
}

public sealed class DistributedRedisCacheConfig
{
    public string HostName { get; set; } = String.Empty;
    public string Port { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    /// <summary>
    /// add to the first of keys =>
    /// example(application name)
    /// </summary>
    public string InstanceName { get; set; } = String.Empty;
}
