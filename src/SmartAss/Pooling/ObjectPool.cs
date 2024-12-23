// <copyright file = "ObjectPool.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using SmartAss.Diagnostics;
using static System.FormattableString;

namespace SmartAss.Pooling;

/// <summary>Contains multiple objects that can be reused.</summary>
[DebuggerDisplay("{DebuggerDisplay}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public sealed class ObjectPool<T> : IEnumerable<T> where T : class
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly T[] pool;

    /// <summary>A locker object to prevent getting invalid objects back.</summary>
    private readonly object locker = new();

    /// <summary>Initializes a new instance of the <see cref="ObjectPool{T}"/> class.</summary>
    public ObjectPool(int capacity = 1024) => pool = new T[capacity];

    /// <summary>Gets the capacity of the object pool.</summary>
    public int Capacity => pool.Length;

    /// <summary>Gets the number of items in the object pool.</summary>
    public int Count { get; private set; }

    /// <summary>Gets an item from the object pool.</summary>
    /// <remarks>
    /// Creates a new item, if the object pool is empty.
    /// </remarks>
    [Impure]
    public T Get(Func<T> create)
    {
        T item;
        lock (locker)
        {
            item = (Count == 0)
                ? create()
                : pool[--Count];
        }

        return item;
    }

    /// <summary>Gets an item from the object pool.</summary>
    /// <remarks>
    /// Creates a new item, if the object pool is empty.
    /// </remarks>
    [Pure]
    public Reusable<T> Reusable(Func<T> create) => new(Get(create), this);

    /// <summary>Releases the item for reuse.</summary>
    public void Release(T item)
    {
        if (item is null) { return; }
        lock (locker)
        {
            if (Count < pool.Length)
            {
                pool[Count++] = item;
            }
        }
    }

    /// <summary>Releases the items for reuse.</summary>
    public void Release(IEnumerable<T> items)
    {
        Guard.NotNull(items, nameof(items));

        lock (locker)
        {
            foreach (var item in items)
            {
                if (Count == Capacity) { return; }
                pool[Count++] = item;
            }
        }
    }

    /// <summary>Releases the list for reuse, and clears it.</summary>
    public void ReleaseAndClear(SimpleList<T> list)
    {
        Guard.NotNull(list, nameof(list));

        Release(list);
        list.Clear();
    }

    /// <summary>Populates the full object pool.</summary>
    [FluentSyntax]
    public ObjectPool<T> Populate(Func<T> create, int count)
    {
        lock (locker)
        {
            while (Count < count)
            {
                pool[Count++] = create();
            }
        }
        return this;
    }

    /// <inheritdoc />
    [Pure]
    public IEnumerator<T> GetEnumerator() => new ArrayEnumerator<T>(pool, Count);

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Represents the buffer as a DEBUG <see cref="string"/>.</summary>
    internal string DebuggerDisplay => Invariant($"Count = {Count:#,##0}, Capacity: {Capacity:#,##0}");
}
