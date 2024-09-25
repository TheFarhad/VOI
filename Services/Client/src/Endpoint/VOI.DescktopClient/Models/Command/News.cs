namespace VOI.DescktopClient.Models.Command;

public sealed record NewsCreate(string Title, string Description, string Body, List<Guid> Keywords);

public sealed record NewsEdit(Guid Code, string Title, string Description, string Body, List<Guid> Keywords);

public sealed record NewsDelete(Guid Code);