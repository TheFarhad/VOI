namespace VOI.Keyword.Domain.Aggregates.Category.ValueObject;

using Exception;
using Framework.Core.Domain.Aggregate.ValueObject;

public sealed class CategoryTitle : ValueObject<CategoryTitle>
{
    public string Value { get; }

    private CategoryTitle(string title)
    {
        if (String.IsNullOrWhiteSpace(title))
        {
            throw new CategoryTitleNullException();
        }
        if (title is { Length: (< 2) or (> 100) })
        {
            throw new CategoryTitleCharacterException();
        }
        Value = title;
    }

    public static CategoryTitle New(string title)
       => new(title);

    public static explicit operator string(CategoryTitle source) => source.Value;
    public static implicit operator CategoryTitle(string source) => new(source);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}
