namespace VOI.Keyword.Contract.Application.Query;

using Framework.Core.Contract.Application.Shared;

public sealed record KeywordSearchByTitleAndStatus
    : PageRequest<KeywordSearchByTitleAndStatusPayload>
{
    public required string Title { get; init; }
    public required string Status { get; init; }
}