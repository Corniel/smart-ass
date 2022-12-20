using SmartAss.Collections;

namespace Collections.Cirle_specs;

internal class Can_be_created
{
    [Test]
    public void form_sequence_of_elements()
    {
        var circle = new Circle<int>(new[] { 0, 1, 2, 3, 4 });
        circle.Should().HaveElements(0, 1, 2, 3, 4);
    }
}

internal class Elements
{
    [Test]
    public void can_be_removed()
    {
        var circle = new Circle<int>(new[] { 0, 1, 2, 3, 4 });
        circle.Skip(2).First().Remove();

        circle.Should().HaveElements(0, 1, 3, 4);
    }

    [Test]
    public void can_be_inserted()
    {
        var circle = new Circle<int>(new[] { 0, 1, 2, 3, 4 });
        circle.Skip(2).First().InsertAfter(42);
        circle.Should().HaveElements(0, 1, 2, 42, 3, 4);
    }

    [Test]
    public void can_be_reinserted()
    {
        var circle = new Circle<int>(new[] { 0, 1, 2, 3, 4 });
        var elm = circle.Skip(2).First().Remove();
        circle.Skip(4).First().InsertAfter(elm);
        circle.Should().HaveElements(0, 2, 1, 3, 4);
    }
}

internal class Iterate
{
    [TestCase(-7)]
    [TestCase(-2)]
    [TestCase(+3)]
    [TestCase(+8)]
    public void with_steps(int step)
    {
        var circle = new Circle<int>(new[] { 0, 1, 2, 3, 4 });
        var node = circle.First();
        node.Step(step).Value.Should().Be(3);
    }
}
