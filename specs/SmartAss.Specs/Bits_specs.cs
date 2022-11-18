using FluentAssertions;

namespace Bits_specs;

public class Count
{
    [TestCase(0, 0)]
    [TestCase(1, 0b_0010_0000)]
    [TestCase(2, 0b_0010_0010)]
    [TestCase(3, 0b_0010_1010)]
    [TestCase(4, 0b_1010_1010)]
    [TestCase(5, 0b_0110_1011)]
    [TestCase(6, 0b_0111_1110)]
    [TestCase(7, 0b_1111_1011)]
    [TestCase(8, 0b_1111_1111)]
    public void Byte(int total, byte bits)
        => Bits.Byte.Count(bits).Should().Be(total);

    [TestCase(0, 0U)]
    [TestCase(1, 0b_0000_0000_0010_0000U)]
    [TestCase(2, 0b_0000_0000_0010_0010U)]
    [TestCase(3, 0b_0010_0000_0010_0010U)]
    [TestCase(4, 0b_0010_0000_0010_1010U)]
    [TestCase(5, 0b_0010_0010_0010_1010U)]
    [TestCase(6, 0b_0010_0110_0010_1010U)]
    [TestCase(7, 0b_0010_0110_0010_1011U)]
    [TestCase(8, 0b_1010_0110_0010_1011U)]
    [TestCase(9, 0b_1010_0111_0010_1011U)]
    public void UInt32(int total, uint bits)
        => Bits.UInt32.Count(bits).Should().Be(total);

    [TestCase(0, 0UL)]
    [TestCase(1, 0b_0000_0000_0010_0000UL)]
    [TestCase(2, 0b_0000_0000_0010_0010UL)]
    [TestCase(3, 0b_0010_0000_0010_0010UL)]
    [TestCase(4, 0b_0010_0000_0010_1010UL)]
    [TestCase(5, 0b_0010_0010_0010_1010UL)]
    [TestCase(6, 0b_0010_0110_0010_1010UL)]
    [TestCase(7, 0b_0010_0110_0010_1011UL)]
    [TestCase(8, 0b_1010_0110_0010_1011UL)]
    [TestCase(9, 0b_1010_0111_0010_1011UL)]
    public void UInt64(int total, ulong bits)
        => Bits.UInt64.Count(bits).Should().Be(total);
}

public class Size
{
    [TestCase(0, 0)]
    [TestCase(1, 0b_0000_0001)]
    [TestCase(2, 0b_0000_0010)]
    [TestCase(3, 0b_0000_0110)]
    [TestCase(4, 0b_0000_1010)]
    [TestCase(5, 0b_0001_1011)]
    [TestCase(6, 0b_0011_1110)]
    [TestCase(7, 0b_0111_1011)]
    [TestCase(8, 0b_1011_1111)]
    public void Byte(int total, byte bits)
        => Bits.Byte.Size(bits).Should().Be(total);

    [TestCase(0, 0U)]
    [TestCase(1, 0b_0000_0001U)]
    [TestCase(2, 0b_0000_0010U)]
    [TestCase(3, 0b_0000_0110U)]
    [TestCase(4, 0b_0000_1010U)]
    [TestCase(5, 0b_0001_1011U)]
    [TestCase(6, 0b_0011_1110U)]
    [TestCase(7, 0b_0111_1011U)]
    [TestCase(8, 0b_1011_1111U)]
    public void UInt32(int total, uint bits)
        => Bits.UInt32.Size(bits).Should().Be(total);

    [TestCase(0, 0UL)]
    [TestCase(1, 0b_0000_0001UL)]
    [TestCase(2, 0b_0000_0010UL)]
    [TestCase(3, 0b_0000_0110UL)]
    [TestCase(4, 0b_0000_1010UL)]
    [TestCase(5, 0b_0001_1011UL)]
    [TestCase(6, 0b_0011_1110UL)]
    [TestCase(7, 0b_0111_1011UL)]
    [TestCase(8, 0b_1011_1111UL)]
    public void UInt64(int total, ulong bits)
        => Bits.UInt64.Size(bits).Should().Be(total);
}

public class Mirror
{
    [TestCase(0x01, 0x80)]
    [TestCase(0x24, 0x24)]
    [TestCase(0xC8, 0x13)]
    public void Byte(byte bits, byte mirrored)
        => Bits.Byte.Mirror(bits).Should().HaveBits(mirrored);

    [TestCase(0x0000_0001U, 0x8000_0000U)]
    [TestCase(0x0000_2400U, 0x0024_0000U)]
    [TestCase(0x000C_0080U, 0x0100_3000U)]
    [TestCase(0x51C8_7AB8U, 0x1D5E_138AU)]
    public void UInt32(uint bits, uint mirrored)
        => Bits.UInt32.Mirror(bits).Should().HaveBits(mirrored);
    
    [TestCase(0x0000_0000_0001_0000UL, 0x0000_8000_0000_0000UL)]
    [TestCase(0x0000_0000_0200_0400UL, 0x0020_0040_0000_0000UL)]
    [TestCase(0x000C_0080_3456_0670UL, 0x0E60_6A2C_0100_3000UL)]
    [TestCase(0x51C8_7AB8_0545_3210UL, 0x084C_A2A0_1D5E_138AUL)]
    public void UInt64(ulong bits, ulong mirrored)
     => Bits.UInt64.Mirror(bits).Should().HaveBits(mirrored);
}

public class Flag
{
    [TestCase(0, 0x13)]
    [TestCase(1, 0x13)]
    [TestCase(2, 0x17)]
    [TestCase(5, 0x33)]
    public void Byte(int position, byte flaged)
        => Bits.Byte.Flag(0x13, position).Should().HaveBits(flaged);

    [TestCase(0, 0x13U)]
    [TestCase(1, 0x13U)]
    [TestCase(2, 0x17U)]
    [TestCase(5, 0x33U)]
    public void UInt32(int position, uint flaged)
        => Bits.UInt32.Flag(0x13U, position).Should().HaveBits(flaged);

    [TestCase(0, 0x13UL)]
    [TestCase(1, 0x13UL)]
    [TestCase(2, 0x17UL)]
    [TestCase(5, 0x33UL)]
    public void UInt64(int position, ulong flaged)
        => Bits.UInt64.Flag(0x13UL, position).Should().HaveBits(flaged);
}

public class Unflag
{
    [TestCase(0, 0xFC)]
    [TestCase(1, 0xFC)]
    [TestCase(2, 0xF8)]
    [TestCase(5, 0xDC)]
    public void Byte(int position, byte unflaged)
        => Bits.Byte.Unflag(0xFC, position).Should().HaveBits(unflaged);

    [TestCase(0, 0xFCU)]
    [TestCase(1, 0xFCU)]
    [TestCase(2, 0xF8U)]
    [TestCase(5, 0xDCU)]
    public void UInt32(int position, uint unflaged)
        => Bits.UInt32.Unflag(0xFC, position).Should().HaveBits(unflaged);

    [TestCase(0, 0xFCUL)]
    [TestCase(1, 0xFCUL)]
    [TestCase(2, 0xF8UL)]
    [TestCase(5, 0xDCUL)]
    public void UInt64(int position, ulong unflaged)
        => Bits.UInt64.Unflag(0xFC, position).Should().HaveBits(unflaged);
}

public class Parse
{
    [TestCase("", 0U)]
    [TestCase("0001", 0b_0000_0001U)]
    [TestCase("0011", 0b_0000_0011U)]
    [TestCase("1111", 0b_0000_1111U)]
    [TestCase("100101", 0b_0010_0101U)]
    [TestCase("111 1111", 0b_0111_1111U)]
    [TestCase("111 Some 1 Stuff 1 that 1 should 1 be ignored", 0b_0111_1111U)]
    public void UInt32(string pattern, uint bits)
        => Bits.UInt32.Parse(pattern).Should().HaveBits(bits);

    [TestCase("", 0U)]
    [TestCase("0001", 0b_0000_0001UL)]
    [TestCase("0011", 0b_0000_0011UL)]
    [TestCase("1111", 0b_0000_1111UL)]
    [TestCase("100101", 0b_0010_0101UL)]
    [TestCase("111 1111", 0b_0111_1111UL)]
    [TestCase("111 Some 1 Stuff 1 that 1 should 1 be ignored", 0b_0111_1111UL)]
    public void UInt64(string pattern, ulong bits)
        => Bits.UInt64.Parse(pattern).Should().HaveBits(bits);
}

public class ToString
{
    [TestCase(0x01, "00000001")]
    [TestCase(0x03, "00000011")]
    [TestCase(0x0F, "00001111")]
    [TestCase(0x25, "00100101")]
    [TestCase(0x7F, "01111111")]
    [TestCase(byte.MaxValue, "11111111")]
    public void Byte(byte bits, string str)
        => Bits.Byte.ToString(bits).Should().Be(str);

    [TestCase(0x01U, "00000000 00000000 00000000 00000001")]
    [TestCase(0x03U, "00000000 00000000 00000000 00000011")]
    [TestCase(0x0FU, "00000000 00000000 00000000 00001111")]
    [TestCase(0x25U, "00000000 00000000 00000000 00100101")]
    [TestCase(0x7FU, "00000000 00000000 00000000 01111111")]
    [TestCase(uint.MaxValue, "11111111 11111111 11111111 11111111")]
    public void UInt32(uint bits, string str)
       => Bits.UInt32.ToString(bits).Should().Be(str);
}
