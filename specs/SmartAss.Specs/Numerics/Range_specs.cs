using SmartAss.Numerics;

namespace Numerics.Range_specs;

public class Int32
{
    public class Size
    {
        [Test]
        public void Zero_for_empty() => Int32Range.Empty.Size.Should().Be(0);

        [Test]
        public void Lower_and_upper_inclusive() => new Int32Range(3, 10).Size.Should().Be(8);
    }

    public class Join
    {
        [Test]
        public void empty_when_not_connected()
            => new Int32Range(0, 4).Join(new Int32Range(6, 8)).Should().Be(Int32Range.Empty);

        [Test]
        public void Connected_when_adjacent()
            => new Int32Range(0, 4).Join(new Int32Range(5, 8)).Should().Be(new Int32Range(0, 8));

        [Test]
        public void Connected_when_overlapping()
            => new Int32Range(0, 4).Join(new Int32Range(3, 8)).Should().Be(new Int32Range(0, 8));
    }

    public class Interects
    {
        [Test]
        public void with_single_number_is_single_number()
            => new Int32Range(1, 4).Intersection(new Int32Range(3, 3)).Should().Be(new Int32Range(3, 3));
    }

    public class Except
    {
        [TestCase("0-5")]
        [TestCase("1-4")]
        public void bigger_or_equal_to_range_is_empty(Int32Range except)
            => new[] { new Int32Range(1, 4) }.Except(except).Should().BeEmpty();

        [TestCase("0-1")]
        [TestCase("0-2")]
        [TestCase("8-100")]
        [TestCase("9-459")]
        public void without_overlap_has_no_effect(Int32Range except)
           => new[] { new Int32Range(3, 7) }.Except(except).Single().Should().Be(new Int32Range(3, 7));

        [Test]
        public void within_range_splits()
        {
            var range = new[] { new Int32Range(0, 8) };
            var splitted = range.Except(new Int32Range(3, 4));
            splitted.Should().BeEquivalentTo(new[] { new Int32Range(0, 2), new Int32Range(5, 8) });
        }

        [Test]
        public void with_overlap_and_bigger_at_right()
        {
            var range = new[] { new Int32Range(0, 8) };
            var splitted = range.Except(new Int32Range(8, 10));
            splitted.Single().Should().Be(new Int32Range(0, 7));
        }

        [Test]
        public void with_overlap_and_bigger_at_left()
        {
            var range = new[] { new Int32Range(4, 8) };
            var splitted = range.Except(new Int32Range(0, 6));
            splitted.Single().Should().Be(new Int32Range(7, 8));
        }
    }
}

public class Int64
{
    public class Size
    {
        [Test]
        public void Zero_for_empty() => Int64Range.Empty.Size.Should().Be(0);

        [Test]
        public void Lower_and_upper_inclusive() => new Int64Range(3, 10).Size.Should().Be(8);
    }

    public class Join
    {
        [Test]
        public void null_when_not_connected()
            => new Int64Range(0, 4).Join(new Int64Range(6, 8)).Should().Be(Int64Range.Empty);

        [Test]
        public void Connected_when_adjacent()
            => new Int64Range(0, 4).Join(new Int64Range(5, 8)).Should().Be(new Int64Range(0, 8));

        [Test]
        public void Connected_when_overlapping()
            => new Int32Range(0, 4).Join(new Int32Range(3, 8)).Should().Be(new Int32Range(0, 8));
    }

    public class Interects
    {
        [Test]
        public void with_single_number_is_single_number()
            => new Int64Range(1, 4).Intersection(new Int64Range(3, 3)).Should().Be(new Int64Range(3, 3));
    }

    public class Except
    {
        [TestCase("0-5")]
        [TestCase("1-4")]
        public void bigger_or_equal_to_range_is_empty(Int64Range except)
            => new[] { new Int64Range(1, 4) }.Except(except).Should().BeEmpty();

        [TestCase("0-1")]
        [TestCase("0-2")]
        [TestCase("8-100")]
        [TestCase("9-459")]
        public void without_overlap_has_no_effect(Int64Range except)
           => new[] { new Int64Range(3, 7) }.Except(except).Single().Should().Be(new Int64Range(3, 7));

        [Test]
        public void within_range_splits()
        {
            var range = new[] { new Int64Range(0, 8) };
            var splitted = range.Except(new Int64Range(3, 4));
            splitted.Should().BeEquivalentTo(new[] { new Int64Range(0, 2), new Int64Range(5, 8) });
        }

        [Test]
        public void with_overlap_and_bigger_at_right()
        {
            var range = new[] { new Int64Range(0, 8) };
            var splitted = range.Except(new Int64Range(8, 10));
            splitted.Single().Should().Be(new Int64Range(0, 7));
        }

        [Test]
        public void with_overlap_and_bigger_at_left()
        {
            var range = new[] { new Int64Range(4, 8) };
            var splitted = range.Except(new Int64Range(0, 6));
            splitted.Single().Should().Be(new Int64Range(7, 8));
        }
    }
}
