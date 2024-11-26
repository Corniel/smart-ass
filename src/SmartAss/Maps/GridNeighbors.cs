using SmartAss.Navigation;
using SmartAss.Numerics;

namespace SmartAss.Maps;

public interface GridNeighbors : IReadOnlyList<Point>
{
    Point this[CompassPoint compass] { get; }
    bool Contains(CompassPoint compass);

    IEnumerable<KeyValuePair<CompassPoint, Point>> Directions {get; }
}
