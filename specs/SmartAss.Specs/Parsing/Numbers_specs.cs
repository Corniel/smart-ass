using SmartAss.Parsing;

namespace Parsing.Numbers_specs;

public class Int32s
{
    [TestCase("Not ending with a number 125  ", 125)]
    [TestCase("1, 2, 3, \r\n4\t5 text6test 123 125", 1, 2, 3, 4, 5, 6, 123, 125)]
    [TestCase("1, -2, 3,-125", 1, -2, 3, -125)]
    public void All_none_digits_are_ignored(string str, params int[] parsed)
        => str.Int32s().Should().BeEquivalentTo(parsed);
}
public class Int64s
{
    [TestCase("Not ending with a number 125  ", 125)]
    [TestCase("1, 2, 3, \r\n4\t5 text6test 123 125", 1, 2, 3, 4, 5, 6, 123, 125)]
    [TestCase("1, -2, 3,-125", 1, -2, 3, -125)]
    public void All_none_digits_are_ignored(string str, params long[] parsed)
       => str.Int64s().Should().BeEquivalentTo(parsed);
}
