using SmartAss.Collections;
using SmartAss.Specs.AwesomeAssertions;
using System.Numerics;

namespace AwesomeAssertions;

public static class SmartAssAssertions
{
    [Pure]
    public static SmartAssLoopAssertions<T> Should<T>(this LoopNode<T> subject) where T : IEqualityOperators<T, T, bool>
        => new(subject);
}
