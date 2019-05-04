using SmartAss.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using static System.FormattableString;

namespace SmartAss.Collections
{
    /// <summary>Represents a simple list.</summary>
    /// <remarks>
    /// This class is different from a <see cref="List{T}"/> on multiple parts:
    /// The enumerator has no state changed checks.
    /// The capacity is a hard limit.
    /// The clear just resets the count.
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay}"), DebuggerTypeProxy(typeof(CollectionDebugView))]
    public class SimpleList<T> : ICollection<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected readonly T[] array;

        /// <summary>Creates a new instance of a simple list.</summary>
        public SimpleList(int capacity) => array = new T[capacity];

        /// <inheritdoc />
        public T this[int index] => array[index];

        /// <inheritdoc />
        public int Count { get; protected set; }

        /// <summary>Gets the maximum capacity of the simple list.</summary>
        public int Capacity => array.Length;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <summary>Returns true if the simple list is empty.</summary>
        public bool IsEmpty() => Count == 0;

        /// <summary>Returns true if the simple list has any items.</summary>
        public bool HasAny() => Count != 0;

        /// <summary>Returns true if the simple list contains one item.</summary>
        public bool HasSingle() => Count == 1;

        /// <summary>Returns true if the simple list contains multiple items.</summary>
        public bool HasMultiple() => Count > 1;

        /// <inheritdoc />
        public virtual void Add(T item) => array[Count++] = item;

        /// <inheritdoc />
        public void AddRange(IEnumerable<T> items)
        {
            foreach(var item in items)
            {
                Add(item);
            }
        }

        /// <summary>Gets the index of the item.</summary>
        /// <param name="item">
        /// The item to search for.
        /// </param>
        /// <returns>
        /// Minus one if not found, else the index.
        /// </returns>
        public int IndexOf(T item)
        {
            for (var index = 0; index < Count; index++)
            {
                if (Equals(array[index], item))
                {
                    return index;
                }
            }
            return -1;
        }

        /// <inheritdoc />
        public virtual bool Contains(T item) => IndexOf(item) != -1;

        /// <inheritdoc />
        public virtual bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index != -1)
            {
                for (var i = index + 1; i < Count; i++)
                {
                    array[i - 1] = array[i];
                }
                Count--;
                return true;
            }
            return false;
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex) => Array.Copy(this.array, 0, array, arrayIndex, Count);

        /// <inheritdoc />
        public virtual void Clear() => Count = 0;

        #region IEnumerable

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => new ArrayEnumerator<T>(array, Count);

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        /// <summary>Represents the simple list as a DEBUG <see cref="string"/>.</summary>
        internal string DebuggerDisplay => Invariant($"Count = {Count:#,##0}, Capacity: {Capacity:#,##0}");

    }
}
