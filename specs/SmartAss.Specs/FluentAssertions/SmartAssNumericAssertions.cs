using FluentAssertions.Execution;
using FluentAssertions.Numeric;

namespace FluentAssertions;

public static class SmartAssNumericAssertions
{
    public static AndConstraint<NumericAssertions<byte>> HaveBits(this NumericAssertions<byte> assertions, byte bits, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(assertions.Subject == bits)
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected: 0x{bits:X2} ({bits}){Environment.NewLine}Actual:   0x{assertions.Subject:X2} ({assertions.Subject})");

        return new(assertions);
    }

    public static AndConstraint<NumericAssertions<uint>> HaveBits(this NumericAssertions<uint> assertions, uint bits, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(assertions.Subject == bits)
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected: 0x{bits:X2} ({bits}){Environment.NewLine}Actual:   0x{assertions.Subject:X2} ({assertions.Subject})");

        return new(assertions);
    }

    public static AndConstraint<NumericAssertions<ulong>> HaveBits(this NumericAssertions<ulong> assertions, ulong bits, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(assertions.Subject == bits)
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected: 0x{bits:X2} ({bits}){Environment.NewLine}Actual:   0x{assertions.Subject:X2} ({assertions.Subject})");

        return new(assertions);
    }
}
