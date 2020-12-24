// <copyright file = "CharPixels.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using SmartAss.Diagnostics;
using SmartAss.Numeric;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CharPixel = System.Collections.Generic.KeyValuePair<SmartAss.Numeric.Point, char>;

namespace SmartAss.Parsing
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    [DebuggerTypeProxy(typeof(CollectionDebugView))]
    public class CharPixels : IEnumerable<CharPixel>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly CharPixel[] items;

        private CharPixels(IEnumerable<CharPixel> pixels, int col, int row, bool hasMissingColumns)
        {
            items = pixels.ToArray();
            Cols = col;
            Rows = row;
            HasMissingColumns = hasMissingColumns;
        }

        /// <summary>Gets the total of pixels.</summary>
        public int Count => items.Length;

        /// <summary>Gets the number of columns.</summary>
        public int Cols { get; }

        /// <summary>Gets the number of rows.</summary>
        public int Rows { get; }

        /// <summary>When not all rows have the same amounts of columns.</summary>
        public bool HasMissingColumns { get; }

        public Grid<char> Grid()
        {
            var grid = new Grid<char>(Cols, Rows);
            foreach (var pixel in this) { grid[pixel.Key] = pixel.Value; }
            return grid;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var row = 0;
            var sb = new StringBuilder();
            foreach (var pixel in items)
            {
                if (pixel.Key.Y != row)
                {
                    row = pixel.Key.Y;
                    sb.AppendLine();
                }
                sb.Append(pixel.Value);
            }
            return sb.ToString();
        }

        /// <summary>Parses <see cref="CharPixel"/>'s.</summary>
        public static CharPixels Parse(string input)
        {
            Guard.NotNull(input, nameof(input));

            var col = 0;
            var row = 0;
            var cols = 0;
            var rows = 0;

            var hasMissingColumns = false;
            var buffer = new SimpleList<CharPixel>(input.Length);

            foreach (var ch in input)
            {
                if (ch == '\n')
                {
                    if (col != 0)
                    {
                        hasMissingColumns |= row != 0 && col != cols;
                        row++;
                    }
                    col = 0;
                }
                else if (!char.IsWhiteSpace(ch))
                {
                    buffer.Add(new CharPixel(new Point(col, row), ch));
                    cols = Math.Max(cols, ++col);
                    rows = Math.Max(rows, row);
                }
            }
            return new CharPixels(buffer, cols, rows + 1, hasMissingColumns);
        }

        /// <inheritdoc />
        public IEnumerator<CharPixel> GetEnumerator()
            => ((IEnumerable<CharPixel>)items).GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
            => $"Count: {Count}, Columns: {Cols}, Rows: {Rows}{(HasMissingColumns ? ", missing columns" : string.Empty)}";
    }
}
