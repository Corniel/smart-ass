using SmartAss.Collections;

namespace Jagged_specs;

public class Fully_Initializes
{
    [Test]
    public void Two_dimensional_array()
    {
        var array = Jagged.Array<int>(2, 3);

        array.Should().BeEquivalentTo(new int[][]
        {
            [0, 0, 0],
            [0, 0, 0],
        });
    }

    [Test]
    public void Three_dimensional_array()
    {
        var array = Jagged.Array<int>(2, 3, 4);

        array.Should().BeEquivalentTo(new int[][][]
        {
            [
                [0, 0, 0, 0],
                [0, 0, 0, 0],
                [0, 0, 0, 0],
            ],
            [
                [0, 0, 0, 0],
                [0, 0, 0, 0],
                [0, 0, 0, 0],
            ],
        });
    }

    [Test]
    public void Four_dimensional_array()
    {
        var array = Jagged.Array<int>(2, 3, 4, 1);

        array.Should().BeEquivalentTo(new int[][][][]
        {
            [
                [[0], [0], [0], [0]],
                [[0], [0], [0], [0]],
                [[0], [0], [0], [0]],
            ],
            [
                [[0], [0], [0], [0]],
                [[0], [0], [0], [0]],
                [[0], [0], [0], [0]],
            ],
        });
    }
}
