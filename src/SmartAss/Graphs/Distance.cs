using System.Diagnostics.CodeAnalysis;

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

    public override string ToString() => DebuggerDisplay.ToString();

    public override bool Equals([NotNullWhen(true)] object obj) => obj is Distance other && Equals(other);
    public bool Equals(Distance other) => other.Value == Value;
    public override int GetHashCode() => Value;

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
