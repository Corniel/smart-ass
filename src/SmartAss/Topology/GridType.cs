// <copyright file = "GridType.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace SmartAss.Topology
{
    [Flags]
    public enum GridType
    {
        Horizontal = 0b001,
        Veritical = 0b0010,
        Diagonal = 0b00100,
        Spheric = 0b0001000,

        Grid = Horizontal | Veritical,
        Sphere = Grid | Spheric,
    }
}
