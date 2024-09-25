namespace Framework.Core.Domain.Shared.Prop;

using System.Collections.Generic;
using Aggregate.ValueObject;

public class NationalCode : ValueObject<NationalCode>
{
    public string Value { get; private set; }

    protected NationalCode() { }
    protected NationalCode(string value) => Init(value, () => { });

    private void Init(string value, Action? action = default)
    {
        //if (!value.IsNationalCode())
        //    throw new InvalidValueObjectStateException("ValidationErrorStringFormat", nameof(NationalCode));
        Value = value;
    }

    public static NationalCode Instance(string value) => new(value);

    public override string ToString() => Value;

    public static explicit operator string(NationalCode source) => source.Value;

    public static implicit operator NationalCode(string value) => new(value);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}
