namespace VOI.DescktopClient.Models.Query;

using Framework.Core.Application.Shared;

public record Response<TPayload>
{
    public TPayload? Payload { get; init; } = default!;
    public List<Error> Errors { get; init; } = [];
    public bool HasError => Errors.Count > 0;
}

public record Error
{
    public ErrorType Type { get; init; } = ErrorType.Unknown;
    public string Message { get; init; } = default!;
    public string Description { get; init; } = default!;
    public int Status { get; init; } = default!;
}
