namespace VOI.DescktopClient.Models.Command;

public sealed record KeywordCreate(string Title, string Description);
public sealed record KeywordChangeStatus(Guid Code, string status);
public sealed record KeywordChangeTitle(Guid Code, string Title);
public sealed record KeywordDelete(Guid Code);
