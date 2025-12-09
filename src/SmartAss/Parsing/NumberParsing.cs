// <copyright file = "NumberParsing.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
using System.Numerics;

#pragma warning disable S3900 // Arguments of public methods should be validated against null
// intended for simple parsing

namespace SmartAss.Parsing;

public static class NumberParsing
{
    private const StringSplitOptions SplitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
    private static readonly string[] Splitters = [" ", ",", "\r\n", "\n", "\t"];

    /// <summary>Gets the <see cref="int"/> value of the <see cref="string"/>.</summary>
    [Pure]
    public static int Int32(this string str)
    {
        var interator = new Int32sParser(str);
        interator.MoveNext();
        return interator.Current;
    }

    /// <summary>Gets the <see cref="int"/> value of the <see cref="string"/>.</summary>
    [Pure]
    public static int? Int32N(this string str)
    {
        var interator = new Int32sParser(str);
        return interator.MoveNext() ? interator.Current : null;
    }

    /// <summary>Gets the <see cref="int"/> value of the <see cref="string"/>.</summary>
    [Pure]
    public static int TryInt32(this string str, int fallback) => str.Int32N() ?? fallback;

    /// <summary>Gets the <see cref="long"/> value of the <see cref="string"/>.</summary>
    [Pure]
    public static long Int64(this string str, long? fallback = null)
    {
        if (long.TryParse(str, out var n)) { return n; }
        else if (fallback.HasValue) { return fallback.Value; }
        else { throw new FormatException($"'{n}' is not a number"); }
    }

    /// <summary>Gets the <see cref="long"/> value of the <see cref="string"/>.</summary>
    [Pure]
    public static long? Int64N(this string str)
    {
        var interator = new Int64sParser(str);
        return interator.MoveNext() ? interator.Current : null;
    }

    /// <summary>Gets the <see cref="intlong/> value of the <see cref="string"/>.</summary>
    [Pure]
    public static long TryInt64(this string str, int fallback) => str.Int64N() ?? fallback;

    /// <summary>Gets the <see cref="long"/> value of the <see cref="string"/>.</summary>
    [Pure]
    public static ulong UInt64(this string str, ulong? fallback = null)
    {
        if (ulong.TryParse(str, out var n)) { return n; }
        else if (fallback.HasValue) { return fallback.Value; }
        else { throw new FormatException($"'{n}' is not a number"); }
    }

    /// <summary>Gets the <see cref="BigInteger"/> value of the <see cref="string"/>.</summary>
    [Pure]
    public static BigInteger BigInt(this string str, BigInteger? fallback = null)
    {
        if (BigInteger.TryParse(str, out var n)) { return n; }
        else if (fallback.HasValue) { return fallback.Value; }
        else { throw new FormatException($"'{n}' is not a number"); }
    }

    /// <summary>Gets the <see cref="int"/> values of the <see cref="string"/>.</summary>
    [Pure]
    public static Int32sParser Int32s(this string str) => new(str);

    /// <summary>Gets the <see cref="int"/> values of the <see cref="string"/>.</summary>
    [Pure]
    public static IEnumerable<int> Int32s(this IEnumerable<string> strings) => strings.SelectMany(s => s.Int32s());

    /// <summary>Gets the <see cref="long"/> values of the <see cref="string"/>.</summary>
    [Pure]
    public static Int64sParser Int64s(this string str) => new(str);

    /// <summary>Gets the <see cref="long"/> values of the <see cref="string"/>.</summary>
    [Pure]
    public static IEnumerable<long> Int64s(this IEnumerable<string> strings) => strings.SelectMany(s => s.Int64s());

    /// <summary>Gets the <see cref="BigInteger"/> values of the <see cref="string"/>.</summary>
    [Pure]
    public static IEnumerable<BigInteger> BigInts(this string str)
        => str.Split(Splitters, SplitOptions)
        .Select(n => BigInt(n));

    public struct Int32sParser(string s) : IEnumerator<int>, IEnumerable<int>
    {
        private readonly string str = s;
        private int pos = -1;

        public int Current { get; private set; } = 0;

        readonly object IEnumerator.Current => Current;

        [Impure]
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

        [Pure]
        public readonly IEnumerator<int> GetEnumerator() => this;

        [Pure]
        readonly IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public readonly void Dispose() { /* Nothing to dispose */ }

        public void Reset() => throw new NotSupportedException();
    }

    public struct Int64sParser(string s) : IEnumerator<long>, IEnumerable<long>
    {
        private int pos = -1;

        public long Current { get; private set; } = 0;

        readonly object IEnumerator.Current => Current;

        [Impure]
        public bool MoveNext()
        {
            var any = false;
            var negative = false;
            Current = 0;
            while (++pos < s.Length)
            {
                var ch = s[pos];
                if (ch >= '0' && ch <= '9')
                {
                    negative |= !any && pos != 0 && s[pos - 1] == '-';
                    any = true;
                    Current *= 10;
                    Current += negative ? -(ch - '0') : ch - '0';
                }
                else if (any) { return true; }
            }
            return any;
        }

        [Pure]
        public readonly IEnumerator<long> GetEnumerator() => this;

        [Pure]
        readonly IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public readonly void Dispose() { /* Nothing to dispose */ }

        public void Reset() => throw new NotSupportedException();
    }
}
