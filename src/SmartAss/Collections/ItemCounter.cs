﻿namespace SmartAss.Collections;

public static class ItemCounter
{
    [Pure]
    public static ItemCounter<TItem> New<TItem>(TItem item) where TItem : notnull
        => New([item]);

    [Pure]
    public static ItemCounter<TItem> New<TItem>(IEnumerable<TItem> items) where TItem : notnull
        => new() { items };
}

[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView))]
[DebuggerDisplay("Count: {Count}, Total: {Total}")]
public class ItemCounter<TItem> : IEnumerable<ItemCount<TItem>> where TItem : notnull
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly Dictionary<TItem, long> lookup = new();

    public long this[TItem key]
    {
        get => lookup.TryGetValue(key, out var count) ? count : 0;
        set => lookup[key] = value;
    }

    public ICollection<TItem> Keys => lookup.Keys;

    public int Count => lookup.Values.Count(count => count != 0);

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
    [Pure]
    public IEnumerable<ItemCount<TItem>> All()
        => lookup.Select(kvp => ItemCount.Create(kvp.Key, kvp.Value));

    [Pure]
    public IEnumerator<ItemCount<TItem>> GetEnumerator() 
        => All().Where(item => item.Any()).GetEnumerator();

    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
