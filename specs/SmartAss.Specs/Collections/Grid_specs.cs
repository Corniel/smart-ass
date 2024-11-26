using SmartAss.Collections;
using SmartAss.Numerics;

namespace Grid_specs;

public class Can_be_created_from
{
    private static readonly KeyValuePair<Point, int>[] elements =
    [
        KeyValuePair.Create(new Point(0, 0), 1),
        KeyValuePair.Create(new Point(1, 0), 2),
        KeyValuePair.Create(new Point(2, 0), 3),
        KeyValuePair.Create(new Point(0, 1), 4),
        KeyValuePair.Create(new Point(1, 1), 5),
        KeyValuePair.Create(new Point(2, 1), 6),
    ];

    [Test]
    public void jagged_array()
    {
        var jagged = new int[][]
        {
            [1, 2, 3],
            [4, 5, 6],
        };
        var grid = new Grid<int>(jagged);
        Assert.AreEqual(elements, grid);
    }

    [Test]
    public void two_dimensional_array()
    {
        var jagged = new int[,]
        {
            { 1, 2, 3 },
            { 4, 5, 6 },
        };
        var grid = new Grid<int>(jagged);
        Assert.AreEqual(elements, grid);
    }

    [Test]
    public void its_dimensions()
    {
        var grid = new Grid<int>(3, 2);
        Assert.AreEqual(3, grid.Cols);
        Assert.AreEqual(2, grid.Rows);
    }
}
public class Can_not_be_created_from
{
    [Test]
    public void jagged_array_with_different_array_lengths()
    {
        Assert.Catch<ArgumentException>(() => new Grid<int>(
        [
            [1],
            [1, 2],
        ]));
    }

    [Test]
    public void jagged_array_with_null_array()
    {
        Assert.Catch<ArgumentException>(() => new Grid<int>(
        [
            [1, 3],
            null,
            [1, 2],
        ]));
    }
}
public class Elements
{
    [Test]
    public void can_be_set_via_position()
    {
        var grid = new Grid<int>(3, 2);
        grid[new Point(1, 1)] = 3;
        Assert.IsTrue(grid.Any(kvp => kvp.Key == new Point(1, 1) && kvp.Value == 3));
    }

    [Test]
    public void can_be_set_via_coordinates()
    {
        var grid = new Grid<int>(3, 2);
        grid[1, 1] = 3;
        Assert.IsTrue(grid.Any(kvp => kvp.Key == new Point(1, 1) && kvp.Value == 3));
    }

    [Test]
    public void can_be_retrieved_via_position()
    {
        var grid = new Grid<int>(3, 2);
        grid[1, 1] = 3;
        Assert.AreEqual(3, grid[new Point(1, 1)]);
    }

    [Test]
    public void can_be_retrieved_via_coordinates()
    {
        var grid = new Grid<int>(3, 2);
        grid[new Point(1, 1)] = 3;
        Assert.AreEqual(3, grid[1, 1]);
    }

    [TestCase(-1, +0)]
    [TestCase(+0, -1)]
    [TestCase(-1, -1)]
    [TestCase(+4, +0)]
    [TestCase(+0, +3)]
    [TestCase(+4, +3)]
    public void can_not_be_set_when_out_of_grid(int col, int row)
    {
        var grid = new Grid<int>(3, 2);
        Assert.Throws<NotOnGrid>(() => grid[col, row] = 666);
        Assert.Throws<NotOnGrid>(() => grid[new Point(col, row)] = 666);
    }
    [TestCase(-1, +0)]
    [TestCase(+0, -1)]
    [TestCase(-1, -1)]
    [TestCase(+4, +0)]
    [TestCase(+0, +3)]
    [TestCase(+4, +3)]
    public void can_not_be_retrieved_when_out_of_grid(int col, int row)
    {
        var grid = new Grid<int>(3, 2);
        Assert.Throws<NotOnGrid>(() => Console.WriteLine(grid[col, row]));
        Assert.Throws<NotOnGrid>(() => Console.WriteLine(grid[new Point(col, row)]));
    }
}
public class Positions
{ 
    [Test]
    public void are_skipped_when_null()
    {
        var grid = new Grid<object>(3, 2);
        grid[1, 1] = new object();

        grid.Positions().Should().BeEquivalentTo([new Point(1, 1)]);
    }
}
public class Tiles
{
    [Test]
    public void are_skipped_when_null()
    {
        var grid = new Grid<object>(3, 2);
        grid[1, 1] = new object();
        grid.Tiles.Count().Should().Be(1);
    }
}
public class Size
{
    [Test]
    public void is_product_of_cols_and_rows()
    {
        Assert.AreEqual(6, new Grid<int>(2, 3).Size);
    }
}
public class Rotation
{
    [Test]
    public void _90_deg_with_different_dimensions_swaps_rows_cols()
    {
        var matrix = new Grid<int>(
        [
            [1, 2],
            [3, 4],
            [5, 6],
        ]);

        var rotated = new Grid<int>(
        [
            [2, 4, 6],
            [1, 3, 5],
        ]);

        Assert.AreEqual(rotated, matrix.Rotate(DiscreteRotation.Deg090));
    }

    [Test]
    public void _180_deg_with_same_dimensions()
    {
        var matrix = new Grid<int>(
        [
            [1, 2],
            [3, 4],
            [5, 6],
        ]);

        var rotated = new Grid<int>(
        [
            [6, 5],
            [4, 3],
            [2, 1],
        ]);

        Assert.AreEqual(rotated, matrix.Rotate(DiscreteRotation.Deg180));
    }

    [Test]
    public void _270_deg_with_different_dimensions_swaps_rows_cols()
    {
        var matrix = new Grid<int>(
        [
            [1, 2],
            [3, 4],
            [5, 6],
        ]);

        var rotated = new Grid<int>(
        [
            [5, 3, 1],
            [6, 4, 2],
        ]);

        Assert.AreEqual(rotated, matrix.Rotate(DiscreteRotation.Deg270));
    }

    [Test]
    public void _4_times_results_in_identical_values()
    {
        var matrix = new Grid<int>(
        [
            [1, 2],
            [3, 4],
            [5, 6],
        ]);

        Assert.AreEqual(matrix, matrix
            .Rotate(DiscreteRotation.Deg090)
            .Rotate(DiscreteRotation.Deg090)
            .Rotate(DiscreteRotation.Deg090)
            .Rotate(DiscreteRotation.Deg090));
    }
}

public class Flip
{
    [Test]
    public void horizontal_mirrors_x_coordinates()
    {
        var matrix = new Grid<int>(
        [
            [1, 2],
            [3, 4],
            [5, 6],
        ]);

        var flipped = new Grid<int>(
        [
            [2, 1],
            [4, 3],
            [6, 5],
        ]);

        matrix.Flip(horizontal: true).Should().BeEquivalentTo(flipped);
    }

}
