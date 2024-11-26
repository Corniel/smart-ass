// <copyright file = "TileQueue.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using SmartAss.Diagnostics;
using SmartAss.Logging;
using static System.FormattableString;

namespace SmartAss.Maps;

/// <summary>Queue for processing tiles.</summary>
/// <typeparam name="T">
/// Type of the tile.
/// </typeparam>
/// <remarks>
/// The capacity is fixed.
/// </remarks>
[DebuggerDisplay("{DebuggerDisplay}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public sealed class TileQueue<T> : IEnumerable<T>
    where T : class, Tile
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly T[] queue;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int head;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private int tail;

    /// <summary>Initializes a new instance of the <see cref="TileQueue{T}"/> class.</summary>
    public TileQueue(int capacity)
    {
        Logger.Ctor<TileDistances>();
        queue = new T[capacity];
    }

    /// <summary>Gets the tiles in the queue.</summary>
    public int Count => head - tail;

    /// <summary>Returns true if the queue is empty.</summary>
    public bool IsEmpty => head == tail;

    /// <summary>Returns true if the queue contains any tiles.</summary>
    public bool HasAny => head != tail;

    /// <summary>Enqueues a tile.</summary>
    [FluentSyntax]
    public TileQueue<T> Enqueue(T tile)
    {
        queue[head++] = tile;
        return this;
    }

    /// <summary>Enqueues multiple tiles.</summary>
    [FluentSyntax]
    public TileQueue<T> EnqueueRange(IEnumerable<T> tiles)
    {
        foreach (var tile in Guard.NotNull(tiles, nameof(tiles))) { Enqueue(tile); }
        return this;
    }

    /// <summary>Dequeues a tile.</summary>
    [Impure]
    public T Dequeue() => queue[tail++];

    /// <summary>Dequeues all tiles currently in the queue.</summary>
    /// <remarks>
    /// If tiles are added during the dequeuing, those are not dequeued.
    /// </remarks>
    [Impure]
    public IEnumerable<T> DequeueCurrent()
    {
        var count = Count;
        for (var i = 0; i < count; i++) { yield return Dequeue(); }
    }

    /// <summary>Clears the queue.</summary>
    public void Clear()
    {
        head = 0;
        tail = 0;
    }

    /// <inheritdoc />
    [Pure]
    public IEnumerator<T> GetEnumerator() => new ArrayEnumerator<T>(queue, tail, Count);

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>Represents the map as a DEBUG <see cref="string"/>.</summary>
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => Invariant($"Count: {Count:#,##0}");
}
