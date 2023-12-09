// <copyright file = "Modulo.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SmartAss.Numerics;

namespace System
{
    /// <summary>In mathematics, the term modulo ("with respect to a modulus of", the Latin ablative of modulus.</summary>
    public static class ModuloExtensions
    {
        [Pure]
        public static ModuloInt32 Sum(this IEnumerable<ModuloInt32> items)
        {
            ModuloInt32 sum = default;

            foreach (var item in items ?? Array.Empty<ModuloInt32>()) { sum += item; }
            return sum;
        }

        [Pure]
        public static ModuloInt64 Sum(this IEnumerable<ModuloInt64> items)
        {
            ModuloInt64 sum = default;

            foreach (var item in items ?? Array.Empty<ModuloInt64>()) { sum += item; }
            return sum;
        }
    }
}
