// <copyright file = "CompassPoints.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Numerics;

namespace SmartAss.Navigation;

public static class CompassPoints
{
    public static readonly IReadOnlyList<CompassPoint> All = [CompassPoint.N, CompassPoint.E, CompassPoint.S, CompassPoint.W, CompassPoint.NE, CompassPoint.NW, CompassPoint.SE, CompassPoint.SW];
    public static readonly IReadOnlyList<CompassPoint> Primary = [CompassPoint.N, CompassPoint.E, CompassPoint.S, CompassPoint.W];
    public static readonly IReadOnlyList<CompassPoint> Secondary = [CompassPoint.NE, CompassPoint.NW, CompassPoint.SE, CompassPoint.SW];

    [Pure]
    public static string Short(this CompassPoint compassPoint)
        => compassPoint == CompassPoint.None
        ? "?"
        : compassPoint.ToString().ToLowerInvariant();

    [Pure]
    public static Vector ToVector(this CompassPoint compassPoint)
        => Vectors.TryGetValue(compassPoint, out var vector)
        ? vector
        : default;

    [Pure]
    public static CompassPoint Flip(this CompassPoint compassPoint) => compassPoint switch
    {
        CompassPoint.N => CompassPoint.S,
        CompassPoint.E => CompassPoint.W,
        CompassPoint.S => CompassPoint.N,
        CompassPoint.W => CompassPoint.E,
        CompassPoint.NE => CompassPoint.SW,
        CompassPoint.NW => CompassPoint.SE,
        CompassPoint.SE => CompassPoint.NW,
        CompassPoint.SW => CompassPoint.NE,
        _ => CompassPoint.None,
    };

    [Pure]
    public static IEnumerable<Vector> ToVectors(this IEnumerable<CompassPoint> compassPoints)
        => compassPoints.Select(ToVector);

    internal static readonly IReadOnlyDictionary<CompassPoint, Vector> Vectors = new Dictionary<CompassPoint, Vector>()
    {
        [CompassPoint.N] = Vector.N,
        [CompassPoint.E] = Vector.E,
        [CompassPoint.S] = Vector.S,
        [CompassPoint.W] = Vector.W,
        [CompassPoint.NE] = Vector.NE,
        [CompassPoint.NW] = Vector.NW,
        [CompassPoint.SE] = Vector.SE,
        [CompassPoint.SW] = Vector.SW,
    };

    [Pure]
    public static CompassPoint Parse(string str) => Enum.Parse<CompassPoint>(str, true);
}
