// <copyright file = "Maths.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Numerics;

namespace SmartAss
{
    /// <summary>Math operations not in <see cref="Math"/>.</summary>
    public static class Maths
    {
        /// <summary>Gets the Greatest Common Divisor.</summary>
        /// <remarks>
        /// See: https://en.wikipedia.org/wiki/Greatest_common_divisor.
        /// </remarks>
        public static TNumber Gcd<TNumber>(TNumber a, TNumber b)
            where TNumber : struct,
            INumber<TNumber>,
            IBitwiseOperators<TNumber, TNumber, TNumber>,
            IShiftOperators<TNumber, int, TNumber>
        {
            var even = TNumber.One;

            while (a.IsEven() && b.IsEven())
            {
                a >>= 1;
                b >>= 1;
                even <<= 1;
            }
            while (b != TNumber.Zero)
            {
                (a, b) = (b, a % b);
            }
            return a * even;
        }

        /// <summary>Gets the Least Common Multiple .</summary>
        public static TNumber Lcm<TNumber>(TNumber a, TNumber b)
            where TNumber : struct,
            INumber<TNumber>,
            IBitwiseOperators<TNumber, TNumber, TNumber>,
            IShiftOperators<TNumber, int, TNumber>

            => a * b / Gcd(a, b);

        /// <summary>Gets the Least Common Multiple .</summary>
        public static TNumber Lcm<TNumber>(IEnumerable<TNumber> numbers)
            where TNumber : struct,
            INumber<TNumber>,
            IBitwiseOperators<TNumber, TNumber, TNumber>,
            IShiftOperators<TNumber, int, TNumber>
        {
            var iterator = numbers.GetEnumerator();

            if (!iterator.MoveNext())
            {
                throw new ArgumentException("The sequence contains no items.", nameof(numbers));
            }

            var lcm = iterator.Current;

            while (iterator.MoveNext())
            {
                lcm = Lcm(lcm, iterator.Current);
            }
            return lcm;
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
    }
}
