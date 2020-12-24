// <copyright file = "Vector.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;

namespace SmartAss.Numerics
{
    public readonly struct Vector : IEquatable<Vector>
    {
        /// <summary>Zero length vector.</summary>
        public static readonly Vector O;

        /// <summary>North (0, -1).</summary>
        public static readonly Vector N = new Vector(+0, -1);

        /// <summary>East (1, 0).</summary>
        public static readonly Vector E = new Vector(+1, +0);

        /// <summary>South (0, 1).</summary>
        public static readonly Vector S = new Vector(+0, +1);

        /// <summary>West (-1, 0).</summary>
        public static readonly Vector W = new Vector(-1, +0);

        /// <summary>Northeast (1, -1).</summary>
        public static readonly Vector NE = N + E;

        /// <summary>Northwest (-1, -1).</summary>
        public static readonly Vector NW = N + W;

        /// <summary>Northeast (1, 1).</summary>
        public static readonly Vector SE = S + E;

        /// <summary>Southwest (-1, 1).</summary>
        public static readonly Vector SW = S + W;

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

        public Vector Rotate(DiscreteRotation rotation)
            => ((int)rotation).Mod(4) switch
            {
                1 => new Vector(+Y, -X),
                2 => new Vector(-X, -Y),
                3 => new Vector(-Y, +X),
                _ => this,
            };

        private Vector Add(Vector vector) => new Vector(X + vector.X, Y + vector.Y);

        private Vector Multiply(int factor) => new Vector(X * factor, Y * factor);

        /// <inheritdoc />
        public override string ToString() => $"({X}, {Y})";

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is Vector other && Equals(other);

        /// <inheritdoc />
        public bool Equals(Vector other) => X == other.X && Y == other.Y;

        /// <inheritdoc />
        public override int GetHashCode() => X ^ (Y << 16);

        /// <summary>Compares two vectors.</summary>
        public static bool operator ==(Vector a, Vector b) => a.Equals(b);

        /// <summary>Compares two vectors.</summary>
        public static bool operator !=(Vector a, Vector b) => !(a == b);

        /// <summary>Adds two vectors.</summary>
        public static Vector operator +(Vector a, Vector b) => a.Add(b);

        /// <summary>Multiplies a vector by a factor.</summary>
        public static Vector operator *(Vector vector, int factor) => vector.Multiply(factor);
    }
}
