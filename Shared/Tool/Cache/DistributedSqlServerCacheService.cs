namespace Tool.Cache;

using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Serialize;

public sealed class DistributedSqlServerCacheService : IDistributionCacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<DistributedSqlServerCacheService> _logger;
    private readonly ISerializeService _serializer;

    public DistributedSqlServerCacheService(IDistributedCache cache, ILogger<DistributedSqlServerCacheService> logger, [FromKeyedServices(SerializeFlag.NewtonSoft)] ISerializeService serialize)
    {
        _cache = cache;
        _logger = logger;
        _serializer = serialize;
        _logger.LogInformation("Distributed SqlServer Cache Start working");
    }

    public void Set<TSource>(string key, TSource source, DateTime? absoluteExpiration, TimeSpan? slidingExpiration)
    {
        _logger.LogTrace("Distributed SqlServer Cache {obj} with key : {key} " +
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
            _logger.LogTrace("Distributed SqlServer Cache Failed Get Cache with key : {key}", key);
        },
        () =>
        {
            _logger.LogTrace("Distributed SqlServer Cache Successful Get Cache with key : {key} and data : {data}",
                         key, output);

            result = _serializer.Deserialize<TOutput>(output);
        });
        return result;
    }

    public void Remove(string key)
    {
        _logger.LogTrace("Distributed SqlServer Cache Remove Cache with key : {key}", key);
        _cache.Remove(key);
    }

    public void Refresh(string key)
    {
        _logger.LogTrace("Distributed SqlServer Cache Reset Sliding Expirtaion with key : {key}", key);
        _cache.Refresh(key);
    }
}

public class DistributedSqlServerCacheConfig
{
    public bool AutoCreateTable { get; set; } = true;
    public string ConnectionString { get; set; } = string.Empty;
    public string Schema { get; set; } = "dbo";
    public string Table { get; set; } = "SqlSecverCache";
}
