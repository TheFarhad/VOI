namespace Tool.Cache;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Dapper;
using StackExchange.Redis;
using Tool.Cache;

public static class Dependencies
{
    public static IServiceCollection InMemoryCacheDependencies(this IServiceCollection source) =>
        source
            .AddMemoryCache()
            .AddTransient<ICacheSercive, MemoryCacheService>();

    public static IServiceCollection DistributedRedisCacheDependencies(this IServiceCollection source, IConfiguration configuration, string redisCacheConfigSection)
    {
        // در روش اول، فقط میتوانیم داده ها را به صورت رشته یا باینری در ردیس ذخیره کنیم

        source.Configure<DistributedRedisCacheConfig>(configuration.GetSection(redisCacheConfigSection));
        var redisConfigs = configuration.Get<DistributedRedisCacheConfig>();

        source
            .AddStackExchangeRedisCache(_ =>
            {
                _.Configuration = $"{redisConfigs.HostName}:{redisConfigs.Port}";
                _.InstanceName = redisConfigs.InstanceName;
            })
            .AddKeyedTransient<IDistributionCacheService, DistributedRedisCacheService>(DistributedCacheFlag.DistributedRedis);

        #region to access to complete features of redis

        // در این روش میشود از انواع روش های ذخیره سازی داده ها در ردیس استفاده کنیم
        // حتما باید برای استفاده از این روش، کلاس جدیدی نوشته شود تا بتواند انواع روشه ای ذخیره سازی را ساپورت کند

        //var options = new ConfigurationOptions { Password = redisConfigs.Password };
        //options.EndPoints.Add($"{redisConfigs.HostName}:{redisConfigs.Port}");
        //source
        //   .AddStackExchangeRedisCache(_ =>
        //   {
        //       _.ConnectionMultiplexerFactory = async () => await ConnectionMultiplexer.ConnectAsync(options);
        //       _.InstanceName = redisConfigs.InstanceName;
        //   })
        //   .AddTransient<IDistributionFullRedisCache, DistributedFullRedisCache>();

        #endregion

        return source;
    }

    public static IServiceCollection DistributedSqlServerCacheDependencies(this IServiceCollection source, IConfiguration configuration, string sqlServerCacheConfigSection)
    {
        source.Configure<DistributedSqlServerCacheConfig>(configuration.GetSection(sqlServerCacheConfigSection));
        var configs = configuration.Get<DistributedSqlServerCacheConfig>();

        if (configs?.AutoCreateTable is true) CreateTable(configs);

        source
            .AddDistributedSqlServerCache(_ =>
            {
                _.ConnectionString = configs.ConnectionString;
                _.SchemaName = configs.Schema;
                _.TableName = configs.Table;
            })
            .AddKeyedTransient<IDistributionCacheService, DistributedSqlServerCacheService>(DistributedCacheFlag.DistributedSqlServer);

        return source;
    }

    private static void CreateTable(DistributedSqlServerCacheConfig configs)
    {
        var table = configs.Table;
        var schema = configs.Schema;

        var command =
            $@"IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
                                                WHERE TABLE_SCHEMA={schema} AND TABLE_NAME={table})){"\n"}
            BEGIN {"\n"}
            CREATE TABLE [{schema}].[{table}]{"\n"}
            ({"\n"}
            [Id][nvarchar](449) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,{"\n"}
            [Value] [varbinary](max)NOT NULL,{"\n"}
            [ExpiresAtTime] [datetimeoffset](7) NOT NULL,{"\n"}
            [SlidingExpirationInSeconds] [bigint] NULL,{"\n"}
            [AbsoluteExpiration] [datetimeoffset](7) NULL,{"\n"}
            PRIMARY KEY(Id),{"\n"}
            INDEX Index_ExpiresAtTime NONCLUSTERED (ExpiresAtTime)){"\n"}
            ){"\n"}
            End";

        new SqlConnection(configs.ConnectionString).Execute(command);
    }
}
