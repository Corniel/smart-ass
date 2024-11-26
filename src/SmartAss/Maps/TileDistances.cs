// <copyright file = "TileDistances.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Diagnostics;
using SmartAss.Logging;
using static System.FormattableString;

namespace SmartAss.Maps;

[DebuggerDisplay("{DebuggerDisplay}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public class TileDistances : IEnumerable<object>
{
    private const int Mask = int.MaxValue;
    private const int Unknown = 0;
    private const int Infinite = (int.MaxValue - 1) ^ Mask;

    private readonly int[] distances;

    public TileDistances(int capacity)
    {
        Logger.Ctor<TileDistances>();
        distances = new int[capacity];
    }

    public int Known => distances.Count(d => d != Unknown);

    public int Size => distances.Length;

    public int this[int index]
    {
        get => distances[index] ^ Mask;
        set => distances[index] = value ^ Mask;
    }

    [Pure]
    public bool IsKnown(int index) => distances[index] != Unknown;

    [Pure]
    public bool IsUnknown(int index) => distances[index] == Unknown;

    public void SetInfinite(int index) => distances[index] = Infinite;

    public void Clear() => Array.Clear(distances, 0, distances.Length);

    [Pure]
    public IEnumerator<object> GetEnumerator() => distances.Select(Debug).GetEnumerator();

    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    [Pure]
    private static object Debug(int n)
    {
        if (n == Unknown) { return "?"; }
        if (n == Infinite) { return "oo"; }
        return n ^ Mask;
    }

    /// <summary>Represents the map as a DEBUG <see cref="string"/>.</summary>
    protected virtual string DebuggerDisplay => Invariant($"Size: {Size:#,##0}, Known: {Known:#,##0}");
}
