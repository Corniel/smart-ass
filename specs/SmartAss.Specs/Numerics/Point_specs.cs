using SmartAss.Numerics;

namespace Numerics.Point_specs;

internal class Flip_x
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

internal class Flip_y
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
