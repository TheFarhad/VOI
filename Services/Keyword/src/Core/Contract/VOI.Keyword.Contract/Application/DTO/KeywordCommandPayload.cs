namespace VOI.Keyword.Contract.Application.DTO;

public sealed record KeywordCreatePayload(int Id);
public sealed record KeywordChangeTitlePayload(int Id);
public sealed record KeywordChangeStatusPayload(int Id);
public sealed record KeywordDeletePayload(int Id);