using SmartAss.Pairings;

namespace System.Linq;

public static class SmartAssListExtensions
{
    [OverloadResolutionPriority(-1)]
    [Pure]
    public static IEnumerable<Pair<T>> RoundRobin<T>(this IEnumerable<T> items) => items.ToArray().RoundRobin();

    [OverloadResolutionPriority(1)]
    [Pure]
    public static IEnumerable<Pair<T>> RoundRobin<T>(this IReadOnlyList<T> list) => Pair.RoundRobin(list);
}
