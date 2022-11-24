// <copyright file = "NumberExtensions.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace System;

public static class NumberExtensions
{
    public static IReadOnlyList<byte> Digits(this int number)
    {
        var digits = new byte[10];
        var pos = 10;
        var remainder = number;

        while (remainder != 0) 
        {
            digits[--pos] = unchecked((byte)(remainder % 10));
            remainder = remainder / 10;
        }
        return digits[pos..];
    }
}
