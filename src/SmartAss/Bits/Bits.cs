// <copyright file = "Bits.cs">
// Copyright (c) 2018-current, Corniel Nobel.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using System.Text;

namespace SmartAss
{
    /// <summary>Represents a class to manipulate and investigate bits.</summary>
    public static class Bits
    {
        /// <summary>Bits based on <see cref="byte"/>.</summary>
        public static readonly IBitsOperator<byte> Byte = new BitsByte();

        /// <summary>Bits based on <see cref="uint"/>.</summary>
        public static readonly IBitsOperator<uint> UInt32 = new BitsUInt32();

        /// <summary>Bits based on <see cref="ulong"/>.</summary>
        public static readonly IBitsOperator<ulong> UInt64 = new BitsUInt64();

        /// <summary>Parses a pattern into a bit mask, ignoring all characters except 0 and.</summary>
        public static ulong Parse(string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                return 0;
            }

            var mask = 0UL;
            var shift = 0;

            for (var index = pattern.Length - 1; index >= 0; index--)
            {
                var ch = pattern[index];
                if (ch == '1')
                {
                    mask |= 1UL << shift++;
                }
                else if (ch == '0')
                {
                    shift++;
                }
            }
            return mask;
        }

        /// <summary>Represents the <see cref="byte[]"/> as binary string.</summary>
        public static string ToString(byte[] bytes)
        {
            var sb = new StringBuilder();

            for (var index = bytes.Length - 1; index >= 0; index--)
            {
                var b = bytes[index];
                if (sb.Length != 0)
                {
                    sb.Append(' ');
                }
                sb.Append(Byte.ToString(b));
            }
            return sb.ToString();
        }
    }
}
