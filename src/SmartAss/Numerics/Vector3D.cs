// <copyright file = "Vector3D.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace SmartAss.Numerics
{
    public readonly struct Vector3D : IEquatable<Vector3D>
    {
        /// <summary>Zero length vector.</summary>
        public static readonly Vector3D O;

        /// <summary>Initializes a new instance of the <see cref="Vector3D"/> struct.</summary>
        public Vector3D(int x, int y, int z)
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

        public Vector3D Adjust(int x = 0, int y = 0, int z = 0) => new Vector3D(X + x, Y + y, Z + z);

        /// <inheritdoc />
        public override string ToString() => $"({X}, {Y}, {Z})";

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is Vector3D other && Equals(other);

        /// <inheritdoc />
        public bool Equals(Vector3D other)
            => X == other.X
            && Y == other.Y
            && Z == other.Z;

        /// <inheritdoc />
        public override int GetHashCode() => X ^ (Y << 11) ^ (Z << 22);

        /// <summary>Compares two vectors.</summary>
        public static bool operator ==(Vector3D a, Vector3D b) => a.Equals(b);

        /// <summary>Compares two vectors.</summary>
        public static bool operator !=(Vector3D a, Vector3D b) => !(a == b);
    }
}
