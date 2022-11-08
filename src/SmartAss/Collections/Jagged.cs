// <copyright file = "Jagged.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SmartAss.Collections
{
    /// <summary>Helper for creating jagged arrays.</summary>
    public static class Jagged
    {
        /// <summary>Creates a jagged array of m x n.</summary>
        /// <typeparam name="T">
        /// Type of the jagged array.
        /// </typeparam>
        public static T[][] Array<T>(int m, int n)
        {
            var array = new T[m][];
            for (var i = 0; i < m; i++)
            {
                array[i] = new T[n];
            }
            return array;
        }

        /// <summary>Creates a jagged array of m x n x o.</summary>
        /// <typeparam name="T">
        /// Type of the jagged array.
        /// </typeparam>
        public static T[][][] Array<T>(int m, int n, int o)
        {
            var array = new T[m][][];
            for (var i = 0; i < m; i++)
            {
                array[i] = Array<T>(n, o);
            }
            return array;
        }

        /// <summary>Creates a jagged array of m x n x o x p.</summary>
        /// <typeparam name="T">
        /// Type of the jagged array.
        /// </typeparam>
        public static T[][][][] Array<T>(int m, int n, int o, int p)
        {
            var array = new T[m][][][];
            for (var i = 0; i < m; i++)
            {
#pragma warning disable S2234 // Parameters should be passed in the correct order
                array[i] = Array<T>(n, o, p);
#pragma warning restore S2234 // Parameters should be passed in the correct order
            }
            return array;
        }

        /// <summary>Creates a jagged array of m x n x o x p xq.</summary>
        /// <typeparam name="T">
        /// Type of the jagged array.
        /// </typeparam>
        public static T[][][][][] Array<T>(int m, int n, int o, int p, int q)
        {
            var array = new T[m][][][][];
            for (var i = 0; i < m; i++)
            {
#pragma warning disable S2234 // Parameters should be passed in the correct order
                array[i] = Array<T>(n, o, p, q);
#pragma warning restore S2234 // Parameters should be passed in the correct order
            }
            return array;
        }
    }
}
