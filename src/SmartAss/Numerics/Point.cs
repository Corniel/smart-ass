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
        _ => throw new IndexOutOfRangeException(),
    };

    /// <summary>Deconstructs the point.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }

    /// <summary>Gets all projections on the transforms.</summary>
    [Pure]
    public IEnumerable<Point> Projections(IEnumerable<Vector> transforms)
    {
        var point = this;
        return transforms.Select(distance => point + distance);
    }

    [Pure]
    public Repeater Repeat(Vector transform, bool includeCurrent = false)
        => new(includeCurrent ? this - transform : this, transform);

    /// <summary>Represents the point as a vector.</summary>
    [Pure]
    public Vector Vector() => new(X, Y);

    /// <summary>Rotates this point around a center.</summary>
    /// <param name="other">
    /// The point to rotate around.
    /// </param>
    /// <param name="rotation">
    /// Rotation to perform.
    /// </param>
    [Pure]
    public Point Rotate(Point other, DiscreteRotation rotation) => other + (this - other).Rotate(rotation);

    [Pure]
    public Point FlipX(int axis = 0) => new(X, axis * 2 - Y);

    [Pure]
    public Point FlipY(int axis = 0) => new(axis * 2 - X, Y);

    [Pure]
    public int ManhattanDistance(Point other)
        => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);

    /// <summary>
    /// Gets all points that have exactly the specified Manhattan distance to
    /// this point.
    /// </summary>
    [Pure]
    public ManhattanDistances OnManhattanDistance(int distance) => new(this, distance);

    /// <inheritdoc />
    [Pure]
    public override string ToString() => $"({X}, {Y})";

    /// <inheritdoc />
    [Pure]
    public override bool Equals(object? obj) => obj is Point other && Equals(other);

    /// <inheritdoc />
    [Pure]
    public bool Equals(Point other) => X == other.X && Y == other.Y;

    /// <inheritdoc />
    [Pure]
    public override int GetHashCode() => X ^ (Y << 16);

    [Pure]
    private Point Add(Vector vector) => new(X + vector.X, Y + vector.Y);

    [Pure]
    private Point Subtract(Vector vector) => new(X - vector.X, Y - vector.Y);

    [Pure]
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

    /// <summary>Implicitly casts from a tuple to a point.</summary>
    /// <remarks>
    /// Allows: Point p = (0, 3).
    /// </remarks>
    public static implicit operator Point((int X, int Y) t) => new(t.X, t.Y);

    [Pure]
    public static Point Parse(string str)
    {
        var split = str.Split(',');
        return new Point(split[0].Int32(), split[1].Int32());
    }

    /// <summary>Represents all points on a specified Manhattan distance.</summary>
    /// <remarks>
    /// Distance: 3
    ///
    /// ....0.....
    /// ...b.1....
    /// ..a...2...
    /// .9..#..3..
    /// ..8...4...
    /// ...7.5....
    /// ....6.....
    /// </remarks>
    public struct ManhattanDistances : Iterator<Point>, IReadOnlyCollection<Point>
    {
        internal ManhattanDistances(Point point, int distance)
        {
            Point = point;
            Distance = distance;
            Count = distance << 2;
            Half = distance << 1;
            i = -1;
        }

        /// <inheritdoc />
        public int Count { get; }

        private readonly Point Point;
        private readonly int Distance;
        private readonly int Half;
        private int i;

        /// <inheritdoc />
        public Point Current => new(Point.X + Delta(i), Point.Y + Delta((i + Distance) % Count));

        /// <inheritdoc />
        [Impure]
        public bool MoveNext() => ++i < Count;

        /// <inheritdoc />
        public void Reset() => throw new NotSupportedException();

        /// <summary>Gets the delta to apply.</summary>
        /// remarks>
        /// 0, 1, .., n-1, n, n-1, .., 1, 0, -1, .. -n, .., -1, 0.
        /// </remarks>
        [Pure]
        private int Delta(int p)
        {
            var d = p <= Half ? p : Count - p;
            return d - Distance;
        }

        /// <inheritdoc />
        public void Dispose() { /* Nothing to dispose */ }
    }

    public struct Repeater : Iterator<Point>
    {
        internal Repeater(Point point, Vector transform)
        {
            Current = point;
            Transform = transform;
        }

        public Point Current { get; private set; }

        private readonly Vector Transform;

        [Impure]
        public bool MoveNext()
        {
            Current += Transform;
            return true;
        }

        public void Reset() => throw new NotSupportedException();

        public void Dispose() => Do.Nothing();
    }
}
