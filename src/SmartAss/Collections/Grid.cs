// <copyright file = "Grid.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Numerics;
using SmartAss.Parsing;

namespace SmartAss.Collections;

/// <summary>Represents a two dimensional grid.</summary>
/// <typeparam name="T">
/// The type of the elements of the grid.
/// </typeparam>
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
[DebuggerDisplay("Cols: {Cols}, Rows: {Rows}")]
public partial class Grid<T> : IEnumerable<KeyValuePair<Point, T>>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly T[][] elements;

    /// <summary>Initializes a new instance of the <see cref="Grid{T}"/> class.</summary>
    public Grid(int cols, int rows)
    {
        Cols = cols;
        Rows = rows;
        elements = Jagged.Array<T>(rows, cols);
    }

    /// <summary>Initializes a new instance of the <see cref="Grid{T}"/> class.</summary>
    public Grid(T[][] grid)
    {
        elements = Guard.NotNull(grid, nameof(grid));

        if (grid.Any(row => row is null) || grid.Select(row => row.Length).Distinct().Count() != 1)
        {
            throw new ArgumentException("Not all rows have the same amount of columns.");
        }

        Cols = grid[0].Length;
        Rows = grid.Length;
    }

    /// <summary>Initializes a new instance of the <see cref="Grid{T}"/> class.</summary>
    public Grid(T[,] grid)
    {
        Guard.NotNull(grid, nameof(grid));

        Cols = grid.GetLength(1);
        Rows = grid.GetLength(0);
        elements = Jagged.Array<T>(Rows, Cols);

        foreach (var p in Points.Grid(Cols, Rows))
        {
            this[p] = grid[p.Y, p.X];
        }
    }

    /// <summary>Gets the number of columns.</summary>
    public int Cols { get; }

    /// <summary>Gets the number of rows.</summary>
    public int Rows { get; }

    /// <summary>Gets size (cols * rows).</summary>
    public int Size => Cols * Rows;

    /// <summary>Gets and set an element based on its column and row.</summary>
    public T this[int col, int row]
    {
        get => Get(new Point(col, row));
        set => Set(new Point(col, row), value);
    }

    /// <summary>Gets and set an element based on its location.</summary>
    public T this[Point location]
    {
        get => Get(location);
        set => Set(location, value);
    }

    /// <summary>Gets the Neighbors of a point.</summary>
    public Grid<Maps.GridNeighbors> Neighbors { get; protected set; }

    public void Set(T value, IEnumerable<Point> locations)
    {
        foreach (var location in locations ?? Array.Empty<Point>())
        {
            Set(location, value);
        }
    }

    /// <summary>Gets all points containing an value.</summary>
    /// <remarks>
    /// If <typeparamref name="T"/> is a struct, none is excluded.
    /// </remarks>
    public IEnumerable<Point> Positions => this.Select(e => e.Key);

    /// <summary>Gets all (not null) tiles.</summary>
    /// <remarks>
    /// If <typeparamref name="T"/> is a struct, none is excluded.
    /// </remarks>
    public IEnumerable<T> Tiles => elements.SelectMany(row => row).Where(elm => elm is not null);

    /// <summary>Returns true if the point is on the grid.</summary>
    public bool OnGrid(Point p) => p.X >= 0 && p.X < Cols && p.Y >= 0 && p.Y < Rows;

    public Grid<T> Rotate(DiscreteRotation rotation)
     => ((int)rotation).Mod(4) switch
     {
         1 => RotateDeg090(),
         2 => RotateDeg180(),
         3 => RotateDeg270(),
         _ => this,
     };

    public Grid<T> Flip(bool horizontal) => horizontal ? FlipHorizontal() : FlipVertical();

    public string ToString(Func<T, char> transform) => CharPixels.From(this, transform).ToString();

    public void Clear()
    {
        foreach(var pos in Points.Grid(Cols, Rows)) { this[pos] = default; }
    }

    private T Get(Point p)
    {
        try
        {
            return elements[p.Y][p.X];
        }
        catch (IndexOutOfRangeException) { throw new NotOnGrid(p); }
    }

    private void Set(Point p, T value)
    {
        try
        {
            elements[p.Y][p.X] = value;
        }
        catch (IndexOutOfRangeException) { throw new NotOnGrid(p); }
    }

    private Grid<T> RotateDeg090()
    {
        var rotated = new Grid<T>(Rows, Cols);
        foreach (var source in Points.Grid(Cols, Rows))
        {
            rotated[source.Y, Cols - source.X - 1] = this[source];
        }
        return rotated;
    }

    private Grid<T> RotateDeg180()
    {
        var rotated = new Grid<T>(Cols, Rows);
        foreach (var source in Points.Grid(Cols, Rows))
        {
            rotated[Cols - source.X - 1, Rows - source.Y - 1] = this[source];
        }
        return rotated;
    }

    private Grid<T> RotateDeg270()
    {
        var rotated = new Grid<T>(Rows, Cols);
        foreach (var source in Points.Grid(Cols, Rows))
        {
            rotated[Rows - source.Y - 1, source.X] = this[source];
        }
        return rotated;
    }

    private Grid<T> FlipHorizontal()
    {
        var flipped = new Grid<T>(Cols, Rows);
        foreach (var point in Points.Grid(Cols, Rows))
        {
            flipped[Cols - point.X - 1, point.Y] = this[point];
        }
        return flipped;
    }

    private Grid<T> FlipVertical()
    {
        var flipped = new Grid<T>(Cols, Rows);
        foreach (var point in Points.Grid(Cols, Rows))
        {
            flipped[point.X, Rows - point.Y - 1] = this[point];
        }
        return flipped;
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<Point, T>> GetEnumerator()
        => Points.Grid(Cols, Rows).Select(p => KeyValuePair.Create(p, this[p])).Where(e => e.Value is not null).GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public static Grid<T> FromPoints(IEnumerable<Point> points)
        => new Grid<T>(points.Max(p => p.X) + 1, points.Max(p => p.Y) + 1);

    public static Grid<T> FromPoints(IEnumerable<Point> points, T value)
    {
        var grid = FromPoints(points);
        grid.Set(value, points);
        return grid;
    }
}
