using SmartAss.Numerics;
using SmartAss.Parsing;

namespace CharPixels_specs;

public class Parse
{
    [Test]
    public void detects_missing_columns()
    {
        var pixels = "ABCD\nEFG\nHIJKP\nMNOP7\nQRSTZ".CharPixels();

        AssertThat("ABCD\nEFG\nHIJKP\nMNOP7\nQRSTZ",
            cols: 5,
            rows: 5,
            missesColumns: true,
            actual: pixels);
    }

    [TestCase('\t')]
    [TestCase(' ')]
    [TestCase('\r')]
    public void ignores_whitespace_when_specified(char whitespace)
    {
        var pixels = $"{whitespace}{whitespace}ABCD\r\nED{whitespace}FG\r\nHIJK{whitespace}\r\n{whitespace}\r\nMNOP\r\nQRST".CharPixels(ignoreSpace: true);

        AssertThat("ABCD\nEDFG\nHIJK\nMNOP\nQRST",
            cols: 4,
            rows: 5,
            missesColumns: false,
            actual: pixels);
    }

    [Test]
    public void skips_empty_lines()
    {
        var pixels = "\n\nABCD\nEDFG\nHIJK\n\nMNOP\nQRST\n".CharPixels();

        AssertThat("ABCD\nEDFG\nHIJK\nMNOP\nQRST",
            cols: 4,
            rows: 5,
            missesColumns: false,
            actual: pixels);
    }

    [Test]
    public void does_not_require_new_line_at_end()
    {
        var pixels = "ABCD\nEDFG\nHIJK\nMNOP\nQRST".CharPixels();

        AssertThat("ABCD\nEDFG\nHIJK\nMNOP\nQRST",
            cols: 4,
            rows: 5,
            missesColumns: false,
            actual: pixels);
    }

    [Test]
    public void sets_pixels_with_right_positions()
    {
        var pixels = "AB\nCDE".CharPixels();
        var positions = pixels.Select(p => p.Key).ToArray();
        positions.Should().BeEquivalentTo(
        [
            new Point(0, 0),
            new Point(1, 0),
            new Point(0, 1),
            new Point(1, 1),
            new Point(2, 1),
        ]);
    }

    private static void AssertThat(
        string toString,
        int cols,
        int rows,
        bool missesColumns,
        CharPixels actual)
    {
        var str = string.Join('\n', actual.ToString().Lines());
        str.Should().Be(toString);
        actual.Should().BeEquivalentTo(new
        {
            Cols = cols,
            Rows = rows,
            HasMissingColumns = missesColumns,
        });
    }
}
