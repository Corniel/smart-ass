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
        Assert.AreEqual(Vector.N, Vector.N.Rotate((DiscreteRotation)rotation));
    }

    [Test]
    public void one_step_rotates_counter_clock_whise()
    {
        Assert.AreEqual(Vector.W, Vector.N.Rotate(DiscreteRotation.Deg090));
    }

    [Test]
    public void minus_one_step_rotates_clock_whise()
    {
        Assert.AreEqual(Vector.E, Vector.N.Rotate((DiscreteRotation)(-1)));
    }

    [Test]
    public void two_steps_mirrors()
    {
        Assert.AreEqual(Vector.S, Vector.N.Rotate(DiscreteRotation.Deg180));
    }
}
