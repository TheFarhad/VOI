namespace VOI.Keyword.Domain.Aggregates.Category.ValueObject;

using System.Collections.Generic;
using Exception;
using Framework.Core.Domain.Aggregate.ValueObject;

public sealed class CategoryDescription : ValueObject<CategoryDescription>
{
    public string Value { get; }

    private CategoryDescription(string title)
    {
        if (String.IsNullOrWhiteSpace(title))
        {
            throw new CategoryDescriptionNullException();
        }
        if (title is { Length: (< 20) or (> 500) })
        {
            throw new CategoryDescriptionCharacterException();
        }
        Value = title;
    }

    public static CategoryDescription New(string title)
        => new(title);

    public static explicit operator string(CategoryDescription source) => source.Value;
    public static implicit operator CategoryDescription(string source) => new(source);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}
