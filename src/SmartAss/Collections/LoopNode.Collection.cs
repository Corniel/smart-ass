using System.Diagnostics.CodeAnalysis;

namespace SmartAss.Collections;

public partial class LoopNode<T>
{
    /// <inheritdoc />
    bool ICollection.IsSynchronized => false;

    /// <inheritdoc />
    object ICollection.SyncRoot => false;

    /// <inheritdoc />
    public void CopyTo(Array array, int index) => ToArray().CopyTo(array, index);

    /// <summary>Bypasses the specified number of nodes in the loop.</summary>
    [Pure]
    public LoopNode<T> Skip(long count)
    {
        var skip = count.Mod(Loop.Count);

        if (skip == 0) { return this; }
        else if (skip * 2 > Loop.Count)
        {
            skip -= Loop.Count; // Move the shortest way.
        }

        if (skip > 0)
        {
            var next = Next;
            while (--skip > 0) { next = next.Next; }
            return next;
        }
        else
        {
            var prev = Prev;
            while (++skip < 0) { prev = prev.Prev; }
            return prev;
        }
    }

    /// <summary>Creates an array of the nodes in the loop.</summary>
    [Pure]
    public LoopNode<T>[] ToArray() => GetEnumerator().Take(Count).ToArray();

    /// <summary>Creates an list of the nodes in the loop.</summary>
    [Pure]
    public List<LoopNode<T>> ToList() => GetEnumerator().Take(Count).ToList();

    /// <summary>Enumerates through the loop (indefinitely).</summary>
    [Pure]
    public Enumerator GetEnumerator() => new(this);

    /// <inheritdoc />
    [ExcludeFromCodeCoverage(Justification = "Equal to public GetEnumerator()")]
    [Pure]
    IEnumerator<LoopNode<T>> IEnumerable<LoopNode<T>>.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    [ExcludeFromCodeCoverage(Justification = "Equal to public GetEnumerator()")]
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public struct Enumerator : Iterator<LoopNode<T>>
    {
        private readonly bool IsLoop;

        internal Enumerator(LoopNode<T> current)
        {
            Current = current.Prev ?? current;
            IsLoop = !current.IsDetached;
        }

        /// <inheritdoc />
        public LoopNode<T> Current { get; private set; }

        /// <inheritdoc />
        [Impure]
        public bool MoveNext()
        {
            Current = Current.Next;
            return IsLoop;
        }

        /// <inheritdoc />
        public void Dispose() { /* Do nothing */ }

        /// <inheritdoc />
        public void Reset() => throw new NotSupportedException();
    }
}
