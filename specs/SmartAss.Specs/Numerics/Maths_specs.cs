namespace Numerics.Maths_specs;

internal class Choose
{
    [TestCase(55, 15, 11_899_700_525_790)]
    [TestCase(52, 5, 2_598_960)]
    [TestCase(10, 2, 45)]
    [TestCase(10, 8, 45)]
    [TestCase(10, 1, 10)]
    [TestCase(10, 0, 1)]
    [TestCase(8, 3, 56)]
    public void n_over_k(long n, long k, long combination) => Maths.Choose(n, k).Should().Be(combination);
}
