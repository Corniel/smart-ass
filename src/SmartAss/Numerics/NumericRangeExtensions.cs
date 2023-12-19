using System.Numerics;

namespace SmartAss.Numerics;

public static class NumericRangeExtensions
{
    public static NumericRanges<TNumber> Merge<TNumber>(this IEnumerable<NumericRange<TNumber>> ranges)
         where TNumber : struct, INumber<TNumber>
    {
        Guard.NotNull(ranges, nameof(ranges));

        var list = new List<NumericRange<TNumber>>();

        var ordered = ranges is NumericRanges<TNumber> ? ranges : ranges.Where(r => !r.IsEmpty).OrderBy(r => r.Lower);

        foreach (var range in ordered)
        {
            var add = true;
            for (var i = 0; i < list.Count && add; i++)
            {
                if (list[i].Join(range) is {IsEmpty: false } join)
                {
                    list[i] = join;
                    add = false;
                }
            }
            if (add) { list.Add(range); }
        }
        return new(list);
    }

    [Pure]
    public static NumericRanges<TNumber> Except<TNumber>(this IEnumerable<NumericRange<TNumber>> ranges, NumericRange<TNumber> except)
         where TNumber : struct, INumber<TNumber>
    {
        Guard.NotNull(ranges, nameof(ranges));

        var list = new List<NumericRange<TNumber>>();

        var filtered = ranges is NumericRanges<TNumber> ? ranges : ranges.Where(r => !r.IsEmpty);

        foreach (var range in filtered)
        {
            if (range.Intersection(except) is { IsEmpty: false } section)
            {
                if (range.Lower < section.Lower)
                {
                    list.Add(new(range.Lower, section.Lower - TNumber.One));
                }
                if (range.Upper >= section.Upper && section.Upper < range.Upper)
                {
                    list.Add(new(section.Upper + TNumber.One, range.Upper));
                }
            }
            else
            {
                list.Add(range);
            }
        }

        return new(list);
    }
}
