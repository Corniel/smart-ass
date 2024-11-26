using SmartAss.Numerics;

namespace Vector_specs;

public class Rotating_with
{
    [TestCase(0)]
    [TestCase(-4)]
    [TestCase(+4)]
    [TestCase(+8)]
    public void multiples_of_4_rotations_do_not_change_vector(int rotation)
    {
        Vector.N.Rotate((DiscreteRotation)rotation).Should().Be(Vector.N);
    }

    [Test]
    public void one_step_rotates_counter_clock_whise()
    {
         Vector.N.Rotate(DiscreteRotation.Deg090).Should().Be(Vector.W);
    }

    [Test]
    public void minus_one_step_rotates_clock_whise()
    {
        Vector.N.Rotate((DiscreteRotation)(-1)).Should().Be(Vector.E);
    }

    [Test]
    public void two_steps_mirrors()
    {
        Vector.N.Rotate(DiscreteRotation.Deg180).Should().Be(Vector.S);
    }
}
