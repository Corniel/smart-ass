using SmartAss.Collections;

namespace SmartAss.Numerics;

public static class Sequence
{
    public static readonly Fractorial Fractorial = new();

    /// <summary>Creates an ad-hoc sequence.</summary>
    /// <typeparam name="T">
    /// The type of the items of the sequence.
    /// </typeparam>
    /// <param name="initial">
    /// The initial (first) value.
    /// </param>
    /// <param name="next">
    /// The function that calculates the next values based on the current one.
    /// </param>
    [Pure]
    public static IEnumerable<T> AdHoc<T>(T initial, Func<T, T> next)
        => new Progressor<T>(initial, next);

    private sealed class Progressor<T>(T initial, Func<T, T> next) : Iterator<T>
    {
        private readonly Func<T, T> Next = next;

        private bool First = true;

        /// <inheritdoc />
        public T Current { get; private set; } = initial;

        /// <inheritdoc />
        [Impure]
        public bool MoveNext()
        {
            if (!First)
            {
                Current = Next(Current);
            }
            else
            {
                First = false;
            }
            return true;
        }

        /// <inheritdoc />
        public void Dispose() { /* Nothing to dispose. */ }

        /// <inheritdoc />
        public void Reset() => throw new NotSupportedException();
    }
}
