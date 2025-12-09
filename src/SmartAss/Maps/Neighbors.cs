// <copyright file = "Neighbors.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using SmartAss.Navigation;
using SmartAss.Numerics;

namespace SmartAss.Maps;

public static class Neighbors
{
    [Pure]
    public static GridNeighbors Grid<T>(Grid<T> grid, Point position, IReadOnlyCollection<CompassPoint> directions)
        => new AdHoc<T>(grid, position, directions, n => n);

    [Pure]
    public static GridNeighbors Cilinder<T>(Grid<T> grid, Point position, IReadOnlyCollection<CompassPoint> directions)
        => new AdHoc<T>(grid, position, directions, n => new Point(n.X.Mod(grid.Cols), n.Y));

    [Pure]
    public static GridNeighbors Sphere<T>(Grid<T> grid, Point position, IReadOnlyCollection<CompassPoint> directions)
        => new AdHoc<T>(grid, position, directions, n => new Point(n.X.Mod(grid.Cols), n.Y.Mod(grid.Rows)));

    private readonly struct AdHoc<T>(Grid<T> grid, Point position, IReadOnlyCollection<CompassPoint> directions, Func<Point, Point> selector) : GridNeighbors
    {
        private readonly Grid<T> Grid = grid;
        private readonly Point Position = position;
        private readonly IReadOnlyCollection<CompassPoint> directions = directions;
        private readonly Func<Point, Point> Selector = selector;

        public int Count => Directions.Count();

        public Point this[int index] => this.Skip(index).First();

        public Point this[CompassPoint compass] => Directions.First(dir => dir.Key == compass).Value;

        [Pure]
        public bool Contains(CompassPoint compass) => Directions.Any(dir => dir.Key == compass);

        public IEnumerable<KeyValuePair<CompassPoint, Point>> Directions
        {
            get
            {
                var (sel, pos, grd) = (Selector, Position, Grid);
                return directions.Select(dir => KeyValuePair.Create(dir, sel(pos + dir.ToVector())))
                    .Where(p => grd.OnGrid(p.Value));
            }
        }

        [Pure]
        public IEnumerator<Point> GetEnumerator()
        {
            var grd = Grid;
            return Position.Projections(directions.Select(p => p.ToVector()))
                .Select(Selector)
                .Where(grd.OnGrid)
                .GetEnumerator();
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
