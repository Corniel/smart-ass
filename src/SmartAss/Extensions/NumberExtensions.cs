// <copyright file = "NumberExtensions.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Numerics;

namespace System;

public static class NumberExtensions
{
    /// <summary>Gets the square of the number.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Abs<T>(this T number) where T : struct, INumber<T>
        => number < T.Zero ? -number : +number;

    /// <summary>Gets the square of the number.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sign<T>(this T number) where T : struct, INumber<T>
    {
        if (number == T.Zero) return 0;
        else return number > T.Zero ? +1 : -1;
    }

    /// <summary>Gets the square of the number.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Sqr<T>(this T number) where T : struct, INumberBase<T>
        => number * number;

    /// <summary>Gets the discreet value of the square root of the number.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Sqrt(this double number) => Math.Sqrt(number);

    /// <summary>Gets the discreet value of the square root of the number.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Sqrt(this int number) => Math.Sqrt(number);

    /// <summary>Gets the discreet value of the square root of the number.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Sqrt(this long number) => Math.Sqrt(number);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Floor(this double number) => (int)Math.Floor(number);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Round(this double number) => (int)Math.Round(number);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Ceil(this double number) => (int)Math.Ceiling(number);
}
