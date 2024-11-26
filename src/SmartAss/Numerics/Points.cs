// <copyright file = "Points.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SmartAss.Numerics;

public static class Points
{
    /// <summary>Gets all points of the specified grid (row per row).</summary>
    [Pure]
    public static IEnumerable<Point> Grid(int cols, int rows)
    {
        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                yield return new Point(col, row);
            }
        }
    }

    /// <summary>Gets the range within the two corner points.</summary>
    [Pure]
    public static IEnumerable<Point> Range(Point corner1, Point corner2)
    {
        var x_min = Math.Min(corner1.X, corner2.X);
        var x_max = Math.Max(corner1.X, corner2.X);
        var y_min = Math.Min(corner1.Y, corner2.Y);
        var y_max = Math.Max(corner1.Y, corner2.Y);

        for (var x = x_min; x <= x_max; x++)
        {
            for (var y = y_min; y <= y_max; y++)
            {
                yield return new Point(x, y);
            }
        }
    }

    /// <summary>Gets the aggregated minimum.</summary>
    [Pure]
    public static Point Min(params Point[] points) => points.AsEnumerable().Min();

    /// <summary>Gets the aggregated minimum.</summary>
    [Pure]
    public static Point Min(this IEnumerable<Point> points)
    {
        var x = points.Min(p => p.X);
        var y = points.Min(p => p.Y);
        return new(x, y);
    }

    /// <summary>Gets the aggregated maximum.</summary>
    [Pure]
    public static Point Max(params Point[] points) => points.AsEnumerable().Max();

    /// <summary>Gets the aggregated maximum.</summary>
    [Pure]
    public static Point Max(this IEnumerable<Point> points)
    {
        var x = points.Max(p => p.X);
        var y = points.Max(p => p.Y);
        return new(x, y);
    }

    [Pure]
    public static decimal ShoelaceArea(this IEnumerable<Point> points)
    {
        var area = 0m;

        Point? first = null;
        Point last = default;

        foreach (var point in points)
        {
            if (first is null) first = point;
            else
            {
                area += Prod(last, point);
            }
            last = point;
        }

        return first is { } f
            ? Math.Abs(area + Prod(last, f)) / 2
            : 0;

        long Prod(Point l, Point r) => l.X * r.Y - r.X * l.Y;
    }
}
