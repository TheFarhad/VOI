namespace VOI.DescktopClient.Models.Dto;

using Framework.Core.Contract.Application.Shared;

public sealed record KeywordSearchByTitleAndStatusPayload
    : RequestOutput<KeywordItem>;

public sealed record KeywordItem(int Id, Guid Code, string Title, string Status);