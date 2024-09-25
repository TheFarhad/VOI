namespace Framework.Core.Contract.Application.Shared;

public interface IRequest { }
public interface IRequest<out TOutput> : IRequest
{ }

public abstract record PageRequest<TOutput> : IRequest<TOutput>
{
    public int Page { get; init; } = 1;
    public int Size { get; init; } = 10;
    public int Skip => (Page - 1) * Size;
    public bool SortAscending { get; init; } = true;
    public string SortBy { get; init; } = "Id";
    public bool NeededTotalCount { get; init; }
}

public abstract record RequestOutput<T> where T : class
{
    public IReadOnlyCollection<T>? Items { get; init; } = [];
    public int? Total { get; init; } = default!;
}