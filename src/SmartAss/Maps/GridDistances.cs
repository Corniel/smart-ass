// <copyright file = "TileDistances.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using SmartAss.Diagnostics;
using SmartAss.Numerics;
using static System.FormattableString;

namespace SmartAss.Maps;

[DebuggerDisplay("{DebuggerDisplay}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public class GridDistances : IEnumerable<string>
{
    private const int Mask = int.MaxValue;
    private const int Unknown = 0;
    private const int Infinite = (int.MaxValue - 1) ^ Mask;

    private readonly Grid<int> distances;

    public GridDistances(int cols, int rows) => distances = new(cols, rows);

    public int Size => distances.Size;

    public int Known => distances.Count(d => d.Value != Unknown);

    public int this[Point location]
    {
        get => distances[location] ^ Mask;
        set => distances[location] = value ^ Mask;
    }

    [Pure]
    public bool IsKnown(Point location) => distances[location] != Unknown;

    [Pure]
    public bool IsUnknown(Point location) => distances[location] == Unknown;

    public void SetInfinite(Point location) => distances[location] = Infinite;

    public void Clear() => distances.Clear();

    [Pure]
    public IEnumerator<string> GetEnumerator() => distances.Select(Debug).GetEnumerator();

    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    [Pure]
    private static string Debug(KeyValuePair<Point, int> kvp) => kvp.Value switch
    {
        Unknown => $"({kvp.Key}) ?",
        Infinite => $"({kvp.Key}) oo",
        _ => $"({kvp.Key}) {kvp.Value ^ Mask}",
    };

    /// <summary>Represents the map as a DEBUG <see cref="string"/>.</summary>
    protected virtual string DebuggerDisplay => Invariant($"Size: {Size:#,##0}, Known: {Known:#,##0}");
}
