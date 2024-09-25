namespace Framework.Core.Domain.Shared.Prop;

using Aggregate.ValueObject;

public sealed record File : ValueObject
{
    public byte[] Value { get; }

    public File(byte[] bytes)
    {
        // do somthing...
        Value = bytes;
    }

    public static File Instance(byte[] bytes) => new(bytes);

    public static explicit operator byte[](File source) => source.Value;
    public static implicit operator File(byte[] value) => new(value);
}

//public class File : ValueObject<File>
//{
//    public byte[] Value { get; private set; }

//    private File() { }
//    protected File(byte[] bytes)
//    {
//        // do somthing...
//        Value = bytes;
//    }

//    public static File Instance(byte[] bytes) => new(bytes);

//    public static explicit operator byte[](File source) => source.Value;
//    public static implicit operator File(byte[] value) => new(value);

//    protected override IEnumerable<object> PropsToEquality()
//    {
//        yield return Value;
//    }
//}


