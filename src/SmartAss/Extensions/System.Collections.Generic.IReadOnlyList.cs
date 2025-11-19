namespace System.Collections.Generic;

public static class SmartAssListExtensions
{
    extension<T>(IReadOnlyList<T> list)
    {
        [Pure]
        public ReverseIterator<T> Reversed() => new(list);
    }

    public struct ReverseIterator<T>(IReadOnlyList<T> list) : IEnumerator<T>, IEnumerable<T>
    {
        private readonly IReadOnlyList<T> List = list;
        private int Index = list.Count;

        public readonly T Current => List[Index];

        readonly object IEnumerator.Current => List[Index]!;

        [Pure]
        public bool MoveNext() => Index-- > 0;

        public readonly void Dispose() { /* Nothing to dispose. */ }

        public void Reset() => throw new NotSupportedException();

        [Pure]
        public readonly IEnumerator<T> GetEnumerator() => this;

        [Pure]
        readonly IEnumerator IEnumerable.GetEnumerator() => this;
    }
}
