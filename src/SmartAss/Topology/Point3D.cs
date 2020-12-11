// <copyright file = "Point3D.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace SmartAss.Topology
{
    public readonly struct Point3D : IEquatable<Point3D>
    {
        /// <summary>The orgin.</summary>
        public static readonly Point3D O;

        /// <summary>Initializes a new instance of the <see cref="Point3D"/> struct.</summary>
        public Point3D(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary> Gets or sets the x-coordinate.</summary>
        public int X { get; }

        /// <summary> Gets or sets the y-coordinate.</summary>
        public int Y { get; }

        /// <summary> Gets or sets the z-coordinate.</summary>
        public int Z { get; }

        private Point3D Add(Vector3D vector)
            => new Point3D(X + vector.X, Y + vector.Y, Z + vector.Z);

        /// <inheritdoc />
        public override string ToString() => $"({X}, {Y}, {Z})";

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is Point3D other && Equals(other);

        /// <inheritdoc />
        public bool Equals(Point3D other)
            => X == other.X
            && Y == other.Y
            && Z == other.Z;

        /// <inheritdoc />
        public override int GetHashCode() => X ^ (Y << 11) ^ (Z << 20);

        /// <summary>Compares two points.</summary>
        public static bool operator ==(Point3D a, Point3D b) => a.Equals(b);

        /// <summary>Compares two points.</summary>
        public static bool operator !=(Point3D a, Point3D b) => !(a == b);

        /// <summary>Adds a vector to a point.</summary>
        public static Point3D operator +(Point3D point, Vector3D vector) => point.Add(vector);
    }
}
