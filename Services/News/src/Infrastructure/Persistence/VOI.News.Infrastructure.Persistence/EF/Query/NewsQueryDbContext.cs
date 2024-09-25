namespace VOI.News.Infrastructure.Persistence.EF.Query;

using Models.NewsModel;
using Framework.Infra.Persistence.Query;

internal sealed class NewsQueryDbContext : EFQueryDbContext<NewsQueryDbContext>
{
    public DbSet<News> News => Set<News>();
    public DbSet<Keyword> Keywords => Set<Keyword>();
    public DbSet<KeywordDetail> KeywordDetails => Set<KeywordDetail>();

    private NewsQueryDbContext() : base() { }
    public NewsQueryDbContext(DbContextOptions<NewsQueryDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(NewsServiceDbContextSchema.DefaultSchema);
        base.OnModelCreating(modelBuilder);
    }
}
