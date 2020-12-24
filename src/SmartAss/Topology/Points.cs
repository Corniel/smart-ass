// <copyright file = "Points.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace SmartAss.Topology
{
    public static class Points
    {
        public static T Get<T>(this T[][] jagged, Point point) => Guard.NotNull(jagged, nameof(jagged))[point.X][point.Y];
        public static void Set<T>(this T[][] jagged, Point point, T value) => Guard.NotNull(jagged, nameof(jagged))[point.X][point.Y] = value;
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
