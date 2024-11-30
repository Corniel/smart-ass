// <copyright file = "Point4D.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Parsing;
using System.ComponentModel;

namespace SmartAss.Numerics;

[TypeConverter(typeof(Conversion.Numerics.Point4DTypeConverter))]
public readonly struct Point4D : IEquatable<Point4D>
{
    /// <summary>The origin.</summary>
    public static readonly Point4D O;

    /// <summary>Initializes a new instance of the <see cref="Point4D"/> struct.</summary>
    public Point4D(int x, int y, int z, int t)
    {
        X = x;
        Y = y;
        Z = z;
        T = t;
    }

    /// <summary> Gets or sets the x-coordinate.</summary>
    public int X { get; }

    /// <summary> Gets or sets the y-coordinate.</summary>
    public int Y { get; }

    /// <summary> Gets or sets the z-coordinate.</summary>
    public int Z { get; }

    /// <summary> Gets or sets the t-coordinate.</summary>
    public int T { get; }

    [Pure]
    public int ManhattanDistance(Point4D other)
        => (X - other.X).Abs()
        + (Y - other.Y).Abs()
        + (Z - other.Z).Abs()
        + (T - other.T).Abs();

    /// <inheritdoc />
    [Pure]
    public override string ToString() => $"({X}, {Y}, {Z}, {T})";

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is Point4D other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(Point4D other)
        => X == other.X
        && Y == other.Y
        && Z == other.Z
        && T == other.T;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => X ^ (Y << 8) ^ (Z << 16) ^ (Z << 24);

    /// <summary>Compares two points.</summary>
    public static bool operator ==(Point4D a, Point4D b) => a.Equals(b);

    /// <summary>Compares two points.</summary>
    public static bool operator !=(Point4D a, Point4D b) => !(a == b);

    [Pure]
    public static Point4D Parse(string str)
    {
        Guard.NotNullOrEmpty(str, nameof(str));
        var split = str.Split(',');
        return new Point4D(split[0].Int32(), split[1].Int32(), split[2].Int32(), split[3].Int32());
    }
}
