// <copyright file = "NumbersExtensions.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss;

namespace System;

/// <summary>Extensions on collections of numbers.</summary>
public static class NumbersExtensions
{
    public static int Product(this IEnumerable<int> numbers)
    {
        Guard.NotNull(numbers, nameof(numbers));

        var product = 1;
        foreach (var number in numbers)
        {
            if (number == 0) return 0;
            product *= number;
        }
        return product;
    }

    public static long Product(this IEnumerable<long> numbers)
    {
        Guard.NotNull(numbers, nameof(numbers));

        long product = 1;
        foreach (var number in numbers)
        {
            product *= number;
        }
        return product;
    }

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

    public static int Sum(this IEnumerable<byte> numbers)
    {
        Guard.NotNull(numbers, nameof(numbers));

        var sum = 0;

        foreach (var number in numbers)
        {
            sum += number;
        }
        return sum;
    }
}
