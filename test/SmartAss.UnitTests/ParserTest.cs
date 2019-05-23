using NUnit.Framework;
using System;
using System.Globalization;
using System.Linq;
using Troschuetz.Random.Generators;

namespace SmartAss.UnitTests
{
    public class ParserTest
    {
        [Test]
        public void Int32_Overflow()
        {
            Assert.IsFalse(Parser.ToInt32("12345678901234567890", out int result));
            Assert.AreEqual(0, result);
        }

        [TestCase("8128472268774923738", 8128472268774923738L)]
        public void ToInt64(string str, long expected)
        {
            Assert.IsTrue(Parser.ToInt64(str, out long actual));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToInt32_10million_ShouldBeFaster()
        {
            var numbers = 2 * 1000 * 1000;
            var rnd = new MT19937Generator(17);

            var strings = Enumerable.Range(0, numbers).Select(n => rnd.Next(int.MinValue, int.MaxValue).ToString()).ToArray();

            Speed.Test(numbers, nameof(Int32TryParse), (num) => Int32TryParse(strings[num]));
            Speed.Test(numbers, nameof(ParserInt32), (num) => ParserInt32(strings[num]));

            for (var num = 0; num < numbers; num++)
            {
                Assert.AreEqual(Int32TryParse(strings[num]), ParserInt32(strings[num]));
            }
        }

        [Test]
        public void ToInt64_10million_ShouldBeFaster()
        {
            var numbers = 2 * 1000 * 1000;
            var rnd = new MT19937Generator(17);

            var strings = Enumerable.Range(0, numbers).Select(n =>
            {
                long top = rnd.Next(int.MinValue, int.MaxValue);
                long bot = rnd.Next(int.MinValue, int.MaxValue);
                return ((top << 32) | bot).ToString();

            }).ToArray();

            Speed.Test(numbers, nameof(Int64TryParse), (num) => Int64TryParse(strings[num]));
            Speed.Test(numbers, nameof(ParserInt64), (num) => ParserInt64(strings[num]));

            for (var num = 0; num < numbers; num++)
            {
                Assert.AreEqual(Int64TryParse(strings[num]), ParserInt64(strings[num]));
            }
        }

        [Test]
        public void ToDecimal_10million_ShouldBeFaster()
        {
            var numbers = 2 * 1000 * 1000;
            var rnd = new Random(17);

            var strings = Enumerable.Range(0, numbers).Select(n =>
            {
                var f = (decimal)(2 * rnd.NextDouble() - 1);
                var dec =  f * int.MaxValue;
                return dec.ToString();
            }).ToArray();

            Speed.Test(numbers, nameof(DecimalTryParse), (num) => DecimalTryParse(strings[num]));
            Speed.Test(numbers, nameof(ParserDecimal), (num) => ParserDecimal(strings[num]));

            for (var num = 0; num < numbers; num++)
            {
                Assert.AreEqual(DecimalTryParse(strings[num]), ParserDecimal(strings[num]));
            }
        }

        private static int ParserInt32(string str)
        {
            Parser.ToInt32(str, out int result);
            return result;
        }
        private static long ParserInt64(string str)
        {
            Parser.ToInt64(str, out long result);
            return result;
        }
        private static decimal ParserDecimal(string str)
        {
            Parser.ToDecimal(str, out decimal result);
            return result;
        }

        private static int Int32TryParse(string str)
        {
            int.TryParse(str, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out int result);
            return result;
        }

        private static long Int64TryParse(string str)
        {
            long.TryParse(str, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out long result);
            return result;
        }
        private static decimal DecimalTryParse(string str)
        {
            decimal.TryParse(str, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal result);
            return result;
        }
    }
}
