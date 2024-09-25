namespace Framework.Core.Domain.Shared.Prop;

using System.Collections.Generic;
using Tool.Shared;
using Aggregate.Exception;
using Aggregate.ValueObject;

public class Register : ValueObject<Register>
{
    public DateTime Value { get; private set; }

    protected Register() { }
    protected Register(DateTime value) => Init(value, () => { });

    private void Init(DateTime value, Action? action = default)
    {
        value.IsNull(() =>
        {
            throw new InvalidPropException("");
        },
        () =>
        {
            Value = value;
        });
    }

    public static Register Instance(DateTime value) => new(value);

    public Register AddDays(int value) => new(Value.AddDays(value));

    public Register AddMonths(int value) => new(Value.AddMonths(value));

    public Register AddYears(int value) => new(Value.AddYears(value));

    public static bool operator <(Register left, Register right) => left.Value < right.Value;

    public static bool operator <=(Register left, Register right) => left.Value <= right.Value;

    public static bool operator >(Register left, Register right) => left.Value > right.Value;

    public static bool operator >=(Register left, Register right) => left.Value >= right.Value;

    public static explicit operator DateTime(Register description) => description.Value;

    public static implicit operator Register(DateTime value) => new(value);

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}
