namespace SmartAss.Collections;

[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
[DebuggerDisplay("Count: {Count}, Total: {Total}")]
public class ItemCounter<TItem> : IEnumerable<ItemCount<TItem>>
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Dictionary<TItem, long> lookup = new();

    public long this[TItem key]
    {
        get => lookup.TryGetValue(key, out var count) ? count : 0;
        set => lookup[key] = value;
    }

    public ICollection<TItem> Keys => lookup.Keys;

    public int Count => this.AsEnumerable().Count();
    public IReadOnlyCollection<TItem> Items => lookup.Keys;
    public IReadOnlyCollection<long> Counts => lookup.Values;
    public long Total => lookup.Values.Sum();

    public void Add(IEnumerable<TItem> items)
    {
        foreach (var item in items ?? Array.Empty<TItem>())
        {
            this[item]++;
        }
    }

    public void Clear() => lookup.Clear();

    public bool Any() => lookup.Values.Any(count => count != 0);

    public ItemCount<TItem> Max() => this.OrderByDescending(item => item.Count).FirstOrDefault();
    public ItemCount<TItem> Min() => this.OrderBy(item => item.Count).FirstOrDefault();

    /// <summary>Gets all items (so including records with a count of zero.</summary>
    public IEnumerable<ItemCount<TItem>> All()
        => lookup.Select(kvp => ItemCount.Create(kvp.Key, kvp.Value));

    public IEnumerator<ItemCount<TItem>> GetEnumerator() 
        => All().Where(item => item.Any()).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
