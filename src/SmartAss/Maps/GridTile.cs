// <copyright file = "GridTile.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using SmartAss.Numerics;

namespace SmartAss.Maps
{
    /// <summary>Constructor for a <see cref="GridTile{T}"/>.</summary>
    public delegate T GridTileCtor<T>(int id, Point position) where T : GridTile<T>;

    /// <summary>Represents a tile on a grid.</summary>
    /// <typeparam name="T">
    /// The type of the tile.
    /// </typeparam>
    public class GridTile<T> : Tile<T> where T : GridTile<T>
    {
        /// <summary>Initializes a new instance of the <see cref="GridTile{T}"/> class.</summary>
        protected GridTile(int id, Point location, int maxNeighbors)
        {
            Id = id;
            Location = location;
            Neighbors = new SimpleList<T>(maxNeighbors);
        }

        /// <inheritdoc />
        public int Id { get; }

        /// <summary>Gets the location of the tile.</summary>
        public Point Location { get; }

        /// <summary>Gets row of the column.</summary>
        public int Row => Location.Y;

        /// <summary>Gets the column of the tile.</summary>
        public int Col => Location.X;

        /// <inheritdoc />
        public SimpleList<T> Neighbors { get; }

        /// <inheritdoc />
        public IEnumerable<Tile> GetNeighbors() => Neighbors;
    }
}
