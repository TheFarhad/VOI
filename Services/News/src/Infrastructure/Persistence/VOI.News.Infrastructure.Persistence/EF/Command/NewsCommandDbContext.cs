namespace VOI.News.Infrastructure.Persistence.EF.Command;

using Microsoft.EntityFrameworkCore;
using Shared;
using Domian.News.Entity;
using Framework.Infra.Persistence.Command;

public sealed class NewsCommandDbContext : EventSourcingEFCoreCommandDbContext<NewsCommandDbContext>
{
    public DbSet<News> News => Set<News>();

    private NewsCommandDbContext() : base() { }
    public NewsCommandDbContext(DbContextOptions<NewsCommandDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = GetType().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        modelBuilder.HasDefaultSchema(NewsServiceDbContextSchema.DefaultSchema);

        base.OnModelCreating(modelBuilder);
    }
}
