// <copyright file = "Neighbors.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using SmartAss.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartAss.Maps
{
    public static class Neighbors
    {
        public static readonly IReadOnlyCollection<Vector> NESW = new[] { Vector.N, Vector.E, Vector.S, Vector.W };

        public static IEnumerable<Point> Grid<T>(Grid<T> grid, Point location)
             => location.Projections(NESW).Where(n => grid.OnGrid(n));

        public static IEnumerable<Point> Cilinder<T>(Grid<T> grid, Point location)
            => location.Projections(NESW)
            .Select(n => new Point(n.X.Mod(grid.Cols), n.Y))
            .Where(n => grid.OnGrid(n));

        public static IEnumerable<Point> Sphere<T>(Grid<T> grid, Point location)
            => location.Projections(NESW)
            .Select(n => new Point(n.X.Mod(grid.Cols), n.Y.Mod(grid.Rows)));
    }
}
