namespace Digits_specs;

public class Splits_numbers
{
    [TestCase(123, 1, 2, 3)]
    [TestCase(1234, 1, 2, 3, 4)]
    [TestCase(2147483647, 2, 1, 4, 7, 4, 8, 3, 6, 4, 7)]
    public void into_array_of_digits(int number, params int[] digits)
        => number.Digits().Should().BeEquivalentTo(digits.Select(d => (byte)d));
}
