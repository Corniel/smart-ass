// <copyright file = "Point.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace SmartAss.Topology
{
    public readonly struct Point : IEquatable<Point>
    {
        /// <summary>The orgin.</summary>
        public static readonly Point O;

        /// <summary>Initializes a new instance of the <see cref="Point"/> struct.</summary>
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary> Gets or sets the x-coordinate.</summary>
        public int X { get; }

        /// <summary> Gets or sets the y-coordinate.</summary>
        public int Y { get; }

        private Point Add(Vector vector) => new Point(X + vector.X, Y + vector.Y);

        private Vector Subtract(Point other) => new Vector(X - other.X, Y - other.Y);

        /// <inheritdoc />
        public override string ToString() => $"({X}, {Y})";

        public int ManhattanDistance(Point other)
            => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is Point other && Equals(other);

        /// <inheritdoc />
        public bool Equals(Point other) => X == other.X && Y == other.Y;

        /// <inheritdoc />
        public override int GetHashCode() => X ^ (Y << 16);

        /// <summary>Compares two points.</summary>
        public static bool operator ==(Point a, Point b) => a.Equals(b);

        /// <summary>Compares two points.</summary>
        public static bool operator !=(Point a, Point b) => !(a == b);

        /// <summary>Adds a vector to a point.</summary>
        public static Point operator +(Point point, Vector vector) => point.Add(vector);

        /// <summary>Calulates the vector distantce between two points.</summary>
        public static Vector operator -(Point point, Point other) => point.Subtract(other);
    }
}
