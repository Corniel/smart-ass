using System;

namespace SmartAss
{
    /// <summary>Implements <see cref="IBitsOperator{T}"/> for <see cref="uint"/>.</summary>
    internal class BitsUInt32 : IBitsOperator<uint>
    {
        /// <inheritdoc />
        public int BitSize => 32;

        /// <inheritdoc />
        public int Count(uint bits)
        {
            unchecked
            {
                bits = bits - ((bits >> 1) & 0x55555555);
                bits = (bits & 0x33333333) + ((bits >> 2) & 0x33333333);
                return (int)(((bits + (bits >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
            }
        }

        /// <inheritdoc />
		public int Size(uint bits)
        {
            unchecked
            {
                var size = BitsByte.size[bits >> 24];
                if (size != 0) { return size + 24; }
                size = BitsByte.size[bits >> 16];
                if (size != 0) { return size + 16; }
                size = BitsByte.size[bits >> 8];
                if (size != 0) { return size + 8; }
                return BitsByte.size[bits];
            }
        }

        /// <inheritdoc />
		public int First(uint bits)
        {
            unchecked
            {
                var first = BitsByte.first[bits & 255];
                if (first != 0) { return first; }
                first = BitsByte.first[(bits >> 8) & 255];
                if (first != 0) { return first + 8; }
                first = BitsByte.first[(bits >> 16) & 255];
                if (first != 0) { return first + 16; }
                first = BitsByte.first[(bits >> 24) & 255];
                return first + 24;
            }
        }

        /// <inheritdoc />
        public uint Flag(uint bits, int position) => bits | flag[position];

        /// <inheritdoc />
        public uint Unflag(uint bits, int position) => bits & unflag[position];

        /// <inheritdoc />
        public uint Mirror(uint bits)
        {
            var mirror = bits;
            mirror = (mirror >> 01) & 0x55555555 | (mirror << 01) & 0xaaaaaaaa;
            mirror = (mirror >> 02) & 0x33333333 | (mirror << 02) & 0xcccccccc;
            mirror = (mirror >> 04) & 0x0f0f0f0f | (mirror << 04) & 0xf0f0f0f0;
            mirror = (mirror >> 08) & 0x00ff00ff | (mirror << 08) & 0xff00ff00;
            mirror = (mirror >> 16) & 0x0000ffff | (mirror << 16) & 0xffff0000;
            return mirror;
        }

        /// <inheritdoc />
        public string ToString(uint bits) => Bits.ToString(BitConverter.GetBytes(bits));

        internal static readonly uint[] flag =
        {
            #region values
            0x00000001,
            0x00000002,
            0x00000004,
            0x00000008,
            0x00000010,
            0x00000020,
            0x00000040,
            0x00000080,
            0x00000100,
            0x00000200,
            0x00000400,
            0x00000800,
            0x00001000,
            0x00002000,
            0x00004000,
            0x00008000,
            0x00010000,
            0x00020000,
            0x00040000,
            0x00080000,
            0x00100000,
            0x00200000,
            0x00400000,
            0x00800000,
            0x01000000,
            0x02000000,
            0x04000000,
            0x08000000,
            0x10000000,
            0x20000000,
            0x40000000,
            0x80000000,
             // Overflow
            0x00000000,
            #endregion
        };
        internal static readonly uint[] unflag =
        {
            #region values
            0xFFFFFFFE,
            0xFFFFFFFD,
            0xFFFFFFFB,
            0xFFFFFFF7,
            0xFFFFFFEF,
            0xFFFFFFDF,
            0xFFFFFFBF,
            0xFFFFFF7F,
            0xFFFFFEFF,
            0xFFFFFDFF,
            0xFFFFFBFF,
            0xFFFFF7FF,
            0xFFFFEFFF,
            0xFFFFDFFF,
            0xFFFFBFFF,
            0xFFFF7FFF,
            0xFFFEFFFF,
            0xFFFDFFFF,
            0xFFFBFFFF,
            0xFFF7FFFF,
            0xFFEFFFFF,
            0xFFDFFFFF,
            0xFFBFFFFF,
            0xFF7FFFFF,
            0xFEFFFFFF,
            0xFDFFFFFF,
            0xFBFFFFFF,
            0xF7FFFFFF,
            0xEFFFFFFF,
            0xDFFFFFFF,
            0xBFFFFFFF,
            0x7FFFFFFF,
             // Overflow
            0xFFFFFFFF,
            #endregion
        };
    }
}
