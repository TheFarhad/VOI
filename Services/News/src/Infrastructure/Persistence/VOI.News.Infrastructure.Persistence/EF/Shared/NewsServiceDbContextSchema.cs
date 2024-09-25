namespace VOI.News.Infrastructure.Persistence.EF.Shared;

public static class NewsServiceDbContextSchema
{
    public const string DefaultSchema = "Voi_News";
    public const string DefaultConnectionString = "NewsServiceDb";

    public static class NewsTableSchema
    {
        public const string TableName = "News";
        public const string KeywordTableName = "Keywords";
        public const string KeywordColumnName = "Code";
        public const string NewsKeywordColumnBackField = "_keywords";
    }
}
