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
        public static bool ToInt32(string str, out int n)
        {
            n = default;
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            var start = 0;
            var end = str.Length;
            var number = 0;
            var negative = false;

            if (str[0] == '-')
            {
                start = 1;
                negative = true;
            }

            for (var i = start; i < end; i++)
            {
                var ch = str[i];

                if (ch < '0' || ch > '9')
                {
                    return false;
                }

                unchecked
                {
                    number *= 10;
                    number += ch - '0';
                }

                // becomes negative, so overflow.
                if ((number & 0x80000000) != 0)
                {
                    return false;
                }
            }

            n = negative ? -number : number;
            return true;
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
        public static bool ToInt64(string str, out long n)
        {
            n = default;
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            var start = 0;
            var end = str.Length;
            var number = 0L;
            var negative = false;

            if (str[0] == '-')
            {
                start = 1;
                negative = true;
            }

            for (var i = start; i < end; i++)
            {
                var ch = str[i];

                if (ch < '0' || ch > '9')
                {
                    return false;
                }

                unchecked
                {
                    number *= 10;
                    number += ch - '0';
                }

                if ((number & unchecked((long)0x8000000000000000)) != 0)
                {
                    return false;
                }
            }

            n = negative ? -number : number;
            return true;
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
        public static bool ToDouble(string str, out double n)
        {
            n = default;
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            unchecked
            {
                var start = 0;

                var negative = false;
                int scale = int.MinValue;

                ulong buffer = 0;

                if (str[0] == '-')
                {
                    start = 1;
                    negative = true;
                }

                for (var i = start; i < str.Length; i++)
                {
                    var ch = str[i];
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

                        return false;
                    }

                    uint digit = (uint)(ch - '0');

                    buffer *= 10;
                    buffer += digit;

                    if ((buffer & 0x8000000000000000) != 0)
                    {
                        return false;
                    }
                }

                n = buffer;
                if (scale > 0)
                {
                    n /= Deviders[scale];
                }

                if (negative)
                {
                    n = -n;
                }

                return true;
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
        public static bool ToDecimal(string str, out decimal dec)
        {
            dec = default;
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            unchecked
            {
                var start = 0;

                var negative = false;
                int scale = int.MinValue;

                ulong b0 = 0;
                ulong b1 = 0;

                if (str[0] == '-')
                {
                    start = 1;
                    negative = true;
                }

                for (var i = start; i < str.Length; i++)
                {
                    var ch = str[i];

                    // Not a digit.
                    if (ch < '0' || ch > '9')
                    {
                        // if a dot and not found yet.
                        if (ch == '.' && scale < 0)
                        {
                            scale = 0;
                            continue;
                        }

                        return false;
                    }

                    // Precision does not fit.
                    if (scale++ >= 28)
                    {
                        return false;
                    }

                    uint digit = (uint)(ch - '0');

                    b0 *= 10;
                    b1 *= 10;

                    b0 += digit;

                    // add overflow
                    b1 += b0 >> 48;

                    if ((b1 & ~DecimalMask) != 0)
                    {
                        return false;
                    }

                    // clear overflow.
                    b0 &= DecimalMask;
                }

                var lo = (int)b0;
                var mi = (int)((b1 << 16) | (b0 >> 32));
                var hi = (int)(b1 >> 16);

                dec = new decimal(lo, mi, hi, negative, scale < 0 ? default : (byte)scale);

                return true;
            }
        }
    }
}
