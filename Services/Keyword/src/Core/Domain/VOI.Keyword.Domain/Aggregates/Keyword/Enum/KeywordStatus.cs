namespace VOI.Keyword.Domain.Aggregates.Keyword.Enum;

using Tool.Shared;

public sealed record KeywordStatus : Enumer
{
    public override string Display
       => Value switch
       {
           "Preview" => "پیش نمایش",
           "Active" => "فعال",
           "Inactive" => "غیر‌فعال",
           _ => "نامشخص",
       };

    private KeywordStatus(string value) : base(value) { }

    public static KeywordStatus New(string value)
        => new(value);

    public static readonly KeywordStatus Unknown = new(String.Empty);
    public static readonly KeywordStatus Preview = new("Preview");
    public static readonly KeywordStatus Active = new("Active");
    public static readonly KeywordStatus Inactive = new("Inactive");

    public static readonly List<KeywordStatus> Items;
    static KeywordStatus()
        => Items = [Unknown, Preview, Active, Active];
}
