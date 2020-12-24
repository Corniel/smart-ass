using NUnit.Framework;
using SmartAss.Numeric;
using SmartAss.Parsing;
using System.Linq;

namespace CharPixels_specs
{
    public class Parse
    {
        [Test]
        public void detects_missing_columns()
        {
            var pixels = "ABCD\nEFG\nHIJKP\nMNOP7\nQRSTZ".CharPixels();

            AssertThat("ABCD\r\nEFG\r\nHIJKP\r\nMNOP7\r\nQRSTZ",
                cols: 5,
                rows: 5,
                missesColumns: true,
                actual: pixels);
        }

        [TestCase('\t')]
        [TestCase(' ')]
        [TestCase('\r')]
        public void ignores_whitespace(char whitespace)
        {
            var pixels = $"{whitespace}{whitespace}ABCD\r\nED{whitespace}FG\r\nHIJK{whitespace}\r\n{whitespace}\r\nMNOP\r\nQRST".CharPixels();

            AssertThat("ABCD\r\nEDFG\r\nHIJK\r\nMNOP\r\nQRST",
                cols: 4,
                rows: 5,
                missesColumns: false,
                actual: pixels);
        }

        [Test]
        public void skips_empty_lines()
        {
            var pixels = "\n\nABCD\nEDFG\nHIJK\n\nMNOP\nQRST\n".CharPixels();

            AssertThat("ABCD\r\nEDFG\r\nHIJK\r\nMNOP\r\nQRST",
                cols: 4,
                rows: 5,
                missesColumns: false,
                actual: pixels);
        }

        [Test]
        public void does_not_require_new_line_at_end()
        {
            var pixels = "ABCD\nEDFG\nHIJK\nMNOP\nQRST".CharPixels();

            AssertThat("ABCD\r\nEDFG\r\nHIJK\r\nMNOP\r\nQRST",
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

            Assert.AreEqual(new[]
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(0, 1),
                new Point(1, 1),
                new Point(2, 1),
            },
            positions);
            Assert.AreEqual(5, pixels.Count);
        }

        private static void AssertThat(
            string toString,
            int cols,
            int rows,
            bool missesColumns,
            CharPixels actual)
        {
            Assert.AreEqual(toString, actual.ToString());
            Assert.AreEqual(cols, actual.Cols, "Cols");
            Assert.AreEqual(rows, actual.Rows, "Rows");
            Assert.AreEqual(missesColumns, actual.HasMissingColumns, "HasMissingColumns");
        }
    }
}
