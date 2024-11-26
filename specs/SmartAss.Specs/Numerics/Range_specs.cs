using SmartAss.Numerics;
using IntRange = SmartAss.Numerics.NumericRange<int>;

namespace Numerics.Range_specs;

public class Size
{
    [Test]
    public void Zero_for_empty() => IntRange.Empty.Size.Should().Be(0);

    [Test]
    public void Lower_and_upper_inclusive() => new IntRange(3, 10).Size.Should().Be(8);
}

public class Join
{
    [Test]
    public void null_when_not_connected()
        => new IntRange(0, 4).Join(new IntRange(6, 8)).Should().Be(IntRange.Empty);

    [Test]
    public void Connected_when_adjacent()
        => new IntRange(0, 4).Join(new IntRange(5, 8)).Should().Be(new IntRange(0, 8));

    [Test]
    public void Connected_when_overlapping()
        => new IntRange(0, 4).Join(new IntRange(3, 8)).Should().Be(new IntRange(0, 8));

    [Test]
    public void Biggest_wen_containing()
        => new IntRange(0, 10).Join(new IntRange(3, 8)).Should().Be(new IntRange(0, 10));
}

public class Interects
{
    [Test]
    public void with_single_number_is_single_number()
        => new IntRange(1, 4).Intersection(new IntRange(3, 3)).Should().Be(new IntRange(3, 3));
}

public class Except
{
    [TestCase("0..5")]
    [TestCase("1..4")]
    public void bigger_or_equal_to_range_is_empty(IntRange except)
        => new[] { new IntRange(1, 4) }.Except(except).Should().BeEmpty();

    [TestCase("0..1")]
    [TestCase("0..2")]
    [TestCase("8..100")]
    [TestCase("9..459")]
    public void without_overlap_has_no_effect(IntRange except)
        => new[] { new IntRange(3, 7) }.Except(except).Single().Should().Be(new IntRange(3, 7));

    [Test]
    public void within_range_splits()
    {
        var range = new[] { new IntRange(0, 8) };
        var splitted = range.Except(new IntRange(3, 4));
        splitted.Should().BeEquivalentTo([new IntRange(0, 2), new IntRange(5, 8)]);
    }

    [Test]
    public void with_overlap_and_bigger_at_right()
    {
        var range = new[] { new IntRange(0, 8) };
        var splitted = range.Except(new IntRange(8, 10));
        splitted.Single().Should().Be(new IntRange(0, 7));
    }

    [Test]
    public void with_overlap_and_bigger_at_left()
    {
        var range = new[] { new IntRange(4, 8) };
        var splitted = range.Except(new IntRange(0, 6));
        splitted.Single().Should().Be(new IntRange(7, 8));
    }
}
