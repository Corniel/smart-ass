using System;

namespace SmartAss
{
    /// <summary>Implements <see cref="IBitsOperator{T}"/> for <see cref="ulong"/>.</summary>
    internal class BitsUInt64 : IBitsOperator<ulong>
    {
        /// <inheritdoc />
        public int BitSize => 64;

        /// <inheritdoc />
        public int Count(ulong bits)
        {
            unchecked
            {
                bits = bits - ((bits >> 1) & 0x5555555555555555UL);
                bits = (bits & 0x3333333333333333UL) + ((bits >> 2) & 0x3333333333333333UL);
                return (int)(unchecked(((bits + (bits >> 4)) & 0xF0F0F0F0F0F0F0FUL) * 0x101010101010101UL) >> 56);
            }
        }

        /// <inheritdoc />
        public int Size(ulong bits)
        {
            unchecked
            {
                var size = BitsByte.size[bits >> 54];
                if (size != 0) { return size + 54; }
                size = BitsByte.size[bits >> 48];
                if (size != 0) { return size + 48; }
                size = BitsByte.size[bits >> 40];
                if (size != 0) { return size + 40; }
                size = BitsByte.size[bits >> 32];
                if (size != 0) { return size + 32; }
                size = BitsByte.size[bits >> 24];
                if (size != 0) { return size + 24; }
                size = BitsByte.size[bits >> 16];
                if (size != 0) { return size + 16; }
                size = BitsByte.size[bits >> 8];
                if (size != 0) { return size + 8; }
                return BitsByte.size[bits];
            }
        }

        /// <inheritdoc />
        public int First(ulong bits)
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
                if (first != 0) { return first + 24; }
                first = BitsByte.first[(bits >> 32) & 255];
                if (first != 0) { return first + 32; }
                first = BitsByte.first[(bits >> 40) & 255];
                if (first != 0) { return first + 40; }
                first = BitsByte.first[(bits >> 48) & 255];
                if (first != 0) { return first + 48; }
                first = BitsByte.first[(bits >> 54) & 255];
                return first + 54;
            }
        }

        /// <inheritdoc />
        public ulong Flag(ulong bits, int position) => bits | flag[position];

        /// <inheritdoc />
        public ulong Unflag(ulong bits, int position) => bits & unflag[position];

        /// <inheritdoc />
        public ulong Mirror(ulong bits)
        {
            var mirror = bits;
            mirror = (mirror >> 01) & 0x5555555555555555 | (mirror << 01) & 0xaaaaaaaaaaaaaaaa;
            mirror = (mirror >> 02) & 0x3333333333333333 | (mirror << 02) & 0xcccccccccccccccc;
            mirror = (mirror >> 04) & 0x0f0f0f0f0f0f0f0f | (mirror << 04) & 0xf0f0f0f0f0f0f0f0;
            mirror = (mirror >> 08) & 0x00ff00ff00ff00ff | (mirror << 08) & 0xff00ff00ff00ff00;
            mirror = (mirror >> 16) & 0x0000ffff0000ffff | (mirror << 16) & 0xffff0000ffff0000;
            mirror = (mirror >> 32) & 0x00000000ffffffff | (mirror << 32) & 0xffffffff00000000;
            return mirror;
        }

        /// <inheritdoc />
        public string ToString(ulong bits) => Bits.ToString(BitConverter.GetBytes(bits));


        internal static readonly ulong[] flag =
        {
            #region values
            0x0000000000000001,
            0x0000000000000002,
            0x0000000000000004,
            0x0000000000000008,
            0x0000000000000010,
            0x0000000000000020,
            0x0000000000000040,
            0x0000000000000080,
            0x0000000000000100,
            0x0000000000000200,
            0x0000000000000400,
            0x0000000000000800,
            0x0000000000001000,
            0x0000000000002000,
            0x0000000000004000,
            0x0000000000008000,
            0x0000000000010000,
            0x0000000000020000,
            0x0000000000040000,
            0x0000000000080000,
            0x0000000000100000,
            0x0000000000200000,
            0x0000000000400000,
            0x0000000000800000,
            0x0000000001000000,
            0x0000000002000000,
            0x0000000004000000,
            0x0000000008000000,
            0x0000000010000000,
            0x0000000020000000,
            0x0000000040000000,
            0x0000000080000000,
            0x0000000100000000,
            0x0000000200000000,
            0x0000000400000000,
            0x0000000800000000,
            0x0000001000000000,
            0x0000002000000000,
            0x0000004000000000,
            0x0000008000000000,
            0x0000010000000000,
            0x0000020000000000,
            0x0000040000000000,
            0x0000080000000000,
            0x0000100000000000,
            0x0000200000000000,
            0x0000400000000000,
            0x0000800000000000,
            0x0001000000000000,
            0x0002000000000000,
            0x0004000000000000,
            0x0008000000000000,
            0x0010000000000000,
            0x0020000000000000,
            0x0040000000000000,
            0x0080000000000000,
            0x0100000000000000,
            0x0200000000000000,
            0x0400000000000000,
            0x0800000000000000,
            0x1000000000000000,
            0x2000000000000000,
            0x4000000000000000,
            0x8000000000000000,
            // Overflow
            0x0000000000000000,
            #endregion
        };
        internal static readonly ulong[] unflag =
        {
            #region values
            0xFFFFFFFFFFFFFFFE,
            0xFFFFFFFFFFFFFFFD,
            0xFFFFFFFFFFFFFFFB,
            0xFFFFFFFFFFFFFFF7,
            0xFFFFFFFFFFFFFFEF,
            0xFFFFFFFFFFFFFFDF,
            0xFFFFFFFFFFFFFFBF,
            0xFFFFFFFFFFFFFF7F,
            0xFFFFFFFFFFFFFEFF,
            0xFFFFFFFFFFFFFDFF,
            0xFFFFFFFFFFFFFBFF,
            0xFFFFFFFFFFFFF7FF,
            0xFFFFFFFFFFFFEFFF,
            0xFFFFFFFFFFFFDFFF,
            0xFFFFFFFFFFFFBFFF,
            0xFFFFFFFFFFFF7FFF,
            0xFFFFFFFFFFFEFFFF,
            0xFFFFFFFFFFFDFFFF,
            0xFFFFFFFFFFFBFFFF,
            0xFFFFFFFFFFF7FFFF,
            0xFFFFFFFFFFEFFFFF,
            0xFFFFFFFFFFDFFFFF,
            0xFFFFFFFFFFBFFFFF,
            0xFFFFFFFFFF7FFFFF,
            0xFFFFFFFFFEFFFFFF,
            0xFFFFFFFFFDFFFFFF,
            0xFFFFFFFFFBFFFFFF,
            0xFFFFFFFFF7FFFFFF,
            0xFFFFFFFFEFFFFFFF,
            0xFFFFFFFFDFFFFFFF,
            0xFFFFFFFFBFFFFFFF,
            0xFFFFFFFF7FFFFFFF,
            0xFFFFFFFEFFFFFFFF,
            0xFFFFFFFDFFFFFFFF,
            0xFFFFFFFBFFFFFFFF,
            0xFFFFFFF7FFFFFFFF,
            0xFFFFFFEFFFFFFFFF,
            0xFFFFFFDFFFFFFFFF,
            0xFFFFFFBFFFFFFFFF,
            0xFFFFFF7FFFFFFFFF,
            0xFFFFFEFFFFFFFFFF,
            0xFFFFFDFFFFFFFFFF,
            0xFFFFFBFFFFFFFFFF,
            0xFFFFF7FFFFFFFFFF,
            0xFFFFEFFFFFFFFFFF,
            0xFFFFDFFFFFFFFFFF,
            0xFFFFBFFFFFFFFFFF,
            0xFFFF7FFFFFFFFFFF,
            0xFFFEFFFFFFFFFFFF,
            0xFFFDFFFFFFFFFFFF,
            0xFFFBFFFFFFFFFFFF,
            0xFFF7FFFFFFFFFFFF,
            0xFFEFFFFFFFFFFFFF,
            0xFFDFFFFFFFFFFFFF,
            0xFFBFFFFFFFFFFFFF,
            0xFF7FFFFFFFFFFFFF,
            0xFEFFFFFFFFFFFFFF,
            0xFDFFFFFFFFFFFFFF,
            0xFBFFFFFFFFFFFFFF,
            0xF7FFFFFFFFFFFFFF,
            0xEFFFFFFFFFFFFFFF,
            0xDFFFFFFFFFFFFFFF,
            0xBFFFFFFFFFFFFFFF,
            0x7FFFFFFFFFFFFFFF,
            // Overflow
            0xFFFFFFFFFFFFFFFF,
            #endregion
        };
    }
}
