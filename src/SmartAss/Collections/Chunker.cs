using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace SmartAss.Collections;

public struct Chunker<TSource>(IReadOnlyList<TSource> source, int groupSize) : IEnumerator<ImmutableArray<TSource>>, IEnumerable<ImmutableArray<TSource>>
{
    private readonly ImmutableArray<TSource> Source = [.. source];
    private readonly int GroupSize = groupSize;
    private int Offset = -groupSize;

    /// <inheritdoc />
    public readonly ImmutableArray<TSource> Current => Source[Offset..(Offset + GroupSize)];

    /// <inheritdoc />
    readonly object? IEnumerator.Current => Current;

    /// <inheritdoc />
    [Impure]
    public bool MoveNext()
    {
        Offset += GroupSize;
        return Offset + GroupSize <= Source.Length;
    }

    /// <inheritdoc />
    [Pure]
    public readonly IEnumerator<ImmutableArray<TSource>> GetEnumerator() => this;

    /// <inheritdoc />
    [Pure]
    readonly IEnumerator IEnumerable.GetEnumerator() => this;

    /// <inheritdoc />
    public readonly void Dispose() { /* Nothing to dispose. */ }

    /// <inheritdoc />
    [DoesNotReturn]
    public void Reset() => throw new NotSupportedException();
}
