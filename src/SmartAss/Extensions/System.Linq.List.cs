using SmartAss.Pairings;

namespace System.Linq;

public static class SmartAssListExtensions
{
    [Pure]
    public static IEnumerable<Pair<T>> RoundRobin<T>(this IReadOnlyList<T> list) => Pair.RoundRobin(list);
}
