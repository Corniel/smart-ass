// <copyright file = "NotOnGrid.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Numerics;

namespace SmartAss.Collections;

[Serializable]
public class NotOnGrid : InvalidOperationException
{
    public NotOnGrid()
        : this("The position is not on the grid.") => Do.Nothing();

    public NotOnGrid(Point position)
        : this($"The position {position} is not on the grid.") => Do.Nothing();

    public NotOnGrid(string message)
        : base(message) => Do.Nothing();

    public NotOnGrid(string message, Exception innerException)
        : base(message, innerException) => Do.Nothing();
}
