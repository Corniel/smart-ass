namespace SmartAss.Numerics;

public interface NumericSequence : IEnumerable<long>
{
    long this[int n] { get; }
}
