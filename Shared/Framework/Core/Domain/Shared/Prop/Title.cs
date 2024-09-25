namespace Framework.Core.Domain.Shared.Prop;

public class Title : _String
{
    private Title() { }
    protected Title(string value) : base(value, 3, 100) { }

    public static Title Instance(string value) => new(value);

    public static explicit operator string(Title source) => source.Value;
    public static implicit operator Title(string value) => new(value);
}