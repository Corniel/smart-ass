using SmartAss.Numerics;

namespace SmartAss;

public static class Humanizer
{
    /// <summary>Gets a (positive) modulo.</summary>
    [Pure]
    public static int Mod(this int n, int modulo)
    {
        var m = n % modulo;
        return m < 0 ? m + modulo : m;
    }

    /// <summary>Gets a (positive) modulo.</summary>
    [Pure]
    public static long Mod(this long n, long modulo)
    {
        var m = n % modulo;
        return m < 0 ? m + modulo : m;
    }

    [Pure]
    public static int Mod(this int n, ModuloInt32 modulo)
        => (n - modulo.Value).Mod(modulo.Divisor);

    /// <summary>Creates a <see cref="ModuloInt32"/>.</summary>
    [Pure]
    public static ModuloInt32 Modulo(this int dividend, int divisor) => new(dividend, divisor);

    /// <summary>Creates a <see cref="ModuloInt32"/>.</summary>
    [Pure]
    public static ModuloInt32 Modulo(this long dividend, int divisor) => new(dividend, divisor);

    /// <summary>Creates a <see cref="ModuloInt64"/>.</summary>
    [Pure]
    public static ModuloInt64 Modulo(this int dividend, long divisor) => new(dividend, divisor);

    /// <summary>Creates a <see cref="ModuloInt64"/>.</summary>
    [Pure]
    public static ModuloInt64 Modulo(this long dividend, long divisor) => new(dividend, divisor);
}
