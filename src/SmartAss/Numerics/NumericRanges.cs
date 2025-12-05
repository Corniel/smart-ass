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

    /// <summary>True if any of the ranges contains the specified number.</summary>
    [Pure]
    public bool Contains(TNumber number) => collection.Any(c => c.Contains(number));

    [Pure]
    public NumericRanges<TNumber> Merge(params IEnumerable<NumericRange<TNumber>> other)
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
    public NumericRanges<TNumber> Except(params IEnumerable<NumericRange<TNumber>> other)
    {
        var filtered = this;
        foreach (var except in other)
            filtered = NumericRangeExtensions.Except(filtered, except);

        return filtered.Merge();
    }

    [Pure]
    public override string ToString() => string.Join("; ", collection);

    [Pure]
    public IEnumerable<TNumber> Iterate() => this.SelectMany(r => r);

    [Pure]
    public IEnumerable<TNumber> Where(Func<TNumber, bool> predicate) => this.SelectMany(r => r).Where(predicate);

    [Pure]
    public IEnumerator<NumericRange<TNumber>> GetEnumerator() => (collection ?? []).GetEnumerator();

    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static implicit operator NumericRanges<TNumber>(Range range) => New(range);

    [Pure]
    public static NumericRanges<TNumber> New(params IEnumerable<NumericRange<TNumber>> ranges) => new(ranges.Merge());

    [Pure]
    public static NumericRanges<TNumber> New(params IEnumerable<Range> ranges) => new(ranges.Select(NumericRange<TNumber>.New).Merge());

    [Pure]
    public static NumericRanges<TNumber> Parse(string str) => new(
    [
        .. str.Split(['\r', '\n', ' ', ';', ','], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
              .Select(NumericRange<TNumber>.Parse)
    ]);
}
