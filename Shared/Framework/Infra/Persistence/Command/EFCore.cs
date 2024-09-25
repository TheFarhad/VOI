namespace Framework.Infra.Persistence.Command;

using Shared;
using Core.Domain.Shared.Prop;
using Core.Domain.Aggregate.Entity;

public abstract class EFCommandDbContext<TCommandDbContext> : EFDbContext
    where TCommandDbContext : EFCommandDbContext<TCommandDbContext>
{
    protected EFCommandDbContext() : base() { }
    public EFCommandDbContext(DbContextOptions<TCommandDbContext> options)
        : base(options) { }

    protected override void ConfigureConventions(ModelConfigurationBuilder cb)
    {
        base.ConfigureConventions(cb);

        cb.Properties<Code>().HaveConversion<CodeConverter>();
        cb.Properties<string>().AreUnicode(false).HaveMaxLength(300);
        cb.Properties<Enumer>().AreUnicode(false).HaveMaxLength(100);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .InitializeEntities()
            .IgnoreVersionInAggregateRoots();
    }

    public IEnumerable<string> RelationsGraph(Type clrType)
    {
        var entityType = Model.FindEntityType(clrType);
        var includedNavigations = new HashSet<INavigation>();
        var stack = new Stack<IEnumerator<INavigation>>();
        while (true)
        {
            var navigations = new List<INavigation>();
            var entityNavigations = entityType.GetNavigations();
            foreach (var item in entityNavigations)
            {
                if (includedNavigations.Add(item))
                    navigations.Add(item);
            }
            if (navigations.Count == 0)
            {
                if (stack.Count > 0)
                    yield return string.Join(".", stack.Reverse().Select(e => e.Current.Name));
            }
            else
            {
                foreach (var navigation in navigations)
                {
                    var inverseNavigation = navigation.Inverse;
                    if (inverseNavigation is { })
                        includedNavigations.Add(inverseNavigation);
                }
                stack.Push(navigations.GetEnumerator());
            }
            while (stack.Count > 0 && !stack.Peek().MoveNext())
                stack.Pop();
            if (stack.Count is 0) break;
            entityType = stack.Peek().Current.TargetEntityType;
        }
    }
}

public abstract class EfCommandRepository<TAggragateRoot, TId, TCommandDbContext>
    where TAggragateRoot : AggregateRoot<TId>
    where TId : Identity
    where TCommandDbContext : EFCommandDbContext<TCommandDbContext>
{
    private readonly TCommandDbContext Context;
    protected readonly DbSet<TAggragateRoot> Set;

    public EfCommandRepository(TCommandDbContext context)
    {
        Context = context;
        Set = Context.Set<TAggragateRoot>();
    }

    public void Add(TAggragateRoot source)
        => Set
           .Add(source);

    public async ValueTask<EntityEntry<TAggragateRoot>> AddAsync(TAggragateRoot source, CancellationToken token = default!)
        => await Set.AddAsync(source, token);

    public async Task<bool> AnyAsync(Expression<Func<TAggragateRoot, bool>> predicate, CancellationToken token = default!)
        => await Set.AnyAsync(predicate, token);

    public void BulkAdd(IEnumerable<TAggragateRoot> source)
        => Set.AddRange(source);

    public async Task BulkAddAsync(IEnumerable<TAggragateRoot> source, CancellationToken token = default!)
        => await Set.AddRangeAsync(source, token);

    public async ValueTask<TAggragateRoot?> FindByIdAsync(TId id, CancellationToken token = default!)
        => await Set.FindAsync(id, token);

    public async Task<TAggragateRoot?> FirstOrDefaultAsync(Expression<Func<TAggragateRoot, bool>> predicate, CancellationToken token = default!)
        => await Set.FirstOrDefaultAsync(predicate, token);

    // To bulk insert and update
    public async Task<List<TAggragateRoot>?> ListAsync(Expression<Func<TAggragateRoot, bool>> predicate = default!, CancellationToken token = default!) =>
        predicate is not null ? await Set.Where(predicate).ToListAsync(token)
        : await Set.ToListAsync(token);

    public async Task<TAggragateRoot?> GetGraphAsync(Expression<Func<TAggragateRoot, bool>> predicate, CancellationToken token = default!)
        => await IncludeGraph().FirstOrDefaultAsync(predicate, token);

    public void Remove(TAggragateRoot source)
        => Set.Remove(source);

    public async ValueTask Remove(TId id)
    {
        var entity = await FindByIdAsync(id);
        Remove(entity);
    }

    public async Task RemoveGraph(Id id)
    {
        //var entity = await GetGraphAsync(_ => _.Id == id);
        //Delete(entity);
    }

    #region utilities

    IQueryable<TAggragateRoot> IncludeGraph()
    {
        var result = Set.AsQueryable();
        Context
            .RelationsGraph(typeof(TAggragateRoot))
            .ToList()
            .ForEach(_ =>
            {
                result.Include(_);
            });
        return result;
    }

    #endregion
}

public interface IEFEntityConfig<TEntity, TId> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity<TId> where TId : Identity
{ }