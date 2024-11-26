namespace System;

public static class SmartAssCasts
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static int Int(this long num) => (int)num;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static int Int(this double num) => (int)num;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static long Long(this double num) => (long)num;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [Pure]
    public static long Long(this int num) => (long)num;
}
