namespace Framework.Infra.Persistence.Shared;

using Microsoft.EntityFrameworkCore;

public abstract class EFDbContext : DbContext
{
    protected EFDbContext() { }
    public EFDbContext(DbContextOptions options) : base(options) { }
}