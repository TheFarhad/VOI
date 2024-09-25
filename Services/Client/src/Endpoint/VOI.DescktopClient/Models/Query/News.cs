namespace VOI.DescktopClient.Models.Query;

public sealed record NewsSearchByTitle
{
    public int Page { get; init; } = 1;
    public int Size { get; init; } = 10;
    public int Skip => (Page - 1) * Size;
    public bool SortAscending { get; init; } = true;
    public string SortBy { get; init; } = "Id";
    public bool NeededTotalCount { get; init; }
    public required string Title { get; init; }
}