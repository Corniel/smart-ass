// <copyright file = "Points.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SmartAss.Numerics;

public static class Points3D
{
    /// <summary>Gets the range within the two corner points.</summary>
    [Pure]
    public static IEnumerable<Point3D> Range(Point3D corner1, Point3D corner2)
    {
        var x_min = Math.Min(corner1.X, corner2.X);
        var x_max = Math.Max(corner1.X, corner2.X);
        var y_min = Math.Min(corner1.Y, corner2.Y);
        var y_max = Math.Max(corner1.Y, corner2.Y);
        var z_min = Math.Min(corner1.Z, corner2.Z);
        var z_max = Math.Max(corner1.Z, corner2.Z);

        for (var x = x_min; x <= x_max; x++)
        {
            for (var y = y_min; y <= y_max; y++)
            {
                for (var z = z_min; z <= z_max; z++)
                {
                    yield return new Point3D(x, y, z);
                }
            }
        }
    }
}
