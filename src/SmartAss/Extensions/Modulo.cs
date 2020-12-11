// <copyright file = "Modulo.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace System
{
    /// <summary>In mathematics, the term modulo ("with respect to a modulus of", the Latin ablative of modulus.</summary>
    public static class Modulo
    {
        /// <summary>Gets a (positive) modulo.</summary>
        public static int Mod(this int n, int modulo)
        {
            var m = n % modulo;
            return m < 0 ? m + modulo : m;
        }

        /// <summary>Gets a (positive) modulo.</summary>
        public static long Mod(this long n, long modulo)
        {
            var m = n % modulo;
            return m < 0 ? m + modulo : m;
        }
    }
}
