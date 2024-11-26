// <copyright file = "GrideTypes.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SmartAss.Topology
{
    public static class GrideTypes
    {
        public static bool IsSpheric(this GridType tp) => (tp & GridType.Spheric) == GridType.Spheric;
    }
}
