// <copyright file = "CharPixels.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using SmartAss.Diagnostics;
using SmartAss.Numerics;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using CharPixel = System.Collections.Generic.KeyValuePair<SmartAss.Numerics.Point, char>;

namespace SmartAss.Parsing;

[DebuggerDisplay("{DebuggerDisplay}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
public readonly struct CharPixels : IEnumerable<CharPixel>, IEquatable<CharPixels>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IReadOnlyCollection<CharPixel> items;

    private CharPixels(IEnumerable<CharPixel> pixels, int col, int row, bool hasMissingColumns)
    {
        items = pixels.ToArray();
        Cols = col;
        Rows = row;
        HasMissingColumns = hasMissingColumns;
    }

    /// <summary>Gets the total of pixels.</summary>
    public int Count => items?.Count ?? 0;

    /// <summary>Gets the number of columns.</summary>
    public int Cols { get; }

    /// <summary>Gets the number of rows.</summary>
    public int Rows { get; }

    /// <summary>When not all rows have the same amounts of columns.</summary>
    public bool HasMissingColumns { get; }

    /// <summary>Creates a <see cref="Grid{char}"/>.</summary>
    [Pure]
    public Grid<char> Grid()
    {
        var grid = new Grid<char>(Cols, Rows);
        foreach (var p in grid.Positions()) { grid[p] = ' '; }
        foreach (var pixel in this) { grid[pixel.Key] = pixel.Value; }
        return grid;
    }

    /// <summary>Creates a <see cref="Grid{T}"/> based on the transform function.</summary>
    [Pure]
    public Grid<T> Grid<T>(Func<char, T> transform)
    {
        Guard.NotNull(transform, nameof(transform));

        var grid = new Grid<T>(Cols, Rows);
        foreach (var pixel in this) { grid[pixel.Key] = transform(pixel.Value); }
        return grid;
    }

    /// <inheritdoc />
    [Pure]
    public override string ToString()
    {
        var row = 0;
        var sb = new StringBuilder();
        foreach (var pixel in items)
        {
            if (pixel.Key.Y != row)
            {
                row = pixel.Key.Y;
                sb.AppendLine();
            }
            sb.Append(pixel.Value);
        }
        return sb.ToString();
    }

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode()
    {
        var hash = 0;
        foreach (var pixel in items)
        {
            hash = hash * 17 + pixel.GetHashCode();
        }
        return hash;
    }

    /// <inheritdoc />
    [Pure]
    public override bool Equals([NotNullWhen(true)] object? obj)
        => obj is CharPixels other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(CharPixels other)
        => Cols == other.Cols
        && Rows == other.Rows
        && Count == other.Count
        && Enumerable.SequenceEqual(items, other.items);

    /// <summary>Parses <see cref="CharPixel"/>'s.</summary>
    public static CharPixels Parse(string input, bool ignoreSpace)
    {
        Guard.NotNull(input, nameof(input));

        var col = 0;
        var row = 0;
        var cols = 0;
        var rows = 0;

        var hasMissingColumns = false;
        var buffer = new SimpleList<CharPixel>(input.Length);

        foreach (var ch in input)
        {
            if (ch == '\n')
            {
                if (col != 0)
                {
                    hasMissingColumns |= row != 0 && col != cols;
                    row++;
                }
                col = 0;
            }
            else if (!char.IsWhiteSpace(ch) || (!ignoreSpace && ch == ' '))
            {
                buffer.Add(new CharPixel(new Point(col, row), ch));
                cols = Math.Max(cols, ++col);
                rows = Math.Max(rows, row);
            }
        }
        return new CharPixels(buffer, cols, rows + 1, hasMissingColumns);
    }

    public static CharPixels From<T>(Grid<T> grid, Func<T, char> transform)
    {
        var pixels = new CharPixels(grid.Select(tile => new CharPixel(tile.Key, transform(tile.Value))), grid.Cols, grid.Rows, false);
        return pixels;
    }

    /// <inheritdoc />
    public IEnumerator<CharPixel> GetEnumerator()
        => (items ?? []).GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay
        => $"Count: {Count}, Columns: {Cols}, Rows: {Rows}{(HasMissingColumns ? ", missing columns" : string.Empty)}";
}
