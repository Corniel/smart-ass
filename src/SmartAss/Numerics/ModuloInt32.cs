using Qowaiv.Hashing;
using SmartAss.Collections;
using SmartAss.Conversion.Numerics;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace SmartAss.Numerics;

/// <summary>Represents both the value/remainder as the divisor of the modulo operation.</summary>
[TypeConverter(typeof(ModuloInt32TypeConverter))]
public readonly struct ModuloInt32 : IEquatable<ModuloInt32>, IFormattable,
    IAdditionOperators<ModuloInt32, ModuloInt32, ModuloInt32>,
    IAdditionOperators<ModuloInt32, int, ModuloInt32>,
    ISubtractionOperators<ModuloInt32, int, ModuloInt32>,
    IIncrementOperators<ModuloInt32>,
    IDecrementOperators<ModuloInt32>
{
    public ModuloInt32(long dividend, int divisor)
    {
        Value = unchecked((int)(dividend % divisor));
        if (Value < 0) { Value += divisor; }
        divisor_ = divisor - 1;
    }

    /// <summary>The value (or remainder).</summary>
    public int Value { get; }

    /// <summary>Gets all values.</summary>
    public IEnumerable<int> Values() => new Iterator(this);

    /// <summary>The divisor (or divider).</summary>
    /// 
    public int Divisor => divisor_ + 1;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly int divisor_;

    public bool IsZero => Value == 0;

    private ModuloInt32 Add(ModuloInt32 other)
    {
        var value = Values().First(v => v % other.Divisor == other.Value);
        var gcd = Maths.Gcd(Divisor, other.Divisor);
        return new(value, Divisor * other.Divisor / gcd);
    }

    /// <inheritdoc />
    public static ModuloInt32 operator +(ModuloInt32 l, ModuloInt32 r) => l.Add(r);

    /// <inheritdoc />
    public static ModuloInt32 operator -(ModuloInt32 m, int value) => new(m.Value - value, m.Divisor);

    /// <inheritdoc />
    public static ModuloInt32 operator +(ModuloInt32 m, int value) => new(m.Value + value, m.Divisor);

    /// <inheritdoc />
    public static ModuloInt32 operator --(ModuloInt32 m) => m - 1;

    /// <inheritdoc />
    public static ModuloInt32 operator ++(ModuloInt32 m) => m + 1;

    /// <inheritdoc />
    [Pure]
    public override string ToString() => ToString("", CultureInfo.InvariantCulture);

    /// <inheritdoc />
    [Pure]
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{Value.ToString(format, formatProvider)} mod {Divisor.ToString(format, formatProvider)}";

    /// <inheritdoc/>
    [Pure]
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is ModuloInt32 other && Equals(other);

    /// <inheritdoc/>
    [Pure]
    public bool Equals(ModuloInt32 other) => Value == other.Value && Divisor == other.Divisor;

    /// <inheritdoc/>
    [Pure]
    public override int GetHashCode() => Hash.Code(Value).And(Divisor);

    public static ModuloInt32 Parse(string? s)
        => TryParse(s)
        ?? throw new FormatException("No valid modoli");

    public static ModuloInt32? TryParse(string? s)
        => s?.Split(' ') is { Length: 3 } parts
            && int.TryParse(parts[0], CultureInfo.InvariantCulture, out var value)
            && int.TryParse(parts[2], CultureInfo.InvariantCulture, out var molulo)
            && (parts[1] == "%" || parts[1].ToUpperInvariant() == "MOD")
                ? new(value, molulo)
                : null;

    private sealed class Iterator : Iterator<int>
    {
        public Iterator(ModuloInt32 modulus)
        {
            Current = modulus.Value - modulus.Divisor;
            Step = modulus.Divisor;
        }

        public int Current { get; private set; }
        private readonly int Step;
        

        public bool MoveNext()
        {
            Current += Step;
            return true;
        }

        public void Dispose() { /* Nothing to dispose */ }

        public void Reset() => throw new NotSupportedException();
    }
}
