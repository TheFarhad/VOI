namespace VOI.Keyword.Infrastructure.Persistence.EF.Command.Context;

using Shared;
using Domain.Aggregates.Keyword.Entity;
using Domain.Aggregates.Category.Entity;
using Framework.Infra.Persistence.Command;

public sealed class KeywordCommandDbContext : EventSourcingEFCoreCommandDbContext<KeywordCommandDbContext>
{
    public DbSet<Keyword> Keywords => Set<Keyword>();
    public DbSet<Category> Categories => Set<Category>();

    private KeywordCommandDbContext() : base() { }
    public KeywordCommandDbContext(DbContextOptions<KeywordCommandDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = GetType().Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        modelBuilder.HasDefaultSchema(KeywordServiceDbContextSchema.DefaultSchema);

        base.OnModelCreating(modelBuilder);
    }
}
