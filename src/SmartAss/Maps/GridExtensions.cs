// <copyright file = "GridExtensions.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using SmartAss.Numerics;

namespace SmartAss.Maps;

/// <summary>Function that returns potential neighbors for a location on a grid.</summary>
public delegate IEnumerable<Point> GridNeighbors<T>(Grid<T> grid, Point location);

/// <summary>Map based extensions on <see cref="Grid{T}"/>.</summary>
public static class GridExtensions
{
    /// <summary>Initializes tiles for a grid.</summary>
    /// <typeparam name="T">
    /// The type of tile.
    /// </typeparam>
    /// <param name="grid">
    /// The grid to initialize tiles for.
    /// </param>
    /// <param name="locations">
    /// The locations to initialize.
    /// </param>
    /// <param name="ctor">
    /// The constructor to create the tiles with.
    /// </param>
    /// <param name="neighbors">
    /// The function to determine the neighbors.
    /// </param>
    public static void InitTiles<T>(
        this Grid<T> grid,
        IEnumerable<Point> locations,
        GridTileCtor<T> ctor,
        GridNeighbors<T> neighbors) where T : GridTile<T>
    {
        Guard.NotNull(grid, nameof(grid));
        Guard.NotNull(locations, nameof(locations));
        Guard.NotNull(ctor, nameof(ctor));
        Guard.NotNull(neighbors, nameof(neighbors));

        int id = 0;
        foreach (var location in locations)
        {
            grid[location] = ctor(id++, location);
        }
        foreach (var tile in grid.Tiles)
        {
            tile.Neighbors.AddRange(neighbors(grid, tile.Location).Select(loc => grid[loc]).Where(n => n is not null));
        }
    }

    [Pure]
    public static GridDistances Distances<T>(this Grid<T> grid) => new(grid.Cols, grid.Rows);
}
