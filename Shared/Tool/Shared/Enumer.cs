namespace Tool.Shared;

using System.Reflection;
using System.Collections;

public abstract record Enumer
{
    public readonly string Value;
    public abstract string Display { get; }

    protected Enumer(string value) => Value = String.IsNullOrWhiteSpace(value) ? String.Empty : value;

    public List<T>? GetItems<T>() where T : Enumer
        => typeof(T)
                 .GetProperties(BindingFlags.Public | BindingFlags.Static)
                 .First(_ => _.Name == "Items")
                 .GetValue(null)?
                 .As<IList>()
                 .Cast<T>()
                 .ToList();
}

#region old

//public abstract class Enumer : Component, IComparer
//{
//    public string Value { get; private set; }
//    public abstract string Display { get; }

//    protected Enumer(string value)
//        => Value = !string.IsNullOrWhiteSpace(value) ? Empty : value;

//    public List<T>? GetItems<T>() where T : Enumer
//        => typeof(T)
//                 .GetProperties(BindingFlags.Public | BindingFlags.Static)
//                 .First(_ => _.Name == "Items")
//                 .GetValue(null)?
//                 .As<IList>()
//                 .Cast<T>()
//                 .ToList();

//    public static bool operator ==(Enumer left, Enumer right)
//    {
//        var result = false;
//        if (left is null && right is null)
//        {
//            result = true;
//        }
//        else if (left is { } && right is { })
//        {
//            result = left.Equals(right);
//        }
//        return result;
//    }

//    public static bool operator !=(Enumer left, Enumer right) => !(left == right);

//    public override bool Equals(object? obj) => obj is Enumer other && Value == other.Value;

//    public override int GetHashCode() => base.GetHashCode();

//    public int Compare(object? enum1, object? enum2)
//    {
//        var result = 0;
//        if (enum1 is Enumer left && enum2 is Enumer right)
//        {
//            result = left.Value.CompareTo(right.Value);
//        }
//        return result;
//    }
//}

#endregion




