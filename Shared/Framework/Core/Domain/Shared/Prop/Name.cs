namespace Framework.Core.Domain.Shared.Prop;

public class Name : _String
{
    private Name() { }
    protected Name(string value) : base(value, 2, 60) { }

    public static Name Instance(string value) => new(value);

    public static explicit operator string(Name source) => source.Value;
    public static implicit operator Name(string value) => new(value);
}
