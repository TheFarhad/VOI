namespace Framework.Endpoint;

using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using Tool.File;
using Tool.Encrypt;
using Tool.Identity;
using Tool.Serialize;
using Tool.Compression;
using Endpoint.HostedServices;
using Infra.Persistence.Command;
using System.Text.Json.Serialization;

public static class Dependencies
{
    public static IServiceCollection FrameworkEndpointDependencies(this IServiceCollection service, IConfiguration configuration)
    {
        service
           .AddHttpContextAccessor()
           .FilingDependencies()
           .GZipLargeObjectCompressoionDependencies()
           .SerializationDependencies()
           .EncryptionDependencies()
           .IdentityDependencies(configuration)
           .Configure<GzipCompressionProviderOptions>(options =>
           {
               options.Level = CompressionLevel.Optimal;
           })
           .Configure<BrotliCompressionProviderOptions>(options =>
           {
               options.Level = CompressionLevel.Optimal;
           })
           .AddResponseCompression(_ =>
           {
               _.EnableForHttps = true;
               _.Providers.Add<GzipCompressionProvider>();
               _.Providers.Add<BrotliCompressionProvider>();
           })
           //.AddExceptionHandler<GlobalExceptionHandler>()
           .AddHostedService<RegistryHostedService>()
           .AddSwaggerGen()
           .AddEndpointsApiExplorer()
           .AddControllers()
           .AddJsonOptions(_ =>
           {
               _.JsonSerializerOptions
                   .Converters
                   .Add(new JsonStringEnumConverter());
           });

        return service;
    }

    public static async Task DbContextAutoMigrator<TCommandDbContext>(this WebApplication app)
       where TCommandDbContext : EFCommandDbContext<TCommandDbContext>
    {
        using var scope = app.Services.CreateScope();
        await scope
                .ServiceProvider
                .GetRequiredService<TCommandDbContext>()
                .Database
                .MigrateAsync();
    }
}
