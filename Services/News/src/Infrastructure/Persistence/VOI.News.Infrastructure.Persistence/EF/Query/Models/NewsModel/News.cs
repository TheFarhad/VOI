namespace VOI.News.Infrastructure.Persistence.EF.Query.Models.NewsModel;

[Table("News")]
internal sealed record News
{
    [Key]
    public long Id { get; set; }
    public Guid Code { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }

    public IReadOnlyCollection<Keyword> Keywords { get; set; } = [];
}

[Table("Keywords")]
internal sealed record Keyword
{
    public long Id { get; set; }

    [ForeignKey(nameof(Detail))]
    public Guid Code { get; set; }

    [ForeignKey(nameof(News))]
    public long NewsId { get; set; }

    public News News { get; set; }
    public KeywordDetail Detail { get; set; }
}

[Table("KeywordDetails")]
internal sealed record KeywordDetail
{
    [Key]
    public Guid Code { get; set; }
    public string Title { get; set; }
}