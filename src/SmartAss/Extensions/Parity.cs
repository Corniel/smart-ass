// <copyright file = "Parity.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Numerics;

namespace System;

/// <summary>In mathematics, parity is the property of an integer of whether it is even or odd.</summary>
public static class Parity
{
    extension<TNumber>(TNumber n) where TNumber : struct, INumber<TNumber>, IBitwiseOperators<TNumber, TNumber, TNumber>
    {
        /// <summary>Returns true if the number is even.</summary>
        [Pure]
        public bool IsEven => (n & TNumber.One) == TNumber.Zero;

        /// <summary>Returns true if the number is odd.</summary>
        [Pure]
        public bool IsOdd => (n & TNumber.One) == TNumber.One;
    }
}
