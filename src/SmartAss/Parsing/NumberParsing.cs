// <copyright file = "NumberParsing.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

#pragma warning disable S3900 // Arguments of public methods should be validated against null
// intended for simple parsing

namespace SmartAss.Parsing
{
    public static class NumberParsing
    {
        private const StringSplitOptions SplitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
        private static readonly string[] Splitters = new[] { ",", "\r\n", "\n", "\t" };

        /// <summary>Gets the <see cref="int"/> value of the <see cref="string"/>.</summary>
        public static int Int32(this string str) => TryInt32(str, default);

        /// <summary>Gets the <see cref="int"/> value of the <see cref="string"/>.</summary>
        public static int TryInt32(this string str, int? fallback)
        {
            if (int.TryParse(str, out var n)) { return n; }
            else if (fallback.HasValue) { return fallback.Value; }
            else { throw new FormatException($"'{str}' is not a number"); }
        }

        /// <summary>Gets the <see cref="long"/> value of the <see cref="string"/>.</summary>
        public static long Int64(this string str, long? fallback = null)
        {
            if (long.TryParse(str, out var n)) { return n; }
            else if (fallback.HasValue) { return fallback.Value; }
            else { throw new FormatException($"'{n}' is not a number"); }
        }

        /// <summary>Gets the <see cref="long"/> value of the <see cref="string"/>.</summary>
        public static ulong UInt64(this string str, ulong? fallback = null)
        {
            if (ulong.TryParse(str, out var n)) { return n; }
            else if (fallback.HasValue) { return fallback.Value; }
            else { throw new FormatException($"'{n}' is not a number"); }
        }

        /// <summary>Gets the <see cref="BigInteger"/> value of the <see cref="string"/>.</summary>
        public static BigInteger BigInt(this string str, BigInteger? fallback = null)
        {
            if (BigInteger.TryParse(str, out var n)) { return n; }
            else if (fallback.HasValue) { return fallback.Value; }
            else { throw new FormatException($"'{n}' is not a number"); }
        }

        /// <summary>Gets the <see cref="int"/> values of the <see cref="string"/>.</summary>
        public static IEnumerable<int> Int32s(this string str)
            => str.Split(Splitters, SplitOptions)
            .Select(n => Int32(n));

        /// <summary>Gets the <see cref="long"/> values of the <see cref="string"/>.</summary>
        public static IEnumerable<long> Int64s(this string str)
            => str.Split(Splitters, SplitOptions)
            .Select(n => Int64(n));

        /// <summary>Gets the <see cref="BigInteger"/> values of the <see cref="string"/>.</summary>
        public static IEnumerable<BigInteger> BigInts(this string str)
            => str.Split(Splitters, SplitOptions)
            .Select(n => BigInt(n));
    }
}
