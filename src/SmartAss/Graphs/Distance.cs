namespace SmartAss.Graphs;

[DebuggerDisplay("{DebuggerDisplay}")]
public readonly struct Distance : IEquatable<Distance>
{
    private const int Mask = int.MaxValue;

    public static readonly Distance Unknown;
    public static readonly Distance Zero = new(Mask);
    public static readonly Distance One = new(1 ^ Mask);
    public static readonly Distance Infinite = new(int.MaxValue - 1 ^ Mask);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int Value;

    private Distance(int value) => Value = value;

    [Pure]
    public override string ToString() => DebuggerDisplay.ToString() ?? string.Empty;

    [Pure]
    public override bool Equals(object? obj) => obj is Distance other && Equals(other);

    [Pure]
    public bool Equals(Distance other) => other.Value == Value;

    [Pure]
    public override int GetHashCode() => Value;

    [Pure]
    public int Int() => Value ^ Mask;

    public static bool operator ==(Distance left, Distance right) => left.Equals(right);

    public static bool operator !=(Distance left, Distance right) => !(left == right);

    public static Distance operator ++(Distance distance) => new(distance.Value - 1);

    public static Distance operator +(Distance distance, int add) => new(distance.Value - add);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private object DebuggerDisplay
    {
        get
        {
            if (Value == Unknown.Value) { return "?"; }
            if (Value == Infinite.Value) { return "oo"; }
            return Value ^ Mask;
        }
    }
}
