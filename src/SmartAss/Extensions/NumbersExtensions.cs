// <copyright file = "NumbersExtensions.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss;
using System.Numerics;

namespace System;

/// <summary>Extensions on collections of numbers.</summary>
public static class NumbersExtensions
{
    [Pure]
    public static TNumber Product<TNumber>(this IEnumerable<TNumber> numbers) where TNumber : struct, INumberBase<TNumber>
    {
        Guard.NotNull(numbers, nameof(numbers));

        TNumber product = TNumber.One;

        foreach (var number in numbers)
        {
            if (number == TNumber.Zero)
            {
                return TNumber.Zero;
            }
            product *= number;
        }
        return product;
    }

    [Pure]
    public static TNumber Product<TSource, TNumber>(this IEnumerable<TSource> source, Func<TSource, TNumber> selector) where TNumber : struct, INumberBase<TNumber>
        => source.Select(selector).Product();

    [Pure]
    public static ulong Sum(this IEnumerable<ulong> numbers)
    {
        Guard.NotNull(numbers, nameof(numbers));

        ulong sum = 0;

        foreach (var number in numbers)
        {
            sum += number;
        }
        return sum;
    }
}
