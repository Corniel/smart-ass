// <copyright file = "Vector3D.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Parsing;

namespace SmartAss.Numerics;

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

    public int this[int index] => index switch
    {
        0 => X,
        1 => Y,
        2 => Z,
        _ => throw new IndexOutOfRangeException(),
    };

    [Pure]
    public Vector3D Adjust(int x = 0, int y = 0, int z = 0) => new(X + x, Y + y, Z + z);

    [Pure]
    public int ManhattanDistance(Vector3D other)
        => (X - other.X).Abs() + (Y - other.Y).Abs() + (Z - other.Z).Abs();

    /// <inheritdoc />
    [Pure]
    public override string ToString() => $"({X}, {Y}, {Z})";

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is Vector3D other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(Vector3D other)
        => X == other.X
        && Y == other.Y
        && Z == other.Z;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => X ^ (Y << 11) ^ (Z << 22);

    /// <summary>Compares two vectors.</summary>
    public static bool operator ==(Vector3D a, Vector3D b) => a.Equals(b);

    /// <summary>Compares two vectors.</summary>
    public static bool operator !=(Vector3D a, Vector3D b) => !(a == b);

    /// <summary>Adds two vectors.</summary>
    public static Vector3D operator +(Vector3D l, Vector3D r) => new(l.X + r.X, l.Y + r.Y, l.Z + r.Z);

    /// <summary>Subtracts two vectors.</summary>
    public static Vector3D operator -(Vector3D l, Vector3D r) => new(l.X - r.X, l.Y - r.Y, l.Z - r.Z);

    [Pure]
    public static Vector3D Parse(string str)
    {
        var split = str.Split(',');
        return new Vector3D(split[0].Int32(), split[1].Int32(), split[2].Int32());
    }
}
