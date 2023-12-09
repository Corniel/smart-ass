// <copyright file = "Maths.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SmartAss
{
    /// <summary>Math operations not in <see cref="Math"/>.</summary>
    public static class Maths
    {
       /// <inheritdoc cref="Gcd(long, long)" />
        public static int Gcd(int a, int b) => unchecked((int)Gcd(a.Long(), b.Long()));

        /// <summary>Gets the Greatest Common Divisor.</summary>
        /// <remarks>
        /// See: https://en.wikipedia.org/wiki/Greatest_common_divisor.
        /// </remarks>
        public static long Gcd(long a, long b)
        {
            var even = 1;
            long remainder;
            // while both are even.
            while ((a & 1) == 0 && (b & 1) == 0)
            {
                a >>= 1;
                b >>= 1;
                even <<= 1;
            }

            while (b != 0)
            {
                remainder = a % b;
                a = b;
                b = remainder;
            }

            return a * even;
        }
    }
}
