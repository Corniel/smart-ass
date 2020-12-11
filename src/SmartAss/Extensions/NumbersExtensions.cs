// <copyright file = "NumbersExtensions.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss;
using System.Collections.Generic;

namespace System
{
    /// <summary>Extensions on collections of numbers.</summary>
    public static class NumbersExtensions
    {
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
    }
}
