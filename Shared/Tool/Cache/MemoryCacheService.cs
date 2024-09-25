namespace Tool.Cache;

using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Serialize;

public sealed class MemoryCacheService : ICacheSercive
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<MemoryCacheService> _logger;
    private readonly ISerializeService _serializer;

    public MemoryCacheService(IMemoryCache cache, ILogger<MemoryCacheService> logger, [FromKeyedServices(SerializeFlag.NewtonSoft)] ISerializeService serialize)
    {
        _cache = cache;
        _logger = logger;
        _serializer = serialize;
        _logger.LogInformation("Memory Cache Start working");
    }

    public void Set<TSource>(string key, TSource source, DateTime? absoluteExpiration, TimeSpan? slidingExpiration)
    {
        _logger.LogTrace("MemoryCache Cache {obj} with key : {key} " +
                      ", with data : {data} " +
                      ", with absoluteExpiration : {absoluteExpiration} " +
                      ", with slidingExpiration : {slidingExpiration}",
                      typeof(TSource),
                      key,
                      _serializer.Serialize(source),
                      absoluteExpiration.ToString(),
                      slidingExpiration.ToString());

        _cache.Set(key, source, new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = absoluteExpiration,
            SlidingExpiration = slidingExpiration
        });
    }

    public TOutput Get<TOutput>(string key)
    {
        TOutput result = default!;
        _logger.LogTrace("MemoryCache Try Get Cache with key : {key}", key);

        if (_cache.TryGetValue(key, out result))
        {
            _logger
                .LogTrace("MemoryCache Successful Get Cache with key : {key} and data : {data}",
                                    key,
                                    _serializer.Serialize(result));
        }
        else _logger.LogTrace("MemoryCache Failed Get Cache with key : {key}", key);

        return result;
    }

    public void Remove(string key)
    {
        //_logger.LogTrace("InMemoryCache Remove Cache with key : {key}", key);
        _cache.Remove(key);
    }
}
