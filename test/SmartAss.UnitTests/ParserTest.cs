using NUnit.Framework;
using System;
using System.Globalization;
using System.Linq;

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

        [Test]
        public void ToInt32_10million_ShouldBeFaster()
        {
            var numbers = 10 * 1000 * 1000;
            var rnd = new Random(17);

            var strings = Enumerable.Range(0, numbers).Select(n => rnd.Next(int.MinValue, int.MaxValue).ToString()).ToArray();

            Speed.Test(numbers, "int.TryParse()", (num) => int.TryParse(strings[num], NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out int result));
            Speed.Test(numbers, "Converter.ToInt32()", (num) => Parser.ToInt32(strings[num], out int result));
        }

        [Test]
        public void ToInt64_10million_ShouldBeFaster()
        {
            var numbers = 10 * 1000 * 1000;
            var rnd = new Random(17);

            var strings = Enumerable.Range(0, numbers).Select(n =>
            {
                long top = rnd.Next(int.MinValue, int.MaxValue);
                long bot = rnd.Next(int.MinValue, int.MaxValue);
                return ((top << 32) | bot).ToString();

            }).ToArray();

            Speed.Test(numbers, "int.TryParse()", (num) => long.TryParse(strings[num], NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out long result));
            Speed.Test(numbers, "Converter.ToInt32()", (num) => Parser.ToInt64(strings[num], out long result));
        }
    }
}
