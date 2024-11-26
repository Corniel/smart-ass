// <copyright file = "QueueExtensions.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss;
using SmartAss.Collections; 

namespace System.Collections.Generic;

public static class StackExtensions
{
    public static Stack<T> Copy<T>(this Stack<T> queue)
        => new Stack<T>(Guard.NotNull(queue, nameof(queue)).Count).PushRange(queue);

    public static Stack<T> PushRange<T>(this Stack<T> stack, IEnumerable<T> items)
    {
        Guard.NotNull(stack, nameof(stack));
        Guard.NotNull(items, nameof(items));
        foreach (var item in items)
        {
            stack.Push(item);
        }
        return stack;
    }

    public static PopsAll<T> PopAll<T>(this Stack<T> stack) => new(Guard.NotNull(stack, nameof(stack)));
}

public readonly struct PopsAll<T>(Stack<T> stack) : Iterator<T>
{
    public T Current => stack.Pop();

    public bool MoveNext() => stack.Count != 0;

    public void Dispose() { /* Nothing to dispose. */ }

    public void Reset() => throw new NotSupportedException();
}
