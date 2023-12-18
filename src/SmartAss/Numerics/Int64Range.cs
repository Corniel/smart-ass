using SmartAss.Parsing;
using System.ComponentModel;

namespace SmartAss.Numerics;

[TypeConverter(typeof(Conversion.Numerics.Int64RangeTypeConverter))]
public readonly struct Int64Range : IEquatable<Int64Range>
{
    public static readonly Int64Range Empty;

    public Int64Range(long number) : this(number, number) { }

    public Int64Range(long lower, long upper)
    {
        if (upper < lower)
        {
            throw new ArgumentOutOfRangeException(nameof(upper), "Upper bound should not be smaller than the lower bound.");
        }
        Lower = lower;
        _Upper = upper + 1;
    }

    public long Lower { get; }
    public long Upper => _Upper - 1;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly long _Upper;

    public long Size => _Upper - Lower;

    public bool IsEmpty => Equals(Empty);

    [Pure]
    public Int64Range Intersection(Int64Range other)
    {
        var lower = Math.Max(Lower, other.Lower);
        var upper = Math.Min(Upper, other.Upper);
        return lower <= upper ? new(lower, upper) : Empty;
    }

    [Pure]
    public Int64Range Join(Int64Range other)
    {
        var l = this;
        var r = other;
        if (l.Lower > r.Lower) (l, r) = (r, l);

        if (r.Lower - l.Upper <= 1)
        {
            return new(l.Lower, r.Upper);
        }
        else return Empty;
    }

    [Pure]
    public bool Contains(long number) => Lower <= number && number <= Upper;

    [Pure]
    public bool FullyContains(Int64Range other) => Intersection(other) == other;

    [Pure]
    public bool Overlaps(Int64Range other) => !(this & other).IsEmpty;

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is Int64Range other && Equals(other);

    /// <inheritdoc />
    public bool Equals(Int64Range other) => Lower == other.Lower && Upper == other.Upper;

    /// <inheritdoc />
    public override int GetHashCode() => (Lower ^ (Upper << 24)).GetHashCode();

    /// <summary>Compares two Ranges.</summary>
    public static bool operator ==(Int64Range a, Int64Range b) => a.Equals(b);

    /// <summary>Compares two Ranges.</summary>
    public static bool operator !=(Int64Range a, Int64Range b) => !(a == b);

    public static Int64Range operator &(Int64Range left, Int64Range right) => left.Intersection(right);

    [Pure]
    public override string ToString() => IsEmpty ? "{}" :  $"{{{Lower}..{Upper}}}";

    [Pure]
    public static Int64Range Parse(string str)
    {
        var ns = str.Split('-').Int32s().ToArray();
        return new(ns[0], ns[1]);
    }
}
