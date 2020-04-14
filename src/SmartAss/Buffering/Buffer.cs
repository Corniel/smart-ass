using SmartAss.Collections;
using SmartAss.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using static System.FormattableString;

namespace SmartAss.Buffering
{
    /// <summary>Contains multiple snakes that can be reused.</summary>
    [DebuggerDisplay("{DebuggerDisplay}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    public class Buffer<T> : IEnumerable<T>
        where T : class, IBufferable, new()
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly T[] buffer;

        /// <summary>A locker object to prevent getting invalid snakes back.</summary>
        private readonly object locker = new object();

        /// <summary>Initializes a new instance of the <see cref="Buffer{T}"/> class.</summary>
        public Buffer(int capacity = 1024)
        {
            buffer = new T[capacity];
            Capacity = capacity;
        }

        /// <summary>Gets the capacity of the buffer.</summary>
        public int Capacity { get; }

        /// <summary>Gets the number of items in the buffer.</summary>
        public int Count { get; private set; }

        /// <summary>Pops an item from the buffer.</summary>
        /// <remarks>
        /// Creates a new item, if the buffer is empty.
        /// </remarks>
        public T Pop()
        {
            T item;
            lock (locker)
            {
                item = (Count == 0)
                    ? new T()
                    : buffer[--Count];
            }

            item.Reset();
            return item;
        }

        /// <summary>Pushes an item to the buffer.</summary>
        public void Push(T item)
        {
            if (item is null) { return; }
            lock (locker)
            {
                if (Count < buffer.Length)
                {
                    buffer[Count++] = item;
                }
            }
        }

        /// <summary>Pushes items to the buffer.</summary>
        public void Push(IEnumerable<T> items)
        {
            Guard.NotNull(items, nameof(items));

            lock (locker)
            {
                foreach (var item in items)
                {
                    if (Count == Capacity) { return; }
                    buffer[Count++] = item;
                }
            }
        }

        /// <summary>Pushes items of the list to the buffer and clears the list.</summary>
        public void PushAndClear(SimpleList<T> list)
        {
            Guard.NotNull(list, nameof(list));

            Push(list);
            list.Clear();
        }

        /// <summary>Populates the full buffer.</summary>
        public void Populate()
        {
            lock (locker)
            {
                while (Count < Capacity)
                {
                    buffer[Count++] = new T();
                }
            }
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => new ArrayEnumerator<T>(buffer, Count);

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>Represents the buffer as a DEBUG <see cref="string"/>.</summary>
        internal string DebuggerDisplay => Invariant($"Count = {Count:#,##0}, Capacity: {Capacity:#,##0}");
    }
}
