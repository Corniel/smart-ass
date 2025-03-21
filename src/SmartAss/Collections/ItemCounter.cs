namespace SmartAss.Collections;

public static class ItemCounter
{
    [Pure]
    public static ItemCounter<TItem> New<TItem>(params IEnumerable<TItem> items) where TItem : notnull
    {
        var counter = new ItemCounter<TItem>();
        foreach (var item in items) counter.Add(item);
        return counter;
    }
}

[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
[DebuggerDisplay("Count: {Count}, Total: {Total}")]
public sealed class ItemCounter<TItem> : IEnumerable<ItemCount<TItem>> where TItem : notnull
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Dictionary<TItem, long> lookup = new();

    public long this[TItem key]
    {
        get => lookup.TryGetValue(key, out var count) ? count : 0;
        set => lookup[key] = value;
    }

    public int Count => lookup.Values.Count(count => count != 0);

    public IReadOnlyCollection<TItem> Items => lookup.Keys;

    public IReadOnlyCollection<long> Counts => lookup.Values;

    public long Total => lookup.Values.Sum();

    public void Add(TItem item) => this[item]++;

    [Impure]
    public ItemCounter<TItem> Clear()
    {
        lookup.Clear();
        return this;
    }

    public bool HasAny => lookup.Values.Any(count => count != 0);

    [Pure]
    public ItemCount<TItem> Max() => this.OrderByDescending(item => item.Count).FirstOrDefault();

    [Pure]
    public ItemCount<TItem> Min() => this.OrderBy(item => item.Count).FirstOrDefault();

    /// <summary>Gets all items (so including records with a count of zero.</summary>
    [Pure]
    public IEnumerable<ItemCount<TItem>> All()
        => lookup.Select(kvp => ItemCount.Create(kvp.Key, kvp.Value));

    [Pure]
    public IEnumerator<ItemCount<TItem>> GetEnumerator()
        => All().Where(item => item.HasAny).GetEnumerator();

    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
