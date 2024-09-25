namespace Framework.Core.Domain.Shared.Prop;

using System.Collections.Generic;
using Aggregate.Exception;
using Aggregate.ValueObject;

public class Priority : ValueObject<Priority>
{
    public int Value { get; private set; }

    protected Priority() { }
    protected Priority(int value) => Init(value, () => { });

    private void Init(int value, Action? action = default)
    {
        if (value < 1) throw new InvalidPropException("");
        Value = value;
    }

    public static Priority Instance(int value) => new(value);

    public Priority Increase(int value) => new(Value + value);

    public Priority Decrease(int value) => new(Value - value);

    public static Priority operator +(Priority source, int value) => new(source.Value + value);

    public static Priority operator -(Priority source, int value) => new(source.Value - value);

    public static bool operator <(Priority left, Priority right) => left.Value < right.Value;

    public static bool operator <=(Priority left, Priority right) => left.Value <= right.Value;

    public static bool operator >(Priority left, Priority right) => left.Value > right.Value;

    public static bool operator >=(Priority left, Priority right) => left.Value >= right.Value;

    public static explicit operator int(Priority source) => source.Value;

    public static implicit operator Priority(int value) => new(value);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}
