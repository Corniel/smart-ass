// <copyright file = "BitsOperator.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SmartAss
{
    /// <summary>Operator that can tweak/manipulate bits.</summary>
    /// <typeparam name="TInteger">
    /// The type of integer.
    /// </typeparam>
    public interface BitsOperator<TInteger>
        where TInteger : struct
    {
        /// <summary>Gets the bit size of the <see cref="TInteger"/> type.</summary>
        int BitSize { get; }

        /// <summary>Counts the number of bits with the value 1.</summary>
        int Count(TInteger bits);

        /// <summary>Gets the number of bits needed to represent the number.</summary>
        int Size(TInteger bits);

        /// <summary>Gets the index of the first bit needed to represent the number.</summary>
        int First(TInteger bits);

        /// <summary>Returns true if the bit is '1' at the specified position.</summary>
        bool HasFlag(TInteger bits, int position);

        /// <summary>Flags the bit at the specified position.</summary>
        TInteger Flag(TInteger bits, int position);

        /// <summary>Unflags the bit at the specified position.</summary>
        TInteger Unflag(TInteger bits, int position);

        /// <summary>Mirrors the bits.</summary>
        TInteger Mirror(TInteger bits);

        /// <summary>Represents the bits as binary string.</summary>
        string ToString(TInteger bits);

        /// <summary>Parses a pattern into a bit mask, ignoring all characters except '1' and '0'.</summary>
        TInteger Parse(string str) => Parse(str, ones: "1", zeros: "0");

        /// <summary>Parses a pattern into a bit mask, ignoring all characters except <paramref name="ones"/> and <paramref name="zeros"/>.</summary>
        TInteger Parse(string str, string ones, string zeros);
    }
}
