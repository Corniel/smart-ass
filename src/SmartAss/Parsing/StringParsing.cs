// <copyright file = "StringParsing.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Collections.Generic;

namespace SmartAss.Parsing
{
    public static class StringParsing
    {
        /// <summary>Creates <see cref="Parsing.CharPixels"/> from strings.</summary>
        public static CharPixels CharPixels(this IEnumerable<string> lines) => string.Join('\n', lines).CharPixels();

        /// <summary>Creates <see cref="Parsing.CharPixels"/> from a string.</summary>
        public static CharPixels CharPixels(this string str) => Parsing.CharPixels.Parse(str);
    }
}
