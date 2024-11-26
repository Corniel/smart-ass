using System.Numerics;

namespace SmartAss.Numerics;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public readonly struct NumericRanges<TNumber> : IReadOnlyList<NumericRange<TNumber>> where TNumber : struct, INumber<TNumber>
{
    public static readonly NumericRanges<TNumber> Empty;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IReadOnlyList<NumericRange<TNumber>> collection;

    internal NumericRanges(IReadOnlyList<NumericRange<TNumber>> ranges) => collection = ranges;

    public NumericRange<TNumber> this[int index] => collection[index];

    public int Count => collection?.Count ?? 0;

    public TNumber Size
    {
        get
        {
            var sum = TNumber.Zero;
            foreach (var range in this)
            {
                sum += range.Size;
            }
            return sum;
        }
    }

    [Pure]
    public NumericRanges<TNumber> Merge(IEnumerable<NumericRange<TNumber>> other)
    {
        Guard.NotNull(other, nameof(other));
        return this.Concat(other).Merge();
    }

    [Pure]
    public NumericRanges<TNumber> Intersection(IEnumerable<NumericRange<TNumber>> other)
    {
        var list = new List<NumericRange<TNumber>>();

        foreach (var range in other)
        {
            foreach (var r in collection)
            {
                var inter = r.Intersection(range);
                if (!inter.IsEmpty)
                {
                    list.Add(inter);
                }
            }
        }
        return list.Merge();
    }

    [Pure]
    public NumericRanges<TNumber> Except(NumericRange<TNumber> except) => Except([except]);

    [Pure]
    public NumericRanges<TNumber> Except(IEnumerable<NumericRange<TNumber>> other)
    {
        var filtered = this;
        foreach (var except in other)
        {
            filtered = NumericRangeExtensions.Except(filtered, except);
        }
        return filtered.Merge();
    }

    [Pure]
    public override string ToString() => string.Join("; ", collection);

    [Pure]
    public IEnumerator<NumericRange<TNumber>> GetEnumerator() => (collection ?? []).GetEnumerator();

    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    [Pure]
    public static NumericRanges<TNumber> New(NumericRange<TNumber> range) => new([range]);

    [Pure]
    public static NumericRanges<TNumber> New(params NumericRange<TNumber>[] ranges) => New(ranges.AsEnumerable());

    [Pure]
    public static NumericRanges<TNumber> New(IEnumerable<NumericRange<TNumber>> ranges) => new(ranges.Merge());
}
