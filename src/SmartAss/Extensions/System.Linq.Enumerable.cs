using SmartAss;

namespace System.Linq;

public static class SmartAssEnumerabelExtensions
{
    public static bool AllDistinct<TSource>(this IEnumerable<TSource> source)
    {
        var @set = new HashSet<TSource>();
        return source.All(@set.Add);
    }
    public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> sources, TSource except)
        => sources.Where(s => !s.Equals(except));

    public static IEnumerable<TSource> WithStep<TSource>(this IEnumerable<TSource> source, int step)
        => source
        .Select((item, index) => new { item, index })
        .Where(pair => pair.index % step == 0)
        .Select(pair => pair.item);

    /// <remarks>Only returns full chunks.</remarks>
    public static IEnumerable<IReadOnlyList<TSource>> ChunkBy<TSource>(this IEnumerable<TSource> source, int groupSize)
    {
        Guard.NotNull(source, nameof(source));

        var group = new TSource[groupSize];
        var index = 0;
        foreach (var item in source)
        {
            group[index++] = item;
            if (index == groupSize)
            {
                yield return group;
                index = 0;
                group = new TSource[groupSize];
            }
        }
    }

    public static TValue Sum<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, TValue> selector)
        where TValue : struct, Numerics.IAdditionOperators<TValue, TValue, TValue>
    {
        TValue sum = default;
        foreach (var item in source.Select(selector))
        {
            sum += item;
        }
        return sum;
    }
}
