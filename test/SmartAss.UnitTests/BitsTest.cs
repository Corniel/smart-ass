using NUnit.Framework;
using SmartAss.UnitTests;
using System;

namespace SmartAss.Tests
{
    [TestFixture]
    public class BitsTest
    {
        private const int Zillions = 10_000;

        [TestCase("", 0x00)]
        [TestCase("0001", 0x01)]
        [TestCase("0011", 0x03)]
        [TestCase("1111", 0x0F)]
        [TestCase("100101", 0x25)]
        [TestCase("111 1111", 0x7F)]
        [TestCase("111 Some 1 Stuff 1 that 1 should 1 be ignored", 0x7F)]
        public void Parse(string pattern, int expected)
        {
            var bits = Bits.Parse(pattern);
            BitsAssert.AreEqual((ulong)expected, bits);
        }

        [TestCase(0x01, "00000001")]
        [TestCase(0x03, "00000011")]
        [TestCase(0x0F, "00001111")]
        [TestCase(0x25, "00100101")]
        [TestCase(0x7F, "01111111")]
        [TestCase(byte.MaxValue, "11111111")]
        public void AsString(byte number, string expected)
        {
            var bits = Bits.Byte.ToString(number);
            Assert.AreEqual(expected, bits);
        }

        [Test]
        public void Count_Byte()
        {
            var count = Bits.Byte.Count(244);
            Assert.AreEqual(5, count);
        }

        [Test]
        public void Count_UInt32()
        {
            var rnd = new Random(17);
            var numbers = new uint[Zillions];
            var actual = new int[Zillions];
            for (var i = 0; i < numbers.Length; i++)
            {
                numbers[i] = (uint)rnd.Next();
            }
            var duration = Speed.Test(numbers.Length, (index) => actual[index] = Bits.UInt32.Count(numbers[index]));
            AssertIsFast(duration);
        }

       

        [Test]
        public void Count_UInt64()
        {
            var rnd = new Random(17);
            var numbers = new ulong[Zillions];
            var actual = new int[Zillions];
            for (var i = 0; i < numbers.Length; i++)
            {
                numbers[i] = (ulong)rnd.Next();
                numbers[i] |= (ulong)((long)rnd.Next()) << 32;
            }
            var duration = Speed.Test(numbers.Length, (index) => actual[index] = Bits.UInt64.Count(numbers[index]));
            AssertIsFast(duration);
        }

        [TestCase(0, 0x13)]
        [TestCase(1, 0x13)]
        [TestCase(2, 0x17)]
        [TestCase(5, 0x33)]
        public void Flag_Byte(int position, int expected)
        {
            byte number = 0x13;
            var flagged = Bits.Byte.Flag(number, position);
            BitsAssert.AreEqual((byte)expected, flagged);
        }

        [TestCase(0, 0x13)]
        [TestCase(1, 0x13)]
        [TestCase(2, 0x17)]
        [TestCase(5, 0x33)]
        public void Flag_UInt32(int position, int expected)
        {
            var number = 0x13U;
            var flagged = Bits.UInt32.Flag(number, position);
            BitsAssert.AreEqual((uint)expected, flagged);
        }

        [TestCase(0, 0x13)]
        [TestCase(1, 0x13)]
        [TestCase(2, 0x17)]
        [TestCase(5, 0x33)]
        public void Flag_UInt64(int position, int expected)
        {
            var number = 0x13UL;
            var flagged = Bits.UInt64.Flag(number, position);
            BitsAssert.AreEqual((ulong)expected, flagged);
        }

        [TestCase(0, 0xFC)]
        [TestCase(1, 0xFC)]
        [TestCase(2, 0xF8)]
        [TestCase(5, 0xDC)]
        public void Unflag_Byte(int position, int expected)
        {
            byte number = 0xFC;
            var flagged = Bits.Byte.Unflag(number, position);
            BitsAssert.AreEqual((byte)expected, flagged);
        }

        [TestCase(0, 0xFC)]
        [TestCase(1, 0xFC)]
        [TestCase(2, 0xF8)]
        [TestCase(5, 0xDC)]
        public void Unflag_UInt32(int position, int expected)
        {
            var number = 0xFCU;
            var flagged = Bits.UInt32.Unflag(number, position);
            BitsAssert.AreEqual((uint)expected, flagged);
        }

        [TestCase(0, 0xFC)]
        [TestCase(1, 0xFC)]
        [TestCase(2, 0xF8)]
        [TestCase(5, 0xDC)]
        public void Unflag_UInt64(int position, int expected)
        {
            var number = 0xFCUL;
            var flagged = Bits.UInt64.Unflag(number, position);
            BitsAssert.AreEqual((ulong)expected, flagged);
        }

        [TestCase(0x01, 0x80)]
        [TestCase(0x24, 0x24)]
        [TestCase(0xC8, 0x13)]
        public void Mirror_Byte(byte bits, byte expected)
        {
            var actual = Bits.Byte.Mirror(bits);
            BitsAssert.AreEqual(expected, actual);
        }

        [TestCase(0x0000_0001, 0x8000_0000)]
        [TestCase(0x0000_2400, 0x0024_0000)]
        [TestCase(0x000C_0080, 0x0100_3000)]
        [TestCase(0x51C8_7AB8, 0x1D5E_138A)]
        public void Mirror_UInt32(long bits, long expected)
        {
            var actual = Bits.UInt32.Mirror((uint)bits);
            BitsAssert.AreEqual((uint)expected, actual);
        }

        [TestCase(0x0000_0000_0001_0000, 0x0000_8000_0000_0000)]
        [TestCase(0x0000_0000_0200_0400, 0x0020_0040_0000_0000)]
        [TestCase(0x000C_0080_3456_0670, 0x0E60_6A2C_0100_3000)]
        [TestCase(0x51C8_7AB8_0545_3210, 0x084C_A2A0_1D5E_138A)]
        public void Mirror_UInt64(long bits, long expected)
        {
            var actual = Bits.UInt64.Mirror((ulong)bits);
            BitsAssert.AreEqual((ulong)expected, actual);
        }

        private static void AssertIsFast(TimeSpan duration)
        {
            Console.WriteLine($"{duration.Ticks / (double)Zillions:0.00} Ticks/operation");
            Assert.IsTrue(duration < TimeSpan.FromMilliseconds(Zillions / 100d), duration.ToString());
        }
    }
}
