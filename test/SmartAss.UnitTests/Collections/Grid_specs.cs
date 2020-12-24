using NUnit.Framework;
using SmartAss.Numeric;
using SmartAss.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Grid_specs
{
    public class Can_be_created_from
    {
        private static readonly KeyValuePair<Point, int>[] elements = new[]
        {
            KeyValuePair.Create(new Point(0, 0), 1),
            KeyValuePair.Create(new Point(1, 0), 2),
            KeyValuePair.Create(new Point(2, 0), 3),
            KeyValuePair.Create(new Point(0, 1), 4),
            KeyValuePair.Create(new Point(1, 1), 5),
            KeyValuePair.Create(new Point(2, 1), 6),
        };

        [Test]
        public void jagged_array()
        {
            var jagged = new int[][]
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5, 6 },
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
            Assert.Catch<ArgumentException>(() => new Grid<int>(new int[][]
            {
                new []{ 1 },
                new [] { 1, 2 },
            }));
        }

        [Test]
        public void jagged_array_with_null_array()
        {
            Assert.Catch<ArgumentException>(() => new Grid<int>(new int[][]
            {
                new [] { 1, 3 },
                null,
                new [] { 1, 2 },
            }));
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
            Assert.AreEqual(new[] { new Point(1, 1) }, grid.Positions);
        }
    }
    public class Tiles
    {
        [Test]
        public void are_skipped_when_null()
        {
            var grid = new Grid<object>(3, 2);
            grid[1, 1] = new object();
            Assert.AreEqual(1, grid.Tiles.Count());
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
}
