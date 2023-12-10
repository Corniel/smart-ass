// <copyright file = "QueueExtensions.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss;
using SmartAss.Collections;

namespace System.Collections.Generic
{
    public static class QueueExtensions
    {
        public static Queue<T> Copy<T>(this Queue<T> queue)
            => new Queue<T>(Guard.NotNull(queue, nameof(queue)).Count).EnqueueRange(queue);

        public static Queue<T> EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> items)
        {
            Guard.NotNull(queue, nameof(queue));
            Guard.NotNull(items, nameof(items));
            foreach (var item in items)
            {
                queue.Enqueue(item);
            }
            return queue;
        }

        public static DequeuesAll<T> DequeueAll<T>(this Queue<T> queue) => new(Guard.NotNull(queue, nameof(queue)));

        public static DequeuesCurrent<T> DequeueCurrent<T>(this Queue<T> queue) => new(Guard.NotNull(queue, nameof(queue)));
    }

    public struct DequeuesCurrent<T> : Iterator<T>
    {
        public DequeuesCurrent(Queue<T> queue)
        {
            Queue = queue;
            Count = queue.Count;
        }
        private readonly Queue<T> Queue;
        private int Count;

        public T Current => Queue.Dequeue();

        public bool MoveNext() => Count-- > 0;

        public void Dispose() { /* Nothing to dispose. */ }

        public void Reset() => throw new NotSupportedException();
    }

    public readonly struct DequeuesAll<T>(Queue<T> queue) : Iterator<T>
    {
        public T Current => queue.Dequeue();

        public bool MoveNext() => queue.Count != 0;

        public void Dispose() { /* Nothing to dispose. */ }

        public void Reset() => throw new NotSupportedException();
    }
}
