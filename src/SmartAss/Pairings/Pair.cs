
namespace SmartAss.Pairings;

public static class Pair
{
    public static Pair<T> New<T>(T first, T second) => new(first, second);

    public static IEnumerable<Pair<T>> RoundRobin<T>(IReadOnlyList<T> list)
    {
        if (list is null) { yield break; }

        for (var f = 0; f < list.Count; f++)
        {
            for (var s = f + 1; s < list.Count; s++)
            {
                yield return New(list[f], list[s]);
            }
        }
    }
}

[DebuggerDisplay("First = {First}, Second = {Second}")]
public readonly struct Pair<T>(T first, T second) : IEquatable<Pair<T>>
{
    public readonly T First = first;
    public readonly T Second = second;

    /// <inheritdoc />
    public override bool Equals(object? obj)=> obj is Pair<T> other && Equals(other);

    /// <inheritdoc />
    public bool Equals(Pair<T> other) => EqualityComparer<T>.Default.Equals(First, other.First);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(First, Second);
}
