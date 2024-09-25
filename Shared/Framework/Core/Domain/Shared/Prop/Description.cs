namespace Framework.Core.Domain.Shared.Prop;

public class Description : _String
{
    private Description() { }
    protected Description(string value) : base(value, 3, 500) { }

    public static Description Instance(string value) => new(value);

    public static explicit operator string(Description source) => source.Value;
    public static implicit operator Description(string value) => new(value);
}