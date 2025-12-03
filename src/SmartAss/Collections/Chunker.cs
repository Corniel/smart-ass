using System.Diagnostics.CodeAnalysis;

namespace SmartAss.Collections;

public struct Chunker<TSource> : IEnumerator<Slice<TSource>>, IEnumerable<Slice<TSource>>
{
    public Chunker(IReadOnlyList<TSource> source, int groupSize)
    {
        Source = source;
        GroupSize = groupSize;
        Offset = -groupSize;
    }

    private readonly IReadOnlyList<TSource> Source;
    private readonly int GroupSize;
    private int Offset;

    /// <inheritdoc />
    public readonly Slice<TSource> Current => new(Source, Offset, GroupSize);

    /// <inheritdoc />
    readonly object? IEnumerator.Current => Current;

    /// <inheritdoc />
    [Impure]
    public bool MoveNext()
    {
        Offset += GroupSize;
        return Offset + GroupSize <= Source.Count;
    }

    /// <inheritdoc />
    [Pure]
    public readonly IEnumerator<Slice<TSource>> GetEnumerator() => this;

    /// <inheritdoc />
    [Pure]
    readonly IEnumerator IEnumerable.GetEnumerator() => this;

    /// <inheritdoc />
    public readonly void Dispose() { /* Nothing to dispose. */ }

    /// <inheritdoc />
    [DoesNotReturn]
    public void Reset() => throw new NotSupportedException();
}
