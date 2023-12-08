// <copyright file = "CompassPoints.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Numerics;

namespace SmartAss.Navigation
{
    public static class CompassPoints
    {
        public static readonly IReadOnlyList<CompassPoint> All = new[] { CompassPoint.N, CompassPoint.E, CompassPoint.S, CompassPoint.W, CompassPoint.NE, CompassPoint.NW, CompassPoint.SE, CompassPoint.SW };
        public static readonly IReadOnlyList<CompassPoint> Primary = new[] { CompassPoint.N, CompassPoint.E, CompassPoint.S, CompassPoint.W };
        public static readonly IReadOnlyList<CompassPoint> Secondary = new[] { CompassPoint.NE, CompassPoint.NW, CompassPoint.SE, CompassPoint.SW };

        public static string Short(this CompassPoint compassPoint)
            => compassPoint == CompassPoint.None
            ? "?"
            : compassPoint.ToString().ToLowerInvariant();

        public static Vector ToVector(this CompassPoint compassPoint)
            => Vectors.TryGetValue(compassPoint, out var vector)
            ? vector
            : default;

        public static IEnumerable<Vector> ToVectors(this IEnumerable<CompassPoint> compassPoints)
            => compassPoints.Select(ToVector);

        internal static readonly IReadOnlyDictionary<CompassPoint, Vector> Vectors = new Dictionary<CompassPoint, Vector>()
        {
            [CompassPoint.N] = Vector.N,
            [CompassPoint.E] = Vector.E,
            [CompassPoint.S] = Vector.S,
            [CompassPoint.W] = Vector.W,
            [CompassPoint.NE] = Vector.NE,
            [CompassPoint.NW] = Vector.NW,
            [CompassPoint.SE] = Vector.SE,
            [CompassPoint.SW] = Vector.SW,
        };

        public static CompassPoint Parse(string str) => Enum.Parse<CompassPoint>(str, true);
    }
}
