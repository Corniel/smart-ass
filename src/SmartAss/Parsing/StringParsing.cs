// <copyright file = "StringParsing.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
#pragma warning disable S3900 // Arguments of public methods should be validated against null
// intended for simple parsing

using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartAss.Parsing
{
    public static class StringParsing
    {
        private const StringSplitOptions SplitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

        public static string[] Seperate(this string str, char splitter)
            => str.Split(new[] { splitter }, SplitOptions);

        public static string[] Seperate(this string str, string splitter)
            => str.Split(new[] { splitter }, SplitOptions);

        public static IEnumerable<string> CommaSeperated(this string str)
            => str.Seperate(',');

        public static IEnumerable<T> CommaSeperated<T>(this string str, Func<string, int, T> selector)
            => str.CommaSeperated().Select(selector);

        public static IEnumerable<T> CommaSeperated<T>(this string str, Func<string, T> selector)
            => str.CommaSeperated().Select(selector);

        public static IEnumerable<T> SpaceSeperated<T>(this string str, Func<string, T> selector)
            => str.SpaceSeperated().Select(selector);

        public static IEnumerable<string> SpaceSeperated(this string str)
            => str.Seperate(' ');

        public static IReadOnlyList<string> Lines(this string str, StringSplitOptions options = SplitOptions)
            => str.Split(new[] { "\r\n", "\n", ";" }, options);

        public static IEnumerable<T> Lines<T>(this string str, Func<string, T> selector)
           => str.Lines().Select(selector);

        public static IEnumerable<string[]> GroupedLines(this string str, StringSplitOptions options = SplitOptions)
        {
            var buffer = new List<string>();

            foreach (var line in str.Lines(StringSplitOptions.None))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (buffer.Any())
                    {
                        yield return buffer.ToArray();
                        buffer.Clear();
                    }
                    else if (!options.HasFlag(StringSplitOptions.RemoveEmptyEntries))
                    {
                        yield return Array.Empty<string>();
                    }
                }
                else
                {
                    buffer.Add(options.HasFlag(StringSplitOptions.TrimEntries) ? line.Trim() : line);
                }
            }
            if (buffer.Any())
            {
                yield return buffer.ToArray();
            }
        }

        public static char Char(this string str) => str[0];

        public static string StripChars(this string str, string chars)
        {
            var buffer = new char[str.Length];
            var length = 0;
            foreach (var ch in str)
            {
                if (!chars.Contains(ch)) { buffer[length++] = ch; }
            }
            return new string(buffer, 0, length);
        }

        public static CharPixels CharPixels(this IEnumerable<string> lines) => string.Join('\n', lines).CharPixels();

        public static CharPixels CharPixels(this string str) => Parsing.CharPixels.Parse(str);
    }
}
