using SmartAss.Navigation;
using SmartAss.Numerics;

namespace SmartAss.Collections;

public partial class Grid<T>
{
    [FluentSyntax]
    public Grid<T> SetNeighbors(
        Func<Grid<T>, Point, IReadOnlyCollection<CompassPoint>, Maps.GridNeighbors> selector,
        IReadOnlyCollection<CompassPoint>? directions = null)
    {
        Neighbors = new Grid<Maps.GridNeighbors>(Cols, Rows);
        foreach (var position in Points.Grid(Cols, Rows))
        {
            Neighbors[position] = new GridNeighbors(selector(this, position, directions ?? CompassPoints.Primary));
        }
        return this;
    }

    [DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
    [DebuggerDisplay("Count = {Count}")]
    private sealed class GridNeighbors : Maps.GridNeighbors
    {
        public GridNeighbors(Maps.GridNeighbors adhoc)
        {
            Lookup = adhoc.Directions.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            Neighbors = [.. Lookup.Values];
        }

        private readonly Point[] Neighbors;
        private readonly Dictionary<CompassPoint, Point> Lookup;

        public Point this[CompassPoint compass] => Lookup[compass];

        public Point this[int index] => Neighbors[index];

        public int Count => Neighbors.Length;

        public IEnumerable<KeyValuePair<CompassPoint, Point>> Directions => Lookup;

        [Pure]
        public bool Contains(CompassPoint compass) => Lookup.ContainsKey(compass);

        [Pure]
        public IEnumerator<Point> GetEnumerator() => new ArrayEnumerator<Point>(Neighbors, Count);

        [Pure]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
