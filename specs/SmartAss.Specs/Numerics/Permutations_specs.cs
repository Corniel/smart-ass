namespace Numerics.Permutations_specs;

public class Heaps_algorithm
{
    [Test]
    public void _2_has_2_unique_permuations()
    {
        var permuations = new[] { 0, 1, }.Permutations();

        permuations.Should().BeEquivalentTo(new int[][]
        {
            [0, 1],
            [1, 0],
        });
    }

    [Test]
    public void _3_has_6_unique_permuations()
    {
        var permuations = new[] { 0, 1, 2, }.Permutations();

        permuations.Should().BeEquivalentTo(new int[][]
        {
            [0, 1, 2],
            [1, 0, 2],
            [2, 0, 1],
            [0, 2, 1],
            [1, 2, 0],
            [2, 1, 0],
        });
    }

    [Test]
    public void _4_has_24_unique_permuations()
    {
        int[][] permuations = [
            [0, 1, 2, 3],
            [1, 0, 2, 3],
            [2, 0, 1, 3],
            [0, 2, 1, 3],
            [1, 2, 0, 3],
            [2, 1, 0, 3],
            [3, 1, 2, 0],
            [1, 3, 2, 0],
            [2, 3, 1, 0],
            [3, 2, 1, 0],
            [1, 2, 3, 0],
            [2, 1, 3, 0],
            [3, 0, 2, 1],
            [0, 3, 2, 1],
            [2, 3, 0, 1],
            [3, 2, 0, 1],
            [0, 2, 3, 1],
            [2, 0, 3, 1],
            [3, 0, 1, 2],
            [0, 3, 1, 2],
            [1, 3, 0, 2],
            [3, 1, 0, 2],
            [0, 1, 3, 2],
            [1, 0, 3, 2],
        ];

        Enumerable.Range(0, 4).ToArray().Permutations()
            .Should().BeEquivalentTo(permuations);
    }
}
