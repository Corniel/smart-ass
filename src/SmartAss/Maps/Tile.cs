// <copyright file = "Tile.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;

namespace SmartAss.Maps;

/// <summary>Represents a tile in a map.</summary>
public interface Tile
{
    /// <summary>The identifier of the tile.</summary>
    int Id { get; }

    /// <summary>The neighbors of the tile.</summary>
    [Pure]
    IEnumerable<Tile> GetNeighbors();
}

/// <summary>Represents a tile in a map.</summary>
/// <typeparam name="T">
/// The actual implementation of <see cref="Tile"/>.
/// </typeparam>
public interface Tile<T> : Tile where T : Tile
{
    /// <summary>The neighbors of the tile.</summary>
    SimpleList<T> Neighbors { get; }
}
