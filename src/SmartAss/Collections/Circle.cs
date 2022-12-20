using System.Diagnostics.CodeAnalysis;

namespace SmartAss.Collections;

[DebuggerDisplay("Count = {Count}")]
[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
public class Circle<T> : IReadOnlyCollection<CircleNode<T>>, ICollection
{
    internal CircleNode<T> Root;

    public Circle(T value)
    {
        Root = new CircleNode<T>(this, value, null, null);
        Count++;
    }

    public Circle(IEnumerable<T> values)
    {
        CircleNode<T> curr = null;

        foreach(var value in values) 
        {
            if (Root is null)
            {
                Root = new CircleNode<T>(this, value, null, null);
                curr = Root;
                Count++;
            }
            else
            {
                curr = curr!.InsertAfter(value);
            }
        }
    }

    public int Count { get; internal set; }
    public bool IsSynchronized => false;

    public object SyncRoot => false;

    public CircleNode<T>[] DebuggerDisplay => ToArray();

    public void CopyTo(Array array, int index) => ToArray().CopyTo(array, index);

    public CircleNode<T>[] ToArray() => GetEnumerator().Take(Count).ToArray();

    public List<CircleNode<T>> ToList() => GetEnumerator().Take(Count).ToList();

    public Nexts GetEnumerator() => new(Root);

    [ExcludeFromCodeCoverage(Justification = "Equal to public GetEnumerator()")]
    IEnumerator<CircleNode<T>> IEnumerable<CircleNode<T>>.GetEnumerator() => GetEnumerator();
    
    [ExcludeFromCodeCoverage(Justification = "Equal to public GetEnumerator()")]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    public struct Nexts : Iterator<CircleNode<T>>
    {
        internal Nexts(CircleNode<T> current) => Current = current.Prev;

        public CircleNode<T> Current { get; private set; }

        public bool MoveNext()
        {
            Current = Current.Next;
            return true;
        }

        public void Dispose() { /* Do nothing */ }
        public void Reset() => throw new NotSupportedException();
    }
}
