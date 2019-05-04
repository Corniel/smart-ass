using System;

namespace SmartAss
{
    /// <summary>Implements <see cref="IBitsOperator{T}"/> for <see cref="ulong"/>.</summary>
    internal class BitsUInt64 : IBitsOperator<ulong>
    {
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
            #endregion
        };
        internal static readonly ulong[] unflag =
        {
            #region values
            0XFFFFFFFFFFFFFFFE,
            0XFFFFFFFFFFFFFFFD,
            0XFFFFFFFFFFFFFFFB,
            0XFFFFFFFFFFFFFFF7,
            0XFFFFFFFFFFFFFFEF,
            0XFFFFFFFFFFFFFFDF,
            0XFFFFFFFFFFFFFFBF,
            0XFFFFFFFFFFFFFF7F,
            0XFFFFFFFFFFFFFEFF,
            0XFFFFFFFFFFFFFDFF,
            0XFFFFFFFFFFFFFBFF,
            0XFFFFFFFFFFFFF7FF,
            0XFFFFFFFFFFFFEFFF,
            0XFFFFFFFFFFFFDFFF,
            0XFFFFFFFFFFFFBFFF,
            0XFFFFFFFFFFFF7FFF,
            0XFFFFFFFFFFFEFFFF,
            0XFFFFFFFFFFFDFFFF,
            0XFFFFFFFFFFFBFFFF,
            0XFFFFFFFFFFF7FFFF,
            0XFFFFFFFFFFEFFFFF,
            0XFFFFFFFFFFDFFFFF,
            0XFFFFFFFFFFBFFFFF,
            0XFFFFFFFFFF7FFFFF,
            0XFFFFFFFFFEFFFFFF,
            0XFFFFFFFFFDFFFFFF,
            0XFFFFFFFFFBFFFFFF,
            0XFFFFFFFFF7FFFFFF,
            0XFFFFFFFFEFFFFFFF,
            0XFFFFFFFFDFFFFFFF,
            0XFFFFFFFFBFFFFFFF,
            0XFFFFFFFF7FFFFFFF,
            0XFFFFFFFEFFFFFFFF,
            0XFFFFFFFDFFFFFFFF,
            0XFFFFFFFBFFFFFFFF,
            0XFFFFFFF7FFFFFFFF,
            0XFFFFFFEFFFFFFFFF,
            0XFFFFFFDFFFFFFFFF,
            0XFFFFFFBFFFFFFFFF,
            0XFFFFFF7FFFFFFFFF,
            0XFFFFFEFFFFFFFFFF,
            0XFFFFFDFFFFFFFFFF,
            0XFFFFFBFFFFFFFFFF,
            0XFFFFF7FFFFFFFFFF,
            0XFFFFEFFFFFFFFFFF,
            0XFFFFDFFFFFFFFFFF,
            0XFFFFBFFFFFFFFFFF,
            0XFFFF7FFFFFFFFFFF,
            0XFFFEFFFFFFFFFFFF,
            0XFFFDFFFFFFFFFFFF,
            0XFFFBFFFFFFFFFFFF,
            0XFFF7FFFFFFFFFFFF,
            0XFFEFFFFFFFFFFFFF,
            0XFFDFFFFFFFFFFFFF,
            0XFFBFFFFFFFFFFFFF,
            0XFF7FFFFFFFFFFFFF,
            0XFEFFFFFFFFFFFFFF,
            0XFDFFFFFFFFFFFFFF,
            0XFBFFFFFFFFFFFFFF,
            0XF7FFFFFFFFFFFFFF,
            0XEFFFFFFFFFFFFFFF,
            0XDFFFFFFFFFFFFFFF,
            0XBFFFFFFFFFFFFFFF,
            0X7FFFFFFFFFFFFFFF,
            #endregion
        };
    }
}
