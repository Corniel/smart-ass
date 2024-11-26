using SmartAss.Numerics;

namespace SmartAss.Collections;

public partial class Grid<T>
{
    /// <summary>Gets all points containing an value.</summary>
    /// <remarks>
    /// If <typeparamref name="T"/> is a struct, none is excluded.
    /// </remarks>
    public IEnumerable<Point> Positions() => Positions(p => p is not null);

    public IEnumerable<Point> Positions(Predicate<Point> predicate)
        => this.AsEnumerable().Where(tile => predicate(tile.Key)).Select(tile => tile.Key);

    public IEnumerable<Point> Positions(Predicate<T> predicate)
        => this.AsEnumerable().Where(tile => predicate(tile.Value)).Select(tile => tile.Key);

    public IEnumerable<KeyValuePair<Point, T>> Row(int row)
        => Enumerable.Range(0, Cols).Select(col => KeyValuePair.Create(new Point(col, row), this[col, row]));
         

    public IEnumerable<KeyValuePair<Point, T>> Col(int col)
        => Enumerable.Range(0, Rows).Select(row => KeyValuePair.Create(new Point(col, row), this[col, row]));
}
