// <copyright file = "CountdownTimerEnumerator.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#pragma warning disable S3898 // Value types should implement "IEquatable<T>"
// Of no value for an IEnumerator

namespace SmartAss;

/// <summary>Loops a collection until the countdown timer expires.</summary>
/// <typeparam name="T">
/// Type to enumerate over.
/// </typeparam>
internal struct CountdownTimerEnumerator<T>
    : IEnumerable<T>, IEnumerator<T>
{
    private readonly T[] array;
    private readonly int max;
    private readonly CountdownTimer timer;
    private int index;

    /// <summary>Initializes a new instance of the <see cref="CountdownTimerEnumerator{T}"/> struct.</summary>
    public CountdownTimerEnumerator(T[] collection, CountdownTimer t)
    {
        array = collection;
        max = array.Length - 1;
        index = -1;
        timer = t;
    }

    /// <inheritdoc />
    public readonly T Current => array[index];

    /// <inheritdoc />
    readonly object? IEnumerator.Current => Current;

    /// <inheritdoc />
    [Impure]
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
    [Pure]
    public IEnumerator<T> GetEnumerator() => this;

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <inheritdoc />
    public void Dispose() => Do.Nothing();
}
