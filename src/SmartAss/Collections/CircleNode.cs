namespace SmartAss.Collections;

[DebuggerDisplay("{DebugggerDisplay}")]
public sealed class CircleNode<T>
{
    internal CircleNode(Circle<T> circle, T value, CircleNode<T> prev, CircleNode<T> next)
    {
        Circle = circle;
        Value = value;
        Prev = prev ?? this;
        Next = next ?? this;
    }

    public Circle<T> Circle { get; }
    public T Value { get; }

    public CircleNode<T> Prev { get; private set; }
    public CircleNode<T> Next { get; private set; }

    public CircleNode<T> Remove()
    {
        if (Circle.Count == 1) throw new InvalidOperationException("Can not remove the last node.");
        Circle.Count--;

        if (Circle.Root == this) Circle.Root = Next;

        Next.Prev = Prev;
        Prev.Next = Next;
        Next = null;
        Prev = null;
        return this;
    }

    public CircleNode<T> InsertAfter(T value)
    {
        Circle.Count++;

        var self = new CircleNode<T>(Circle, value, this, Next);
        Next.Prev = self;
        Next = self;
        return self;
    }

    public void InsertAfter(CircleNode<T> node)
    {
        if (node.Prev is { } || node.Next is { }) throw new InvalidOperationException("Can not attached node.");

        Circle.Count++;

        var next = Next;
        Next = node;
        next.Prev = node;
        node.Next = next;
        node.Prev = this;
    }

    public CircleNode<T> Step(long steps)
    {
        var s = steps.Mod(Circle.Count);
        if (s == 0)
        {
            return this;
        }

        else if (s * 2 > Circle.Count)
        {
            s -= Circle.Count; // Move the shortest way.
        }

        if (s > 0)
        {
            var next = Next;
            while (--s > 0)
            {
                next = next.Next;
            }
            return next;
        }
        else
        {
            var prev = Prev;
            while (++s < 0)
            {
                prev = prev.Prev;
            }
            return prev;
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebugggerDisplay
        => Circle.Count == 1 || Prev is null
        ? $".. {Value}, .."
        : $".. {Prev.Value}, {Value}, {Next.Value} ..";

    public override string ToString() => Value.ToString();
}
