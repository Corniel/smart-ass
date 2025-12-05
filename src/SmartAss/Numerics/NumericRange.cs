using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace SmartAss.Numerics;

[TypeConverter(typeof(Conversion.Numerics.NumericRangeTypeConverter))]
public readonly struct NumericRange<TNumber> : IEquatable<NumericRange<TNumber>>, IEnumerable<TNumber>

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

    /// <inheritdoc cref="IReadOnlySet{T}.Contains(T)" />
    [Pure]
    public bool Contains(TNumber number) => Lower <= number && number <= Upper;

    [Pure]
    public bool FullyContains(NumericRange<TNumber> other) => Intersection(other) == other;

    [Pure]
    public bool Overlaps(NumericRange<TNumber> other) => !(this & other).IsEmpty;

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is NumericRange<TNumber> other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(NumericRange<TNumber> other) => Lower == other.Lower && Upper == other.Upper;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => HashCode.Combine(Lower, Upper);

    /// <inheritdoc cref="IEnumerable.GetEnumerator()" />
    [Pure]
    public Iterator GetEnumerator() => new(Lower, Upper);

    /// <inheritdoc />
    [Pure]
    IEnumerator<TNumber> IEnumerable<TNumber>.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Compares two Ranges.</summary>
    public static bool operator ==(NumericRange<TNumber> a, NumericRange<TNumber> b) => a.Equals(b);

    /// <summary>Compares two Ranges.</summary>
    public static bool operator !=(NumericRange<TNumber> a, NumericRange<TNumber> b) => !(a == b);

    public static NumericRange<TNumber> operator &(NumericRange<TNumber> left, NumericRange<TNumber> right) => left.Intersection(right);

    public static implicit operator NumericRange<TNumber>(Range range) => New(range);

    [Pure]
    public override string ToString() => IsEmpty ? "{}" : $"{{{Lower}..{Upper}}}";

    [Pure]
    public static NumericRange<TNumber> New(Range range) => new
    (
        TNumber.CreateChecked(range.Start.Value),
        TNumber.CreateChecked(range.End.Value)
    );

    [Pure]
    public static NumericRange<TNumber> Parse(string str)
    {
        var ns = str.Split("..");
        if (ns.Length != 2) ns = str.Split('-');
        return new(TNumber.Parse(ns[0], null), TNumber.Parse(ns[1], null));
    }

    [Pure]
    private static TNumber Min(TNumber l, TNumber r) => l < r ? l : r;

    [Pure]
    private static TNumber Max(TNumber l, TNumber r) => l > r ? l : r;

    public struct Iterator(TNumber lower, TNumber upper) : IEnumerator<TNumber>
    {
        private readonly TNumber Upper = upper;

        /// <inheritdoc />
        public TNumber Current { get; private set; } = lower - TNumber.One;

        /// <inheritdoc />
        readonly object IEnumerator.Current => Current;

        /// <inheritdoc />
        [Impure]
        public bool MoveNext() => ++Current <= Upper;

        /// <inheritdoc />
        public readonly void Dispose() { /* Nothing to dispose. */ }

        /// <inheritdoc />
        [DoesNotReturn]
        public void Reset() => throw new NotSupportedException();
    }
}
