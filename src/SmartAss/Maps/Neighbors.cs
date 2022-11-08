// <copyright file = "Neighbors.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using SmartAss.Navigation;
using SmartAss.Numerics;

namespace SmartAss.Maps
{
    public static class Neighbors
    {
        public static GridNeighbors Grid<T>(Grid<T> grid, Point position, IReadOnlyCollection<CompassPoint> directions)
            => new AdHoc<T>(grid, position, directions, n => n);

        public static GridNeighbors Cilinder<T>(Grid<T> grid, Point position, IReadOnlyCollection<CompassPoint> directions)
            => new AdHoc<T>(grid, position, directions, n => new Point(n.X.Mod(grid.Cols), n.Y));

        public static GridNeighbors Sphere<T>(Grid<T> grid, Point position, IReadOnlyCollection<CompassPoint> directions)
            => new AdHoc<T>(grid, position, directions, n => new Point(n.X.Mod(grid.Cols), n.Y.Mod(grid.Rows)));

        private readonly struct AdHoc<T> : GridNeighbors
        {
            public AdHoc(Grid<T> grid, Point position, IReadOnlyCollection<CompassPoint> directions, Func<Point, Point> selector)
            {
                Grid = grid;
                Position = position;
                this.directions = directions;
                Selector = selector;
            }
            private readonly Grid<T> Grid;
            private readonly Point Position;
            private readonly IReadOnlyCollection<CompassPoint> directions;
            private readonly Func<Point, Point> Selector;

            public int Count => Directions.Count();

            public Point this[int index] => this.Skip(index).First();

            public Point this[CompassPoint compass] => Directions.First(dir => dir.Key == compass).Value;

            public bool Contains(CompassPoint compass) => Directions.Any(dir => dir.Key == compass);

            public IEnumerable<KeyValuePair<CompassPoint, Point>> Directions
            {
                get
                {
                    var select = Selector;
                    var pos = Position;
                    var grid = Grid;
                    return directions.Select(dir => KeyValuePair.Create(dir, select(pos + dir.ToVector())))
                        .Where(p => grid.OnGrid(p.Value));
                }
            }

            public IEnumerator<Point> GetEnumerator()
            {
                var grid = Grid;
                return Position.Projections(directions.Select(p => p.ToVector()))
                    .Select(Selector)
                    .Where(p => grid.OnGrid(p))
                    .GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
