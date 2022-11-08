using System;
using System.Globalization;
using System.Linq;

namespace SmartAss.UnitTests
{
    public class ParserTest
    {
        const int Zillions = 10_000;

        [Test]
        public void Int32_Overflow()
        {
            Assert.IsFalse(Parser.ToInt32("12345678901234567890", out int result));
            Assert.AreEqual(0, result);
        }

        [TestCase("-666", -666)]
        [TestCase("8128472268774923738", 8128472268774923738L)]
        public void ToInt64(string str, long expected)
        {
            Assert.IsTrue(Parser.ToInt64(str, out long actual));
            Assert.AreEqual(expected, actual);
        }

        [TestCase("666")]
        [TestCase("3.14159")]
        [TestCase("-3.14159")]
        [TestCase("12345.6789012")]
        [TestCase("12345.678901234")]
        [TestCase("81284722.68774923738")]
        public void ToDecimal(string str)
        {
            var expected = decimal.Parse(str, CultureInfo.InvariantCulture);
            Assert.IsTrue(Parser.ToDecimal(str, out decimal actual));
            Assert.AreEqual(expected, actual);
        }

        [TestCase("666")]
        [TestCase("3.14159")]
        [TestCase("-3.14159")]
        [TestCase("12345.6789012")]
        [TestCase("12345.678901234")]
        [TestCase("81284722.68774923738")]
        public void ToDouble(string str)
        {
            var expected = double.Parse(str, CultureInfo.InvariantCulture);
            Assert.IsTrue(Parser.ToDouble(str, out double actual));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToInt32_Zillions_ShouldBeFaster()
        {
            var rnd = new Random(17);

            var strings = Enumerable.Range(0, Zillions).Select(n => rnd.Next(int.MinValue, int.MaxValue).ToString()).ToArray();

            var reference = Speed.Test(Zillions, nameof(Int32TryParse), (num) => Int32TryParse(strings[num]));
            var challence = Speed.Test(Zillions, nameof(ParserInt32), (num) => ParserInt32(strings[num]));

            Console.WriteLine(reference / challence);

            foreach (var str in strings)
            {
                Assert.AreEqual(Int32TryParse(str), ParserInt32(str));
            }
        }

        [Test]
        public void ToInt64_Zillions_ShouldBeFaster()
        {
            var rnd = new Random(17);

            var strings = Enumerable.Range(0, Zillions).Select(n =>
            {
                long top = rnd.Next(int.MinValue, int.MaxValue - 1);
                long bot = rnd.Next(int.MinValue, int.MaxValue - 1);
                return ((top << 32) | bot).ToString();

            }).ToArray();

            var reference = Speed.Test(Zillions, nameof(Int64TryParse), (num) => Int64TryParse(strings[num]));
            var challence = Speed.Test(Zillions, nameof(ParserInt64), (num) => ParserInt64(strings[num]));

            Console.WriteLine(reference / challence);

            foreach (var str in strings)
            {
                Assert.AreEqual(Int64TryParse(str), ParserInt64(str));
            }
        }

        [Test]
        public void ToDouble_Zillions_ShouldBeFaster()
        {
            var rnd = new Random(17);

            var strings = Enumerable.Range(0, Zillions).Select(n =>
            {
                double numerator = rnd.Next(int.MinValue, int.MaxValue);
                double denumerator = rnd.Next(1, int.MaxValue);
                return (numerator / denumerator).ToString(CultureInfo.InvariantCulture);
            }).ToArray();

            var reference = Speed.Test(Zillions, nameof(DoublelTryParse), (num) => DoublelTryParse(strings[num]));
            var challence = Speed.Test(Zillions, nameof(ParserDouble), (num) => ParserDouble(strings[num]));

            AssertChallengeIsBeter(reference, challence);

            foreach (var str in strings)
            {
                Assert.AreEqual(DoublelTryParse(str), ParserDouble(str), 0.000_000_001);
            }
        }

        [Test]
        public void ToDecimal_Zillions_ShouldBeFaster()
        {
            var rnd = new Random(17);

            var strings = Enumerable.Range(0, Zillions).Select(n =>
            {
                double numerator = rnd.Next(int.MinValue, int.MaxValue);
                double denumerator = rnd.Next(1, int.MaxValue);
                return (numerator / denumerator).ToString(CultureInfo.InvariantCulture);
            }).ToArray();

            var reference = Speed.Test(Zillions, nameof(DecimalTryParse), (num) => DecimalTryParse(strings[num]));
            var challence = Speed.Test(Zillions, nameof(ParserDecimal), (num) => ParserDecimal(strings[num]));

            AssertChallengeIsBeter(reference, challence);

            foreach (var str in strings)
            {
                Assert.AreEqual(DecimalTryParse(str), ParserDecimal(str));
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
        private static double ParserDouble(string str)
        {
            Parser.ToDouble(str, out double result);
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
        private static double DoublelTryParse(string str)
        {
            double.TryParse(str, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double result);
            return result;
        }
        private static decimal DecimalTryParse(string str)
        {
            decimal.TryParse(str, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal result);
            return result;
        }


        private static void AssertChallengeIsBeter(TimeSpan reference, TimeSpan challence)
        {
            Console.WriteLine(reference / challence);

            Assert.IsTrue(reference > challence,
                $"Reference: {reference.TotalMilliseconds:#,##0.0}ms, Challence: {challence.TotalMilliseconds:#,##0.0}ms");
        }
    }
}
