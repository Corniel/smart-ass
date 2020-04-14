// <copyright file = "CompassPoints.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SmartAss.Navigation
{
    public static class CompassPoints
    {
        public static readonly CompassPoint[] All = new[] { CompassPoint.N, CompassPoint.E, CompassPoint.S, CompassPoint.W };

        public static char ToChar(this CompassPoint compassPoint) => "?nesw"[(int)compassPoint];
    }
}
