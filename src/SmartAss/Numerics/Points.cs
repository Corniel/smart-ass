// <copyright file = "Points.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

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
    }
}
