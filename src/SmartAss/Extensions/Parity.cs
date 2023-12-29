// <copyright file = "Parity.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Numerics;

namespace System
{
    /// <summary>In mathematics, parity is the property of an integer of whether it is even or odd.</summary>
    public static class Parity
    {
        /// <summary>Returns true if the number is even.</summary>
        public static bool IsEven<TNumber>(this TNumber n) where TNumber : struct, INumber<TNumber>, IBitwiseOperators<TNumber, TNumber, TNumber>
            => (n & TNumber.One) == TNumber.Zero;

        /// <summary>Returns true if the number is odd.</summary>
        public static bool IsOdd<TNumber>(this TNumber n) where TNumber : struct, INumber<TNumber>, IBitwiseOperators<TNumber, TNumber, TNumber>
            => (n & TNumber.One) == TNumber.One;
    }
}
