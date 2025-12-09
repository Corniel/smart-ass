namespace SmartAss.Collections;

public interface Iterator<out T> : IEnumerable<T>, IEnumerator<T>
{
    [Pure]
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => this;

    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    object? IEnumerator.Current => Current;
}
