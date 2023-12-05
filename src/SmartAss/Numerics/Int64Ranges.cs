namespace SmartAss.Numerics
{
    public static class Int64Ranges
    {
        public static IReadOnlyList<Int64Range> Merge(this IEnumerable<Int64Range> ranges)
        {
            Guard.NotNull(ranges, nameof(ranges));

            var list = new List<Int64Range>();

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
        public static List<Int64Range> Except(this IEnumerable<Int64Range> ranges, Int64Range except)
        {
            Guard.NotNull(ranges, nameof(ranges));

            var list = new List<Int64Range>();

            foreach (var range in ranges.Where(r => !r.IsEmpty))
            {
                if (range.Intersection(except) is { IsEmpty: false } section)
                {
                    if (range.Lower < section.Lower)
                    {
                        list.Add(new(range.Lower, section.Lower - 1));
                    }
                    if (range.Upper >= section.Upper && section.Upper < range.Upper)
                    {
                        list.Add(new(section.Upper + 1, range.Upper));
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
