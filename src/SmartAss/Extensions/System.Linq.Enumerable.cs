using System.Collections.Generic;

namespace System.Linq;

public static class SmartAssEnumerabelExtensions
{
    public static IEnumerable<TSource> WithStep<TSource>(this IEnumerable<TSource> source, int step)
        => source
        .Select((item, index) => new { item, index })
        .Where(pair => pair.index % step == 0)
        .Select(pair => pair.item);
}
