namespace Extensions.Parity_specs;

public class Is_even
{
    [TestCase(0)]
    [TestCase(4)]
    [TestCase(4000)]
    [TestCase(int.MinValue)]
    public void true_for_even_int(int n) => n.IsEven().Should().BeTrue();

    [TestCase(1)]
    [TestCase(13)]
    [TestCase(4001)]
    [TestCase(int.MaxValue)]
    public void false_for_odd_int(int n) => n.IsEven().Should().BeFalse();

    [TestCase(0)]
    [TestCase(4)]
    [TestCase(4000)]
    [TestCase(int.MinValue)]
    public void true_for_even_long(long n) => n.IsEven().Should().BeTrue();

    [TestCase(1)]
    [TestCase(13)]
    [TestCase(4001)]
    [TestCase(int.MaxValue)]
    public void false_for_odd_long(long n) => n.IsEven().Should().BeFalse();

    [Test]
    public void true_for_even_int128() => ((Int128)4400).IsEven().Should().BeTrue();

    [Test]
    public void false_for_odd_int128() => ((Int128)4401).IsEven().Should().BeFalse();
}

public class Is_odd
{
    [TestCase(0)]
    [TestCase(4)]
    [TestCase(4000)]
    [TestCase(int.MinValue)]
    public void false_for_even_int(int n) => n.IsOdd().Should().BeFalse();

    [TestCase(1)]
    [TestCase(13)]
    [TestCase(4001)]
    [TestCase(int.MaxValue)]
    public void true_for_odd_int(int n) => n.IsOdd().Should().BeTrue();

    [TestCase(0)]
    [TestCase(4)]
    [TestCase(4000)]
    [TestCase(int.MinValue)]
    public void false_for_even_long(long n) => n.IsOdd().Should().BeFalse();

    [TestCase(1)]
    [TestCase(13)]
    [TestCase(4001)]
    [TestCase(int.MaxValue)]
    public void true_for_odd_long(long n) => n.IsOdd().Should().BeTrue();

    [Test]
    public void false_for_even_int128() => ((Int128)4400).IsOdd().Should().BeFalse();

    [Test]
    public void true_for_odd_int128() => ((Int128)4401).IsOdd().Should().BeTrue();
}
