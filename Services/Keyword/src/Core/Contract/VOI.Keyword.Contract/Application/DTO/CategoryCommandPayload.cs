namespace VOI.Keyword.Contract.Application.DTO;

public sealed record CategoryCreatePayload(int Id);
public sealed record CategoryChangeTitleAndDescriptionPayload(int Id);
public sealed record CategoryDeletePayload(int Id);
