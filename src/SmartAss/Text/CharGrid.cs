// <copyright file = "CharGrid.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Collections;
using System.Text;

namespace SmartAss.Text
{
    public static class CharGrid
    {
        /// <summary>represents the <see cref="char"/>s in the <see cref="Grid{char}"/>
        /// as a <see cref="string"/>.
        /// </summary>
        public static string Stringyfied(this Grid<char> grid)
        {
            Guard.NotNull(grid, nameof(grid));
            var sb = new StringBuilder();
            for (var row = 0; row < grid.Rows; row++)
            {
                for (var col = 0; col < grid.Cols; col++)
                {
                    sb.Append(grid[col, row]);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
