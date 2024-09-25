namespace VOI.Keyword.Infrastructure.Persistence.EF.Query.Context;

using QueryModels;
using Shared;
using Framework.Infra.Persistence.Query;

internal sealed class KeywordQueryDbContext(DbContextOptions<KeywordQueryDbContext> options)
    : EFQueryDbContext<KeywordQueryDbContext>(options)
{
    public DbSet<Keyword> Keywords => Set<Keyword>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(KeywordServiceDbContextSchema.DefaultSchema);
        base.OnModelCreating(modelBuilder);
    }
}
