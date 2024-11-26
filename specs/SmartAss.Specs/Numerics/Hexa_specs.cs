using SmartAss.Numerics;

namespace Numerics.Hexa_specs;

public class Chars
{
    [TestCase("27", '\'')]
    public void Can_be_parsed(string str, char ch) => Hexa.Char(str).Should().Be(ch);
}
