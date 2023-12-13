// <copyright file = "Maths.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System;
using System.Numerics;

namespace SmartAss
{
    /// <summary>Math operations not in <see cref="Math"/>.</summary>
    public static class Maths
    {
        public static long Pow(this long n, long power) => Math.Pow(n, power).Long();

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

        /// <summary>Calculates all combinations when choosing k out n.</summary>
        /// <remarks>
        /// Making the most use of the standard algorithm from Knuth's book "The Art of Computer Programming, 3rd Edition, Volume 2: Seminumerical Algorithms":
        /// </remarks>
        public static long Choose(long n, long k)
        {
            var n_k = n - k;

            // swap as n over k equals n over (n - k)
            if (n_k < k)
            {
                k = n_k;
            }

            long r = 1;
            long d;
            for (d = 1; d <= k; ++d)
            {
                if (r > long.MaxValue / n)
                    break;
                r *= n--;
                r /= d;
            }

            if (d > k)
                return r;

            // Let N be the original n,
            // n is the current n (when we reach here)
            // We want to calculate C(N,k),
            // Currently we already calculated the r value so far:
            // r = C(N, n) = C(N, N-n) = C(N, d-1)
            // Note that N-n = d-1
            // In addition we know the following identity formula:
            //  C(N,k) = C(N,d-1) * C(N-d+1, k-d+1) / C(k, k-d+1)
            //         = C(N,d-1) * C(n, k-d+1) / C(k, k-d+1)
            // Using this formula, we effectively reduce the calculation,
            // while recursively use the same function.
            long b = Choose(n, k - d + 1);
            if (b == long.MaxValue)
            {
                return long.MaxValue;  // overflow
            }

            long c = Choose(k, k - d + 1);
            if (c == long.MaxValue)
            {
                return long.MaxValue;  // overflow
            }

            // Now, the combinatorial should be r * b / c
            // We can use gcd() to calculate this:
            // We Pick b for gcd: b < r almost (if not always) in all cases
            long g = Gcd(b, c);
            b /= g;
            c /= g;
            r /= c;

            if (r > long.MaxValue / b)
                return long.MaxValue;   // overflow

            return r * b;
        }

        public static long Challanger(long n, long k)
        {
            var n_k = n - k;

            // swap as n over k equals n over (n - k)
            if (n_k < k) { k = n_k; }

            if (k == 0) { return 1; }
            
            var combinations = n--;

            for (var i = 2; i < k + 1; i++, n--)
            {
                combinations = combinations * n / i;
            }
            return combinations;
        }
    }
}
