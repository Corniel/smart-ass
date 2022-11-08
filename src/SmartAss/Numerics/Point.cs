// <copyright file = "Point.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using SmartAss.Parsing;
using System.ComponentModel;

namespace SmartAss.Numerics;

[TypeConverter(typeof(Conversion.Numerics.PointTypeConverter))]
public readonly struct Point : IEquatable<Point>
{
    /// <summary>The origin.</summary>
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

    public int this[int index] => index switch
    {
        0 => X,
        1 => Y,
        _ => throw new IndexOutOfRangeException()
    };

    /// <summary>Gets all projections on the transforms.</summary>
    public IEnumerable<Point> Projections(IEnumerable<Vector> transforms)
    {
        var point = this;
        return transforms.Select(distance => point + distance);
    }

    public IEnumerable<Point> Repeat(Vector transform, bool includeCurrent = false)
        => new Repeater(includeCurrent ? this - transform : this, transform);

    /// <summary>Represents the point as a vector.</summary>
    public Vector Vector() => new(X, Y);

    /// <summary>Rotates this point around a center.</summary>
    /// <param name="other">
    /// The point to rotate around.
    /// </param>
    /// <param name="rotation">
    /// Rotation to perform.
    /// </param>
    public Point Rotate(Point other, DiscreteRotation rotation) => other + (this - other).Rotate(rotation);

    public Point FlipX(int axis = 0) => new(X, axis * 2 - Y);

    public Point FlipY(int axis = 0) => new(axis * 2 - X, Y);
    
    public int ManhattanDistance(Point other)
      => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    /// <inheritdoc />
    public override string ToString() => $"({X}, {Y})";

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is Point other && Equals(other);

    /// <inheritdoc />
    public bool Equals(Point other) => X == other.X && Y == other.Y;

    /// <inheritdoc />
    public override int GetHashCode() => X ^ (Y << 16);

    private Point Add(Vector vector) => new(X + vector.X, Y + vector.Y);

    private Point Subtract(Vector vector) => new(X - vector.X, Y - vector.Y);

    private Vector Subtract(Point other) => new(X - other.X, Y - other.Y);

    /// <summary>Compares two points.</summary>
    public static bool operator ==(Point a, Point b) => a.Equals(b);

    /// <summary>Compares two points.</summary>
    public static bool operator !=(Point a, Point b) => !(a == b);

    /// <summary>Adds a vector to a point.</summary>
    public static Point operator +(Point point, Vector vector) => point.Add(vector);
    /// <summary>Subtracts a vector from a point.</summary>
    public static Point operator -(Point point, Vector vector) => point.Subtract(vector);

    /// <summary>Calulates the vector distantce between two points.</summary>
    public static Vector operator -(Point point, Point other) => point.Subtract(other);

    public static Point Parse(string str)
    {
        var split = str.Split(',');
        return new Point(split[0].Int32(), split[1].Int32());
    }

    private struct Repeater : Iterator<Point>
    {
        public Repeater(Point point, Vector transform)
        {
            Current = point;
            Transform = transform;
        }
        public Point Current { get; private set; }

        private readonly Vector Transform;

        public bool MoveNext()
        {
            Current += Transform;
            return true;
        }

        public void Reset() => throw new NotSupportedException();
        public void Dispose() => Do.Nothing();
    }
}
