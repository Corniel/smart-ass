using SmartAss.Collections;
using SmartAss.Diagnostics;
using SmartAss.Logging;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using static System.FormattableString;

namespace SmartAss.Topology
{
    [DebuggerDisplay("{DebuggerDisplay}"), DebuggerTypeProxy(typeof(CollectionDebugView))]
    public class TileQueue<T> : IEnumerable<T> where T : ITile<T>
    {
        private readonly T[] queue;
        private int head;
        private int tail;

        public TileQueue(Map<T> map)
        {
            Logger.Ctor<TileDistances>();
            queue = new T[map.Size];
        }

        public int Count => head - tail;
        public bool IsEmpty => head == tail;
        public bool HasAny => head != tail;

        public void Enqueue(T tile)
        {
            queue[head++] = tile;
        }
        public T Dequeue() => queue[tail++];

        public IEnumerable<T> DequeueCurrent()
        {
            var count = Count;
            for (var i = 0; i < count; i++)
            {
                yield return Dequeue();
            }
        }

        public void Clear()
        {
            head = 0;
            tail = 0;
        }

        #region IEnumerable

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => new ArrayEnumerator<T>(queue, tail, Count);

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        /// <summary>Represents the map as a DEBUG <see cref="string"/>.</summary>
        protected virtual string DebuggerDisplay
        {
            get => Invariant($"Count: {Count:#,##0}");
        }
    }
}
