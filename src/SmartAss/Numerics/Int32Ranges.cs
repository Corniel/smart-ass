namespace SmartAss.Numerics
{
    public static class Int32Ranges
    {
        public static IReadOnlyList<Int32Range> Merge(this IEnumerable<Int32Range> ranges)
        {
            Guard.NotNull(ranges, nameof(ranges));

            var list = new List<Int32Range>();

            foreach(var range in ranges.Where(r => !r.IsEmpty()).OrderBy(r => r.Lower))
            {
                var add = true;
                for(var i = 0; i < list.Count && add;i++)
                {
                    if (list[i].Join(range) is { } join)
                    {
                        list[i] = join;
                        add = false;
                    }
                }
                if (add) { list.Add(range); }
            }
            while (Merge(list)) { /* further merge */ }

            return list;
        }

        private static bool Merge(List<Int32Range> ranges)
        {
            for (var i = ranges.Count - 1; i > 0; i--)
            {
                for (var o = 0; o < i; o++)
                {
                    if (ranges[i].Join(ranges[o]) is { } join)
                    {
                        ranges[o] = join;
                        ranges.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
