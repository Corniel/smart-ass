namespace Extensions.ReadOnlyList_specs;

public class Reverses
{
    [Test]
    public void Array()
    {
        int[] numbers = [1, 3, 6, 42];
        numbers.Reversed().Should().BeEquivalentTo([42, 6, 3, 1]);
    }
}
