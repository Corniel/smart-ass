using SmartAss;
using System.Collections.Generic;

namespace System.Linq;

public static class SmartAssEnumerabelExtensions
{
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
            if(index == groupSize)
            {
                yield return group;
                index = 0;
                group = new TSource[groupSize];
            }
        }
    }
}
