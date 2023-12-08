// <copyright file = "NumberParsing.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
using System;
using System.Collections;
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
        private static readonly string[] Splitters = new[] { " ", ",", "\r\n", "\n", "\t" };


        /// <summary>Gets the <see cref="int"/> value of the <see cref="string"/>.</summary>
        public static int Int32(this string str) => str.Int32N() ?? 0;

        /// <summary>Gets the <see cref="int"/> value of the <see cref="string"/>.</summary>
        public static int? Int32N(this string str)
        {
            var interator = new Int32sParser(str);
            return interator.MoveNext() ? interator.Current : null;
        }

        /// <summary>Gets the <see cref="int"/> value of the <see cref="string"/>.</summary>
        public static int TryInt32(this string str, int fallback) => str.Int32N() ?? fallback;

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
        public static IEnumerable<int> Int32s(this IEnumerable<string> strings) => strings.SelectMany(Int32s);

        /// <summary>Gets the <see cref="int"/> values of the <see cref="string"/>.</summary>
        public static IEnumerable<int> Int32s(this string str) => new Int32sParser(str);

        /// <summary>Gets the <see cref="long"/> values of the <see cref="string"/>.</summary>
        public static IEnumerable<long> Int64s(this string str) => new Int64sParser(str);

        /// <summary>Gets the <see cref="long"/> values of the <see cref="string"/>.</summary>
        public static IEnumerable<long> Int64s(this IEnumerable<string> strings) => strings.SelectMany(Int64s);

        /// <summary>Gets the <see cref="BigInteger"/> values of the <see cref="string"/>.</summary>
        public static IEnumerable<BigInteger> BigInts(this string str)
            => str.Split(Splitters, SplitOptions)
            .Select(n => BigInt(n));

        private struct Int32sParser : IEnumerator<int>, IEnumerable<int>
        {
            private readonly string str;
            private int pos = -1;

            public Int32sParser(string s)
            {
                str = s;
                Current = 0;
            }

            public int Current { get; private set; }
            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                var any = false;
                var negative = false;
                Current = 0;
                while (++pos < str.Length)
                {
                    var ch = str[pos];
                    if (ch >= '0' && ch <= '9')
                    {
                        negative |= !any && pos != 0 && str[pos - 1] == '-';
                        any = true;
                        Current *= 10;
                        Current += negative ? -(ch - '0') : ch - '0';
                    }
                    else if (any) { return true; }
                }
                return any;
            }

            public void Reset() => Do.Nothing();
            public IEnumerator<int> GetEnumerator() => this;
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            public void Dispose() => Do.Nothing();
        }

        private struct Int64sParser : IEnumerator<long>, IEnumerable<long>
        {
            private readonly string str;
            private int pos = -1;

            public Int64sParser(string s)
            {
                str = s;
                Current = 0;
            }

            public long Current { get; private set; }
            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                var any = false;
                var negative = false;
                Current = 0;
                while (++pos < str.Length)
                {
                    var ch = str[pos];
                    if (ch >= '0' && ch <= '9')
                    {
                        negative |= !any && pos != 0 && str[pos - 1] == '-';
                        any = true;
                        Current *= 10;
                        Current += negative ? -(ch - '0') : ch - '0';
                    }
                    else if (any) { return true; }
                }
                return any;
            }

            public void Reset() => Do.Nothing();
            public IEnumerator<long> GetEnumerator() => this;
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            public void Dispose() => Do.Nothing();
        }
    }
}
