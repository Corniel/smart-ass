// <copyright file = "QueueExtensions.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss;

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

        public static IEnumerable<T> DeuqueCurrent<T>(this Queue<T> queue)
        {
            Guard.NotNull(queue, nameof(queue));
            var count = queue.Count;
            while (count-- > 0)
            {
                yield return queue.Dequeue();
            }
        }
    }
}
