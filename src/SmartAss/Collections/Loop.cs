namespace SmartAss.Collections;

public sealed class Loop
{
    /// <summary>Creates a new instance of a loop.</summary>
    internal Loop() { }

    /// <remarks>Keeps track of the size of the loop.</remarks>
    internal int Count;

    /// <summary>Creates a new loop.</summary>
    public static LoopNode<T> New<T>(params T[] values) => NewRange(Guard.NotNull(values, nameof(values)).AsEnumerable());

    public static LoopNode<T> NewRange<T>(IEnumerable<T> values)
    {
        Guard.NotNull(values, nameof(values));

        var loop = new Loop();
        LoopNode<T>? first = null;
        LoopNode<T>? curr = null;

        foreach (var value in values)
        {
            if (first is null)
            {
                first = new LoopNode<T>(loop, value, null, null);
                curr = first;
                loop.Count++;
            }
            else
            {
                curr = curr!.InsertAfter(value);
            }
        }
        return first ?? throw new ArgumentException("No values specified", nameof(values));
    }
}
