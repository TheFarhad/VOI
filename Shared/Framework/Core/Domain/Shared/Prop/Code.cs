namespace Framework.Core.Domain.Shared.Prop;

public sealed class Code : ValueObject<Code>
{
    public Guid Value { get; }

    private Code(Guid value)
        => Value = value;
    private Code(string value)
    {
        if (value.IsGuid(out Guid result)) Value = result;

        // exception for else
    }

    public static Code New(Guid value)
        => new(value);
    public static Code New(string value)
        => new(value);

    public static explicit operator string(Code source) => source.ToString();
    public static explicit operator Guid(Code source) => source.Value;

    public static implicit operator Code(string source) => new(source);
    public static implicit operator Code(Guid source) => new(source);

    public override string ToString()
        => Value.ToString();

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}