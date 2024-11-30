namespace SmartAss.Collections;

/// <summary>Represents a node in a <see cref="Loop{T}"/>.</summary>
/// <typeparam name="T">
/// The type of the values in the loop.
/// </typeparam>
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
[DebuggerDisplay("{DebugggerDisplay}")]
public sealed partial class LoopNode<T> : IEnumerable<LoopNode<T>>, ICollection
{
    private readonly Loop Loop;

    internal LoopNode(Loop loop, T value, LoopNode<T>? prev, LoopNode<T>? next)
    {
        Loop = loop;
        Value = value;
        Prev = prev ?? this;
        Next = next ?? this;
    }

    /// <summary>The value of the node.</summary>
    public T Value { get; }

    public bool IsDetached { get; private set; }

    /// <summary>Gets the size of the loop.</summary>
    public int Count => IsDetached ? 0 : Loop.Count;

    public LoopNode<T> Prev { get; private set; }

    public LoopNode<T> Next { get; private set; }

    /// <summary>Moves the current node forward in the loop.</summary>
    [FluentSyntax]
    public LoopNode<T> Move(long count)
    {
        var prev = Prev;
        Remove();
        return prev.Skip(count).InsertAfter(this);
    }

    [FluentSyntax]
    public LoopNode<T> Remove()
    {
        if (Count == 1) throw new InvalidOperationException("Can not remove the last node.");
        Loop.Count--;

        Next.Prev = Prev;
        Prev.Next = Next;
        Next = null;
        Prev = null;
        IsDetached = true;
        return this;
    }

    [FluentSyntax]
    public LoopNode<T> InsertAfter(T value)
    {
        Loop.Count++;

        var self = new LoopNode<T>(Loop, value, this, Next);
        Next.Prev = self;
        Next = self;
        return self;
    }

    [FluentSyntax]
    public LoopNode<T> InsertAfter(LoopNode<T> node)
    {
        Guard.NotNull(node, nameof(node));

        if (!node.IsDetached) throw new InvalidOperationException("Can not attached node.");

        Loop.Count++;

        var next = Next;
        Next = node;
        next.Prev = node;
        node.Next = next;
        node.Prev = this;
        node.IsDetached = false;
        return this;
    }

    [FluentSyntax]
    public LoopNode<T> Step(long steps)
    {
        var s = steps.Mod(Loop.Count);
        if (s == 0)
        {
            return this;
        }
        else if (s * 2 > Loop.Count)
        {
            s -= Loop.Count; // Move the shortest way.
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
        => Loop.Count == 1 || Prev is null
        ? $".. {Value}, .."
        : $".. {Prev.Value}, {Value}, {Next.Value} .. ({Count})";

    /// <inheritdoc />
    [Pure]
    public override string ToString() => Value?.ToString() ?? string.Empty;
}
