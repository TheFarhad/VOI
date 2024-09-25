namespace Framework.Core.Domain.Shared.Prop;

using Aggregate.Exception;
using Aggregate.ValueObject;

public class _String : ValueObject<_String>
{
    public string Value { get; private set; }
    private int _min;
    private int _max;

    protected _String() { }
    protected _String(string value, int minLength = 2, int maxLength = 100) => Init(value, minLength, maxLength);

    public static _String Instance(string value) => new(value);

    private void Init(string value, int minLength, int maxLength)
    {
        _min = minLength;
        _max = maxLength;

        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidPropException("the value should not be null or empty");

        if (value.Length < _min || value.Length > _max)
            throw new InvalidPropException("the value should not be less than {0} characters and more than {1} characters", $"{_min}", $"{_max}");

        Value = value;
    }

    public static explicit operator string(_String source) => source.Value;
    public static implicit operator _String(string value) => new(value);

    protected override IEnumerable<object> GetEqualityProperties()
    {
        yield return Value;
    }
}