namespace SmartAss.Collections;

public interface Iterator<T> : IEnumerable<T>, IEnumerator<T>
{
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => this;
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    object IEnumerator.Current => Current;
}
