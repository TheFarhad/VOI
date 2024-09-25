namespace VOI.Keyword.Domain.Aggregates.Keyword.ValueObject;

using System.Collections.Generic;
using Exception;
using Framework.Core.Domain.Aggregate.ValueObject;

public sealed class KeywordDescription : ValueObject<KeywordDescription>
{
    public string Value { get; }

    private KeywordDescription(string title)
    {
        if (String.IsNullOrWhiteSpace(title))
        {
            throw new KeywordDescriptionNullException();
        }
        if (title is { Length: (< 10) or (> 500) })
        {
            throw new KeywordDescriptionCharacterException();
        }
        Value = title;
    }

    public static KeywordDescription New(string title)
       => new(title);

    public static explicit operator string(KeywordDescription source)
        => source.Value;
    public static implicit operator KeywordDescription(string source)
        => new(source);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}
