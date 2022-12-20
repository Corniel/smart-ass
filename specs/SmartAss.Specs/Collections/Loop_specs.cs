using SmartAss.Collections;

namespace Collections.Loop_specs;

internal class Can_be_created
{
    [Test]
    public void form_sequence_of_elements()
    {
        var circle = Loop.New(0, 1, 2, 3, 4);
        circle.Should().HaveElements(0, 1, 2, 3, 4);
    }
}

internal class Elements
{
    [Test]
    public void can_be_removed()
    {
        var circle = Loop.New(0, 1, 2, 3, 4);
        circle.Skip(2).Remove();

        circle.Should().HaveElements(0, 1, 3, 4);
    }

    [Test]
    public void can_be_inserted()
    {
        var circle = Loop.New(0, 1, 2, 3, 4);
        circle.Skip(2).InsertAfter(42);
        circle.Should().HaveElements(0, 1, 2, 42, 3, 4);
    }

    [Test]
    public void can_be_reinserted()
    {
        var circle = Loop.New(0, 1, 2, 3, 4);
        var elm = circle.Skip(2).Remove();
        circle.Skip(4).InsertAfter(elm);
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
        var node = Loop.New(0, 1, 2, 3, 4);
        node.Step(step).Value.Should().Be(3);
    }
}
