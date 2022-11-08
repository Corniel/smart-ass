// <copyright file = "Points.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;

namespace SmartAss.Numerics
{
    public static class Points
    {
        /// <summary>Gets all points of the specified grid (row per row).</summary>
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
    }
}
