using SmartAss.Logging;
using System.Collections;
using System.Collections.Generic;

namespace SmartAss.Collections
{
    /// <summary>Enumerates through a (subset of) an array.</summary>
    public struct ArrayEnumerator<T> : IEnumerator<T>, IEnumerable<T>
    {
        private readonly T[] array;
        private readonly int length;
        private int index;

        /// <summary>Creates a new instance of an enumerator.</summary>
        public ArrayEnumerator(T[] array, int count) : this(array, 0, count) { }

        /// <summary>Creates a new instance of an enumerator.</summary>
        public ArrayEnumerator(T[] array, int startIndex, int count)
        {
            this.array = array;
            length = count;
            index = startIndex - 1;
            Logger.Ctor<ArrayEnumerator<T>>();
        }

        /// <inheritdoc />
        public T Current => array[index];

        /// <inheritdoc />
        object IEnumerator.Current => Current;

        /// <inheritdoc />
        public bool MoveNext() => ++index < length;

        /// <inheritdoc />
        public void Reset() => index = -1;

        #region IEnumerable

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => this;

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        /// <inheritdoc />
        public void Dispose() { /* Nothing to dispose */}

    }
}
