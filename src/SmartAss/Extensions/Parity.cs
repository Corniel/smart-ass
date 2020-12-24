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
        public static bool IsEven(this int n) => (n & 1) == 0;

        /// <summary>Returns true if the number is even.</summary>
        public static bool IsEven(this long n) => (n & 1) == 0;

        /// <summary>Returns true if the number is odd.</summary>
        public static bool IsEven(this BigInteger n) => (n & 1) == 0;

        /// <summary>Returns true if the number is odd.</summary>
        public static bool IsOdd(this int n) => (n & 1) == 1;

        /// <summary>Returns true if the number is odd.</summary>
        public static bool IsOdd(this long n) => (n & 1) == 1;

        /// <summary>Returns true if the number is odd.</summary>
        public static bool IsOdd(this BigInteger n) => (n & 1) == 1;
    }
}
