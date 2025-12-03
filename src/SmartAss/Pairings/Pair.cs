using System.ComponentModel;

namespace SmartAss.Pairings;

public static class Pair
{
    [Pure]
    public static Pair<T> New<T>(T first, T second) => new(first, second);

    [Pure]
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

    /// <summary>Deconstructs the pair.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out T first, out T second)
    {
        first = First;
        second = Second;
    }

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is Pair<T> other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(Pair<T> other) => EqualityComparer<T>.Default.Equals(First, other.First);

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => HashCode.Combine(First, Second);
}
