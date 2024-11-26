using SmartAss.Diagnostics;

namespace SmartAss.Collections;

[DebuggerTypeProxy(typeof(CollectionDebugView))]
[DebuggerDisplay("Count = {Count}")]
public readonly struct Slice<T> : IReadOnlyList<T>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IReadOnlyList<T> Collection;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int Offset;

    public Slice(IReadOnlyList<T> collection) : this(collection, 0, collection.Count) { }

    public Slice(IReadOnlyList<T> collection, int offset, int count)
    {
        Collection = collection;
        Offset = offset;
        Count = count;
    }

    public T this[int index] => Collection[Offset + index];

    public Slice<T> this[Range range] => new(Collection, Offset + Slice.Offset(range, Count), Slice.Count(range, Count));

    public int Count { get; }

    [Obsolete("Use [.. this] instead")]
    [Pure]
    public T[] ToArray() => [.. this];

    /// <inheritdoc />>
    [Pure]
    public IEnumerator<T> GetEnumerator() => Collection.Skip(Offset).Take(Count).GetEnumerator();

    /// <inheritdoc />>
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

file static class Slice
{
    [Pure]
    public static int Offset(Range range, int size)
        => range.Start.IsFromEnd
        ? size - range.Start.Value
        : range.Start.Value;

    [Pure]
    public static int Count(Range range, int size)
        => (range.End.IsFromEnd
        ? size - range.End.Value
        : range.End.Value)
        - Offset(range, size);
}
