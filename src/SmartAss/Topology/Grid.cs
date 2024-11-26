// <copyright file = "Grid.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using System;
using System.Linq;
using System.Text;
using static System.FormattableString;

namespace SmartAss.Topology
{
    /// <summary>Represents a 2d raster map.</summary>
    public abstract class Grid<T> : Map<T>
        where T : GridTile<T>
    {
        /// <summary>Initializes a new instance of the <see cref="Grid{T}"/> class.</summary>
        protected Grid(int cols, int rows, GridType type)
            : base(rows * cols)
        {
            GridType = type;
            Cols = cols;
            Rows = rows;

#pragma warning disable S1699
            // Constructors should only call non-overridable methods
            // This is what we want, different initializations for
            // different overrides.
            Initialize(cols, rows);
#pragma warning restore S1699
        }

        /// <summary>Gets the tile based on its row and column.</summary>
        public T this[int col, int row]
        {
            get
            {
                var c = GridType.IsSpheric() ? col.Mod(Cols) : col;
                var r = GridType.IsSpheric() ? row.Mod(Rows) : row;
                return c >= 0 && c < Cols && r >= 0 && r < Rows
                    ? Tiles[Index(c, r)]
                    : null;
            }
        }

        /// <summary>Gets the tile based on its position.</summary>
        public T this[Point position] => this[position.X, position.Y];

        /// <summary>Gets the type of the raster.</summary>
        public GridType GridType { get; }

        /// <summary>The number of rows (height).</summary>
        public int Rows { get; }

        /// <summary>The number of columns (width).</summary>
        public int Cols { get; }

        public bool OnGrid(Point position)
            => position.X >= 0 && position.X < Cols
            && position.Y >= 0 && position.Y < Rows;

        /// <summary>Renders the grid, using a formatter for the tile.</summary>
        public string ToString(Func<T, string> formatter)
        {
            var sb = new StringBuilder();
            for (var row = 0; row < Rows; row++)
            {
                for (var col = 0; col < Cols; col++)
                {
                    sb.Append(formatter(this[col, row]));
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        /// <summary>Initializes a rows * cols sized raster.</summary>
        protected virtual void Initialize(int cols, int rows)
        {
            var neighbors = new SimpleList<Vector>(8);
            if (GridType.HasFlag(GridType.Horizontal))
            {
                neighbors.Add(Vector.W);
                neighbors.Add(Vector.E);
            }
            if (GridType.HasFlag(GridType.Veritical))
            {
                neighbors.Add(Vector.N);
                neighbors.Add(Vector.S);
            }
            if (GridType.HasFlag(GridType.Diagonal))
            {
                neighbors.Add(Vector.NE);
                neighbors.Add(Vector.NW);
                neighbors.Add(Vector.SE);
                neighbors.Add(Vector.SW);
            }

            foreach (var point in Points.Grid(cols, rows))
            {
                var n = GridType.IsSpheric()
                    ? neighbors.Count
                    : neighbors.Select(v => point + v).Count(p => OnGrid(p));

                var index = Index(point.X, point.Y);
                Tiles[index] = Create(index, point.X, point.Y, n);
            }

            foreach (var tile in Tiles)
            {
                foreach (var position in neighbors.Select(v => tile.Position + v))
                {
                    var neighbor = this[position];
                    if (neighbor is ITile)
                    {
                        tile.Neighbors.Add(neighbor);
                    }
                }
            }
        }

        /// <summary>Create a single tile for this raster.</summary>
        protected abstract T Create(int index, int col, int row, int neighbors);

        protected virtual int Index(int col, int row) => col + (row * Cols);

        /// <inheritdoc />
        protected override string DebuggerDisplay
        {
            get => Invariant($"Size: {Rows:#,##0} (height)  x {Cols:#,##0} (width)");
        }
    }
}
