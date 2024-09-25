namespace Framework.Core.Domain.Shared.Prop;

using System.Collections.Generic;
using Aggregate.ValueObject;

public class Id : ValueObject<Id>
{
    public long Value { get; private set; }

    private Id() { }
    private Id(long value) { Value = value; }

    public static Id Instance(long value) => new(value);

    public static explicit operator long(Id source) => source.Value;
    public static implicit operator Id(long source) => new(source);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}