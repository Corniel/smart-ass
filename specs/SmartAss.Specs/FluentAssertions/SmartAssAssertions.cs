using SmartAss.Collections;
using SmartAss.Specs.FluentAssertions;
using System.Numerics;

namespace FluentAssertions;

public static class SmartAssAssertions
{
    [Pure]
    public static SmartAssLoopAssertions<T> Should<T>(this LoopNode<T> subject) where T : IEqualityOperators<T, T, bool>
        => new(subject);
}
