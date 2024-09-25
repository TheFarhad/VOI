namespace VOI.DescktopClient.Models.Dto;

using Framework.Core.Contract.Application.Shared;

public sealed record NewsSearchByTitlePayload : RequestOutput<NewsItem>;
public sealed record NewsItem(Guid Code, string Title, string Body);