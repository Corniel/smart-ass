using SmartAss.Numerics;

namespace Numerics.Digit_count_specs;

public class Counts
{
    [Test]
    public void one_for_0() => 0.DigitCount().Should().Be(1);

    [TestCase(1, 01)]
    [TestCase(1, 09)]
    [TestCase(2, 10)]
    [TestCase(2, 42)]
    [TestCase(2, 99)]
    [TestCase(3, 100)]
    public void digits_for(int digits, long number) => number.DigitCount().Should().Be(digits);
}
