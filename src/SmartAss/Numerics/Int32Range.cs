using SmartAss.Parsing;
using System.ComponentModel;

namespace SmartAss.Numerics;

[TypeConverter(typeof(Conversion.Numerics.Int32RangeTypeConverter))]
public readonly struct Int32Range : IEquatable<Int32Range>
{
    public static readonly Int32Range Empty;

    public Int32Range(int number) : this(number, number) { }

    public Int32Range(int lower, int upper)
    {
        if (upper < lower)
        {
            throw new ArgumentOutOfRangeException(nameof(upper), "Upper bound should not be smaller than the lower bound.");
        }
        Lower = lower;
        _Upper = upper + 1;
    }

    public int Lower { get; }
    public int Upper => _Upper - 1;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int _Upper;

    public int Size => _Upper - Lower;

    public bool IsEmpty => Equals(Empty);

    [Pure]
    public Int32Range Intersection(Int32Range other)
    {
        var lower = Math.Max(Lower, other.Lower);
        var upper = Math.Min(Upper, other.Upper);
        return lower <= upper ? new(lower, upper) : Empty;
    }

    [Pure]
    public Int32Range Join(Int32Range other)
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
    public bool Contains(int number) => Lower <= number && number <= Upper;

    [Pure]
    public bool FullyContains(Int32Range other) => Intersection(other) == other;

    [Pure]
    public bool Overlaps(Int32Range other) => !(this & other).IsEmpty;

    [Pure]
    public IEnumerable<int> Values() => Enumerable.Range(Lower, Size);

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is Int32Range other && Equals(other);

    /// <inheritdoc />
    public bool Equals(Int32Range other) => Lower == other.Lower && Upper == other.Upper;

    /// <inheritdoc />
    public override int GetHashCode() => Lower ^ (Upper << 16);

    /// <summary>Compares two Ranges.</summary>
    public static bool operator ==(Int32Range a, Int32Range b) => a.Equals(b);

    /// <summary>Compares two Ranges.</summary>
    public static bool operator !=(Int32Range a, Int32Range b) => !(a == b);

    public static Int32Range operator &(Int32Range left, Int32Range right) => left.Intersection(right);

    [Pure]
    public override string ToString() => IsEmpty ? "{}" : $"{{{Lower}..{Upper}}}";

    [Pure]
    public static Int32Range Parse(string str)
    {
        var ns = str.Split('-').Int32s().ToArray();
        return new(ns[0], ns[1]);
    }
}
