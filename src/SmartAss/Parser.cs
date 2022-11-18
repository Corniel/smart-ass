// <copyright file = "Parser.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SmartAss
{
    /// <summary>A parser of primitives, with the most limited support.</summary>
    public static class Parser
    {
        private const ulong DecimalMask = 0xFFFFFFFFFFFF;

        private static readonly double[] Deviders = new[]
        {
            1d,
            10d,
            100d,
            1000d,
            10000d,
            100000d,
            1000000d,
            10000000d,
            100000000d,
            1000000000d,
            10000000000d,
            100000000000d,
            1000000000000d,
            10000000000000d,
            100000000000000d,
            1000000000000000d,
            10000000000000000d,
            100000000000000000d,
            1000000000000000000d,
            10000000000000000000d,
            100000000000000000000d,
        };

        /// <summary>Parses a int.</summary>
        /// <param name="str">
        /// The input string.
        /// </param>
        /// <param name="n">
        /// The parsed int.
        /// </param>
        /// <returns>
        /// True if parsable, otherwise false.
        /// </returns>
        public static int? ToInt32(ReadOnlySpan<char> str)
        {
            if (str.Length == 0) return null;

            var number = 0;
            var negative = false;

            if (str[0] == '-')
            {
                str = str[1..];
                negative = true;
            }
            foreach (var ch in str)
            { 
                if (ch < '0' || ch > '9') return null;

                unchecked
                {
                    number *= 10;
                    number += ch - '0';
                }
                // becomes negative, so overflow.
                if ((number & 0x80000000) != 0) return null;
            }
            return negative ? -number : number;
        }

        /// <summary>Parses a long.</summary>
        /// <param name="str">
        /// The input string.
        /// </param>
        /// <param name="n">
        /// The parsed long.
        /// </param>
        /// <returns>
        /// True if parsable, otherwise false.
        /// </returns>
        public static long? ToInt64(ReadOnlySpan<char> str)
        {
            if (str.Length == 0) return null;

            var number = 0L;
            var negative = false;

            if (str[0] == '-')
            {
                str = str[1..];
                negative = true;
            }

            foreach(var ch in str) 
            {
                if (ch < '0' || ch > '9') return null;
                unchecked
                {
                    number *= 10;
                    number += ch - '0';
                }
                if ((number & unchecked((long)0x8000000000000000)) != 0) return null;
            }
            return negative ? -number : number;
        }

        /// <summary>Parses a double.</summary>
        /// <param name="str">
        /// The input string.
        /// </param>
        /// <param name="n">
        /// The parsed double.
        /// </param>
        /// <returns>
        /// True if parsable, otherwise false.
        /// </returns>
        public static double? ToDouble(ReadOnlySpan<char> str)
        {
            if (str.Length == 0) return null;

            unchecked
            {
                var negative = false;
                int scale = int.MinValue;
                ulong buffer = 0;

                if (str[0] == '-')
                {
                    negative = true;
                    str = str[1..];
                }
                foreach (var ch in str)
                {
                    scale++;

                    // Not a digit.
                    if (ch < '0' || ch > '9')
                    {
                        // if a dot and not found yet.
                        if (ch == '.' && scale < 0)
                        {
                            scale = 0;
                            continue;
                        }
                        else return null;
                    }

                    buffer *= 10;
                    buffer += (ulong)(ch - '0');

                    if ((buffer & 0x8000000000000000) != 0) return null;
                }

                var n = scale > 0 ? buffer / Deviders[scale] : buffer;
                return negative ? -n : +n;
            }
        }

        /// <summary>Parses a decimal.</summary>
        /// <param name="str">
        /// The input string.
        /// </param>
        /// <param name="dec">
        /// The parsed decimal.
        /// </param>
        /// <returns>
        /// True if parsable, otherwise false.
        /// </returns>
        public static decimal? ToDecimal(ReadOnlySpan<char> str)
        {
            if (str.Length == 0) return null;

            unchecked
            {
                var negative = false;
                int scale = int.MinValue;
                ulong b0 = 0;
                ulong b1 = 0;

                if (str[0] == '-')
                {
                    str= str[1..];
                    negative= true;
                }

                foreach(var ch in str)
                { 
                    // Not a digit.
                    if (ch < '0' || ch > '9')
                    {
                        // if a dot and not found yet.
                        if (ch == '.' && scale < 0)
                        {
                            scale = 0;
                            continue;
                        }
                        else return null;
                    }

                    // Precision does not fit.
                    if (scale++ >= 28) return null;

                    b0 *= 10;
                    b1 *= 10;
                    b0 += (uint)(ch - '0');

                    // add overflow
                    b1 += b0 >> 48;

                    if ((b1 & ~DecimalMask) != 0) return null;
                    // clear overflow.
                    b0 &= DecimalMask;
                }

                var lo = (int)b0;
                var mi = (int)((b1 << 16) | (b0 >> 32));
                var hi = (int)(b1 >> 16);

                return new decimal(lo, mi, hi, negative, scale < 0 ? default : (byte)scale);
            }
        }
    }
}
