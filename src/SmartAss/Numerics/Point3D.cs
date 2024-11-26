// <copyright file = "Point3D.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Parsing;
using System.ComponentModel;

namespace SmartAss.Numerics;

[TypeConverter(typeof(Conversion.Numerics.Point3DTypeConverter))]
public readonly struct Point3D : IEquatable<Point3D>
{
    /// <summary>The origin.</summary>
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

    [Pure]
    public int ManhattanDistance(Point3D other)
        => (X - other.X).Abs() + (Y - other.Y).Abs() + (Z - other.Z).Abs();

    [Pure]
    private Point3D Add(Vector3D vector)
        => new(X + vector.X, Y + vector.Y, Z + vector.Z);

    [Pure]
    private Point3D Subtract(Vector3D vector)
        => new(X - vector.X, Y - vector.Y, Z - vector.Z);

    /// <inheritdoc />
    [Pure]
    public override string ToString() => $"({X}, {Y}, {Z})";

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is Point3D other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(Point3D other)
        => X == other.X
        && Y == other.Y
        && Z == other.Z;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => X ^ (Y << 11) ^ (Z << 20);

    /// <summary>Compares two points.</summary>
    public static bool operator ==(Point3D a, Point3D b) => a.Equals(b);

    /// <summary>Compares two points.</summary>
    public static bool operator !=(Point3D a, Point3D b) => !(a == b);

    /// <summary>Adds a vector to a point.</summary>
    public static Point3D operator +(Point3D point, Vector3D vector) => point.Add(vector);

    public static Point3D operator -(Point3D point, Vector3D vector) => point.Subtract(vector);

    [Pure]
    public static Point3D Parse(string str)
    {
        var split = str.Split(',');
        return new Point3D(split[0].Int32(), split[1].Int32(), split[2].Int32());
    }
}
