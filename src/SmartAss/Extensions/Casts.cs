namespace System;

public static class SmartAssCasts
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Int(this long num) => (int)num;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Int(this double num) => (int)num;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Long(this double num) => (long)num;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Long(this int num) => (long)num;
}
