using SmartAss.Numerics;

namespace Numerics.Point_specs;

public class Flip_x
{
    [TestCase("4,+3", "4,-3")]
    [TestCase("3,-3", "3,+3")]
    public void negates_y_coordinate(Point point, Point flipped)
        => point.FlipX().Should().Be(flipped);

    [TestCase("4,+3", "4,+1")]
    [TestCase("3,-3", "3,+7")]
    public void other_side_of_line(Point point, Point flipped)
       => point.FlipX(2).Should().Be(flipped);
}

public class Flip_y
{
    [TestCase("+3,4", "-3,4")]
    [TestCase("-3,3", "+3,3")]
    public void negates_y_coordinate(Point point, Point flipped)
        => point.FlipY().Should().Be(flipped);

    [TestCase("+3,4", "+1,4")]
    [TestCase("-3,3", "+7,3")]
    public void other_side_of_line(Point point, Point flipped)
       => point.FlipY(2).Should().Be(flipped);
}

public class on_Manhattan_distance()
{
    [Test]
    public void eight_for_2()
    {
        var point = new Point(0, 0);
        point.OnManhattanDistance(2).Should().BeEquivalentTo(
        [
           new Point(+0, -2),
           new Point(+1, -1),
           new Point(+2, +0),
           new Point(+1, +1),
           new Point(+0, +2),
           new Point(-1, +1),
           new Point(-2, +0),
           new Point(-1, -1),
        ]);
    }

    [Test]
    public void twelve_for_3()
    {
        var point = new Point(0, 0);
        point.OnManhattanDistance(3).Should().BeEquivalentTo(
        [
            new Point(+0, -3),
            new Point(+1, -2),
            new Point(+2, -1),
            new Point(+3, +0),
            new Point(+2, +1),
            new Point(+1, +2),
            new Point(+0, +3),
            new Point(-1, +2),
            new Point(-2, +1),
            new Point(-3, +0),
            new Point(-2, -1),
            new Point(-1, -2),
        ]);
    }
}

public class ShoelaceArea
{
    [Test]
    public void calulates_area_0()
    {
        Point[] points =
        [
            new(03, 04),
            new(05, 11),
            new(12, 08),
            new(09, 05),
            new(05, 06),
        ];

        points.ShoelaceArea().Should().Be(30);
    }

    [Test]
    public void calulates_area_2()
    {
        Point[] points =
        [
            new(1, 6),
            new(3, 1),
            new(7, 2),
            new(4, 4),
            new(8, 5),
        ];

        points.ShoelaceArea().Should().Be(16.5m);
    }
}
