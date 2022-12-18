namespace SmartAss.Numerics
{
    public static class Int32Ranges
    {
        public static IReadOnlyList<Int32Range> Merge(this IEnumerable<Int32Range> ranges)
        {
            Guard.NotNull(ranges, nameof(ranges));

            var list = new List<Int32Range>();

            foreach (var range in ranges.Where(r => !r.IsEmpty()).OrderBy(r => r.Lower))
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
    }
}
