// <copyright file = "StringParsing.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
#pragma warning disable S3900 // Arguments of public methods should be validated against null
// intended for simple parsing

namespace SmartAss.Parsing
{
    public static class StringParsing
    {
        private const StringSplitOptions SplitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

        public static string[] Separate(this string str, char splitter)
            => str.Split(new[] { splitter }, SplitOptions);

        public static string[] Separate(this string str, string splitter)
            => str.Split(new[] { splitter }, SplitOptions);

        public static string[] Separate(this string str, params string[] splitters)
            => str.Split(splitters, SplitOptions);

        public static string[] Separate(this string str, params char[] splitters)
          => str.Split(splitters, SplitOptions);

        public static IReadOnlyList<string> CommaSeparated(this string str)
            => str.Separate(',');

        public static IEnumerable<T> CommaSeparated<T>(this string str, Func<string, int, T> selector)
            => str.CommaSeparated().Select(selector);

        public static IEnumerable<T> CommaSeparated<T>(this string str, Func<string, T> selector)
            => str.CommaSeparated().Select(selector);

        public static IEnumerable<T> SpaceSeparated<T>(this string str, Func<string, T> selector)
            => str.SpaceSeparated().Select(selector);

        public static IReadOnlyList<string> SpaceSeparated(this string str)
            => str.Separate(' ');

        public static IReadOnlyList<string> Lines(this string str, StringSplitOptions options = SplitOptions)
            => str.Contains('\n')
            ? str.Split(["\r\n", "\n"], options)
            : str.Split(';', options);

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

        public static CharPixels CharPixels(this IEnumerable<string> lines, bool ignoreSpace = false) => string.Join('\n', lines).CharPixels(ignoreSpace);

        public static CharPixels CharPixels(this string str, bool ignoreSpace = false) => Parsing.CharPixels.Parse(str, ignoreSpace);
    }
}
