using SmartAss.Collections;

namespace Jagged_specs;

public class Fully_Initializes
{
    [Test]
    public void Two_dimensional_array()
    {
        var array = Jagged.Array<int>(2, 3);
        var exp = new int[][]
        {
            [0, 0, 0],
            [0, 0, 0],
        };
        CollectionAssert.AreEqual(exp, array);
    }

    [Test]
    public void Three_dimensional_array()
    {
        var array = Jagged.Array<int>(2, 3, 4);
        var exp = new int[][][]
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
        };
        CollectionAssert.AreEqual(exp, array);
    }

    [Test]
    public void Four_dimensional_array()
    {
        var array = Jagged.Array<int>(2, 3, 4, 1);
        var exp = new int[][][][]
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
        };
        CollectionAssert.AreEqual(exp, array);
    }
}
