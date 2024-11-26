using SmartAss.Numerics;

namespace Numerics.Modulo_specs;

public class Int32_based
{
    public class Addition
    {
        [TestCase("2 mod 3", "3 mod 4", "11 mod 12")]
        [TestCase("2 mod 3", "3 mod 4", "11 mod 12")]
        [TestCase("11 mod 12", "2 mod 5", "47 mod 60")]
        public void Adds(ModuloInt32 left, ModuloInt32 right, ModuloInt32 added)
            => (left + right).Should().Be(added);

        [TestCase("2 mod 3", 8, "1 mod 3")]
        [TestCase("2 mod 12", 9, "11 mod 12")]
        [TestCase("2 mod 12", -3, "11 mod 12")]
        public void Adds(ModuloInt32 value, int addition, ModuloInt32 added)
           => (value + addition).Should().Be(added);

        [TestCase("1 mod 3", "2 mod 3")]
        [TestCase("2 mod 3", "0 mod 3")]
        public void Increment(ModuloInt32 value, ModuloInt32 increased)
        {
            value++;
            value.Should().Be(increased);
        }
    }

    public class Subtraction
    {
        [TestCase("2 mod 3", 1, "1 mod 3")]
        [TestCase("2 mod 12", 9, "5 mod 12")]
        [TestCase("2 mod 12", -3, "5 mod 12")]
        public void Subtracts(ModuloInt32 value, int addition, ModuloInt32 added)
           => (value - addition).Should().Be(added);

        [TestCase("0 mod 3", "2 mod 3")]
        [TestCase("1 mod 3", "0 mod 3")]
        public void Decrement(ModuloInt32 value, ModuloInt32 increased)
        {
            value--;
            value.Should().Be(increased);
        }
    }
}
public class Int64_based
{
    public class Addition
    {
        [TestCase("2 mod 3", "3 mod 4", "11 mod 12")]
        [TestCase("2 mod 3", "3 mod 4", "11 mod 12")]
        [TestCase("11 mod 12", "2 mod 5", "47 mod 60")]
        public void Adds(ModuloInt64 left, ModuloInt64 right, ModuloInt64 added)
            => (left + right).Should().Be(added);

        [TestCase("2 mod 3", 8, "1 mod 3")]
        [TestCase("2 mod 12", 9, "11 mod 12")]
        [TestCase("2 mod 12", -3, "11 mod 12")]
        public void Adds(ModuloInt64 value, int addition, ModuloInt64 added)
           => (value + addition).Should().Be(added);

        [TestCase("1 mod 3", "2 mod 3")]
        [TestCase("2 mod 3", "0 mod 3")]
        public void Increment(ModuloInt64 value, ModuloInt64 increased)
        {
            value++;
            value.Should().Be(increased);
        }
    }

    public class Subtraction
    {
        [TestCase("2 mod 3", 1, "1 mod 3")]
        [TestCase("2 mod 12", 9, "5 mod 12")]
        [TestCase("2 mod 12", -3, "5 mod 12")]
        public void Subtracts(ModuloInt64 value, int addition, ModuloInt64 added)
           => (value - addition).Should().Be(added);

        [TestCase("0 mod 3", "2 mod 3")]
        [TestCase("1 mod 3", "0 mod 3")]
        public void Decrement(ModuloInt64 value, ModuloInt64 increased)
        {
            value--;
            value.Should().Be(increased);
        }
    }
}
