using Qowaiv.Hashing;
using SmartAss.Collections;
using SmartAss.Conversion.Numerics;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace SmartAss.Numerics;

/// <summary>Represents both the value/remainder as the divisor of the modulo operation.</summary>
[TypeConverter(typeof(ModuloInt64TypeConverter))]
public readonly struct ModuloInt64 : IEquatable<ModuloInt64>, IFormattable,
    IAdditionOperators<ModuloInt64, ModuloInt64, ModuloInt64>,
    IAdditionOperators<ModuloInt64, long, ModuloInt64>,
    ISubtractionOperators<ModuloInt64, long, ModuloInt64>,
    IIncrementOperators<ModuloInt64>,
    IDecrementOperators<ModuloInt64>
{
    public ModuloInt64(long dividend, long divisor)
    {
        Value = unchecked((long)(dividend % divisor));
        if (Value < 0) { Value += divisor; }
        divisor_ = divisor - 1;
    }

    /// <summary>The value (or remainder).</summary>
    public long Value { get; }

    /// <summary>Gets all values.</summary>
    public IEnumerable<long> Values() => new Iterator(this);

    /// <summary>The divisor (or divider).</summary>
    /// 
    public long Divisor => divisor_ + 1;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly long divisor_;

    public bool IsZero => Value == 0;

    private ModuloInt64 Add(ModuloInt64 other)
    {
        var value = Values().First(v => v % other.Divisor == other.Value);
        return new(value, Maths.Lcm(Divisor, other.Divisor));
    }

    /// <inheritdoc />
    public static ModuloInt64 operator +(ModuloInt64 l, ModuloInt64 r) => l.Add(r);

    /// <inheritdoc />
    public static ModuloInt64 operator -(ModuloInt64 m, long value) => new(m.Value - value, m.Divisor);

    /// <inheritdoc />
    public static ModuloInt64 operator +(ModuloInt64 m, long value) => new(m.Value + value, m.Divisor);

    /// <inheritdoc />
    public static ModuloInt64 operator --(ModuloInt64 m) => m - 1;

    /// <inheritdoc />
    public static ModuloInt64 operator ++(ModuloInt64 m) => m + 1;

    /// <inheritdoc />
    [Pure]
    public override string ToString() => ToString("", CultureInfo.InvariantCulture);

    /// <inheritdoc />
    [Pure]
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{Value.ToString(format, formatProvider)} mod {Divisor.ToString(format, formatProvider)}";

    /// <inheritdoc/>
    [Pure]
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is ModuloInt64 other && Equals(other);

    /// <inheritdoc/>
    [Pure]
    public bool Equals(ModuloInt64 other) => Value == other.Value && Divisor == other.Divisor;

    /// <inheritdoc/>
    [Pure]
    public override int GetHashCode() => Hash.Code(Value).And(Divisor);

    public static ModuloInt64 Parse(string? s)
        => TryParse(s)
        ?? throw new FormatException("No valid modoli");

    public static ModuloInt64? TryParse(string? s)
        => s?.Split(' ') is { Length: 3 } parts
            && long.TryParse(parts[0], CultureInfo.InvariantCulture, out var value)
            && long.TryParse(parts[2], CultureInfo.InvariantCulture, out var molulo)
            && (parts[1] == "%" || parts[1].ToUpperInvariant() == "MOD")
                ? new(value, molulo)
                : null;

    private sealed class Iterator : Iterator<long>
    {
        public Iterator(ModuloInt64 modulus)
        {
            Current = modulus.Value - modulus.Divisor;
            Step = modulus.Divisor;
        }

        public long Current { get; private set; }
        private readonly long Step;
        

        public bool MoveNext()
        {
            Current += Step;
            return true;
        }

        public void Dispose() { /* Nothing to dispose */ }

        public void Reset() => throw new NotSupportedException();
    }
}
