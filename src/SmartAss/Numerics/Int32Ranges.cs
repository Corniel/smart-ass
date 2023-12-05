namespace SmartAss.Numerics
{
    public static class Int32Ranges
    {
        public static IReadOnlyList<Int32Range> Merge(this IEnumerable<Int32Range> ranges)
        {
            Guard.NotNull(ranges, nameof(ranges));

            var list = new List<Int32Range>();

            foreach (var range in ranges.Where(r => !r.IsEmpty).OrderBy(r => r.Lower))
            {
                var add = true;
                for (var i = 0; i < list.Count && add; i++)
                {
                    if (list[i].Join(range) is { } join)
                    {
                        list[i] = join;
                        add = false;
                    }
                }
                if (add) { list.Add(range); }
            }

            return list;
        }

        [Pure]
        public static List<Int32Range> Except(this IEnumerable<Int32Range> ranges, Int32Range except)
        {
            Guard.NotNull(ranges, nameof(ranges));

            var list = new List<Int32Range>();

            foreach (var range in ranges.Where(r => !r.IsEmpty))
            {
                if (range.Intersection(except) is { IsEmpty: false } select)
                {
                    if (range.Lower < select.Lower)
                    {
                        list.Add(new(range.Lower, select.Lower - 1));
                    }
                    if (range.Upper >= select.Upper && select.Upper < range.Upper)
                    {
                        list.Add(new(select.Upper + 1, range.Upper));
                    }
                }
                else
                {
                    list.Add(range);
                }
            }

            return list;
        }
    }
}
