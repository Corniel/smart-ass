using SmartAss.Parsing;

namespace Parsing.String_parsing_specs;

public class Lines
{
    [TestCase("Hello\nWorld!")]
    [TestCase("Hello\r\nWorld!")]
    [TestCase("Hello;World!")]
    public void Splits_on_new_lines_and_semicolons(string str)
        => str.Lines().Should().BeEquivalentTo("Hello", "World!");

    [Test]
    public void Splits_not_on_semicolons_in_string_with_new_line()
        => "Hel;lo\r\nWorld!".Lines().Should().BeEquivalentTo("Hel;lo", "World!");
}
