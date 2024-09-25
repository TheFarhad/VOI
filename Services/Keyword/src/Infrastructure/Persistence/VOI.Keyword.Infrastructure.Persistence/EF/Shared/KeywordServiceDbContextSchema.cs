namespace VOI.Keyword.Infrastructure.Persistence.EF.Shared;

public static class KeywordServiceDbContextSchema
{
    public const string DefaultSchema = "Voi_Keyword";
    public const string DefaultConnectionString = "KeywordServiceDb";

    public static class KeywordTableSchema
    {
        public const string TableName = "Keywords";
    }

    public static class CategoryTableSchema
    {
        public const string TableName = "Categories";
    }
}
