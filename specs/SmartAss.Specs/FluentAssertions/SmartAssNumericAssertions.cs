using AwesomeAssertions.Collections;
using AwesomeAssertions.Numeric;
using SmartAss.Numerics;
using System.Numerics;

namespace AwesomeAssertions;

public static class SmartAssNumericAssertions
{
    [CustomAssertion]
    public static AndConstraint<NumericAssertions<byte>> HaveBits(this NumericAssertions<byte> assertions, byte bits, string because = "", params object[] becauseArgs)
    {
        assertions.CurrentAssertionChain
            .ForCondition(assertions.Subject == bits)
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected: 0x{bits:X2} ({bits}){Environment.NewLine}Actual:   0x{assertions.Subject:X2} ({assertions.Subject})");

        return new(assertions);
    }

    [CustomAssertion]
    public static AndConstraint<NumericAssertions<uint>> HaveBits(this NumericAssertions<uint> assertions, uint bits, string because = "", params object[] becauseArgs)
    {
        assertions.CurrentAssertionChain
            .ForCondition(assertions.Subject == bits)
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected: 0x{bits:X2} ({bits}){Environment.NewLine}Actual:   0x{assertions.Subject:X2} ({assertions.Subject})");

        return new(assertions);
    }

    [CustomAssertion]
    public static AndConstraint<NumericAssertions<ulong>> HaveBits(this NumericAssertions<ulong> assertions, ulong bits, string because = "", params object[] becauseArgs)
    {
        assertions.CurrentAssertionChain
            .ForCondition(assertions.Subject == bits)
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected: 0x{bits:X2} ({bits}){Environment.NewLine}Actual:   0x{assertions.Subject:X2} ({assertions.Subject})");

        return new(assertions);
    }

    [CustomAssertion]
    public static AndConstraint<GenericCollectionAssertions<TNumber>> Be<TNumber>(this GenericCollectionAssertions<TNumber> assertions, NumericRange<TNumber> range, string because = "", params object[] becauseArgs)
        where TNumber : struct, INumber<TNumber>
    {
        ((object)assertions.Subject).Should().Be(range, because, becauseArgs);

        return new(assertions);
    }
}
