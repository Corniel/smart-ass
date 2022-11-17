namespace Parsing.Parser_specs;

public class Parses
{
    [TestCase("-666")]
    [TestCase("812738")]
    public void Int32(string str)
    {
        Parser.ToInt32(str, out var parsed).Should().BeTrue();
        parsed.Should().Be(int.Parse(str, CultureInfo.InvariantCulture));
    }

    [TestCase("-666")]
    [TestCase("8128472268774923738")]
    public void Int64(string str)
    {
        Parser.ToInt64(str, out var parsed).Should().BeTrue();
        parsed.Should().Be(long.Parse(str, CultureInfo.InvariantCulture));
    }

    [TestCase("666")]
    [TestCase("3.14159")]
    [TestCase("-3.14159")]
    [TestCase("12345.6789012")]
    [TestCase("12345.678901234")]
    [TestCase("81284722.68774923738")]
    public void Decimal(string str)
    {
        Parser.ToDecimal(str, out var parsed).Should().BeTrue();
        parsed.Should().Be(decimal.Parse(str, CultureInfo.InvariantCulture));
    }

    [TestCase("666")]
    [TestCase("3.14159")]
    [TestCase("-3.14159")]
    [TestCase("12345.6789012")]
    [TestCase("12345.678901234")]
    [TestCase("81284722.68774923738")]
    public void Double(string str)
    {
        Parser.ToDouble(str, out var parsed).Should().BeTrue();
        parsed.Should().Be(double.Parse(str, CultureInfo.InvariantCulture));
    }
}

public class Does_not_parse
{
    [Test]
    public void Overflow()
    {
        Parser.ToInt32("12345678901234567890", out var result).Should().BeFalse();
        result.Should().Be(default);
    }
}
