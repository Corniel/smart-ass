using System.ComponentModel;
using System.Numerics;

namespace SmartAss.Numerics;

[TypeConverter(typeof(Conversion.Numerics.NumericRangeTypeConverter))]
public readonly struct NumericRange<TNumber> : IEquatable<NumericRange<TNumber>>
    
    where TNumber : struct, INumber<TNumber>
{
    public static readonly NumericRange<TNumber> Empty;

    public NumericRange(TNumber number) : this(number, number) { }

    public NumericRange(TNumber lower, TNumber upper)
    {
        if (upper < lower)
        {
            throw new ArgumentOutOfRangeException(nameof(upper), "Upper bound should not be smaller than the lower bound.");
        }
        Lower = lower;
        _Upper = upper + TNumber.One;
    }

    public TNumber Lower { get; }
    
    public TNumber Upper => _Upper - TNumber.One;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly TNumber _Upper;

    public TNumber Size => _Upper - Lower;

    public bool IsEmpty => Equals(Empty);

    [Pure]
    public NumericRange<TNumber> Intersection(NumericRange<TNumber> other)
    {
        var lower = Max(Lower, other.Lower);
        var upper = Min(Upper, other.Upper);
        return lower <= upper ? new(lower, upper) : Empty;
    }

    [Pure]
    public NumericRange<TNumber> Join(NumericRange<TNumber> other)
    {
        var l = this;
        var r = other;
        if (l.Lower > r.Lower) (l, r) = (r, l);

        if (r.Lower - l.Upper <= TNumber.One)
        {
            return new(l.Lower, Max(l.Upper, r.Upper));
        }
        else return Empty;
    }

    [Pure]
    public bool Contains(TNumber number) => Lower <= number && number <= Upper;

    [Pure]
    public bool FullyContains(NumericRange<TNumber> other) => Intersection(other) == other;

    [Pure]
    public bool Overlaps(NumericRange<TNumber> other) => !(this & other).IsEmpty;

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is NumericRange<TNumber> other && Equals(other);

    /// <inheritdoc />
    public bool Equals(NumericRange<TNumber> other) => Lower == other.Lower && Upper == other.Upper;

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Lower, Upper);

    /// <summary>Compares two Ranges.</summary>
    public static bool operator ==(NumericRange<TNumber> a, NumericRange<TNumber> b) => a.Equals(b);

    /// <summary>Compares two Ranges.</summary>
    public static bool operator !=(NumericRange<TNumber> a, NumericRange<TNumber> b) => !(a == b);

    public static NumericRange<TNumber> operator &(NumericRange<TNumber> left, NumericRange<TNumber> right) => left.Intersection(right);

    [Pure]
    public override string ToString() => IsEmpty ? "{}" :  $"{{{Lower}..{Upper}}}";

    [Pure]
    public static NumericRange<TNumber> Parse(string str)
    {
        var ns = str.Split("..");
        if(ns.Length != 2) ns = str.Split('-');
        return new(TNumber.Parse(ns[0], null), TNumber.Parse(ns[1], null));
    }

    static TNumber Min(TNumber l, TNumber r) => l < r ? l : r;
    static TNumber Max(TNumber l, TNumber r) => l > r ? l : r;
}
