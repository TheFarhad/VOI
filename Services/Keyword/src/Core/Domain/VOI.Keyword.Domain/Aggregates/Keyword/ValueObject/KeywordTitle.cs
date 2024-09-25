namespace VOI.Keyword.Domain.Aggregates.Keyword.ValueObject;

using System.Collections.Generic;
using Exception;
using Framework.Core.Domain.Aggregate.ValueObject;

public sealed class KeywordTitle : ValueObject<KeywordTitle>
{
    public string Value { get; }

    private KeywordTitle(string title)
    {
        if (String.IsNullOrWhiteSpace(title))
        {
            throw new KeywordTitleNullException();
        }
        if (title is { Length: (< 2) or (> 100) })
        {
            throw new KeywordTitleCharacterException();
        }
        Value = title;
    }

    public static KeywordTitle New(string title)
        => new(title);

    public static explicit operator string(KeywordTitle source)
        => source.Value;

    public static implicit operator KeywordTitle(string source)
        => new(source);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}
