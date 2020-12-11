// <copyright file = "Vector.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace SmartAss.Topology
{
    public readonly struct Vector : IEquatable<Vector>
    {
        /// <summary>Zero length vector.</summary>
        public static readonly Vector O;

        /// <summary>Initializes a new instance of the <see cref="Vector"/> struct.</summary>
        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary> Gets or sets the x-coordinate.</summary>
        public int X { get; }

        /// <summary> Gets or sets the y-coordinate.</summary>
        public int Y { get; }

        /// <summary>Gets the angle.</summary>
        public double Angle => Math.Atan2(Y, X);

        /// <inheritdoc />
        public override string ToString() => $"({X}, {Y})";

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is Vector other && Equals(other);

        /// <inheritdoc />
        public bool Equals(Vector other) => X == other.X && Y == other.Y;

        /// <inheritdoc />
        public override int GetHashCode() => X ^ (Y << 16);
    }
}
