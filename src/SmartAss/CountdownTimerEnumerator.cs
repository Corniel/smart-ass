// <copyright file = "CountdownTimerEnumerator.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections;
using System.Collections.Generic;

namespace SmartAss
{
    /// <summary>Loops a collection until the countdown timer expires.</summary>
    internal struct CountdownTimerEnumerator<T> : IEnumerable<T>, IEnumerator<T>
    {
        private readonly T[] array;
        private readonly int max;
        private readonly CountdownTimer timer;
        private int index;

        /// <summary>Creates a new instance of a <see cref="CountdownTimerEnumerator{T}"/>.</summary>
        public CountdownTimerEnumerator(T[] collection, CountdownTimer t) : this()
        {
            array = collection;
            max = array.Length - 1;
            index = -1;
            timer = t;
        }

        /// <inheritdoc />
        public T Current => array[index];

        /// <inheritdoc />
        object IEnumerator.Current => Current;

        /// <inheritdoc />
        public bool MoveNext()
        {
            if (++index > max)
            {
                index = 0;
            }
            return !timer.Expired;
        }

        /// <inheritdoc />
        public void Reset() => throw new NotSupportedException();

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => this;

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public void Dispose() { /* Nothing to dispose */ }
    }
}
