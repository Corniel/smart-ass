// <copyright file = "CountdownTimerExtensions.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;
using System.Linq;

namespace SmartAss
{
    /// <summary>Extensions involving a <see cref="CountdownTimer"/>.</summary>
    public static class CountdownTimerExtensions
    {
        /// <summary>Loops through the collection while the timer didn't expire.</summary>
        public static IEnumerable<T> LoopWhileCountingDown<T>(this ICollection<T> collection, CountdownTimer timer)
        {
            var array = collection is T[] arr ? arr : collection.ToArray();
            return new CountdownTimerEnumerator<T>(array, timer);
        }
    }
}
