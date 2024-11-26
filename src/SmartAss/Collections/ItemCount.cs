namespace SmartAss.Collections;

public static class ItemCount
{
    [Pure]
    public static ItemCount<TItem> Create<TItem>(TItem item, long count) => new(item, count);

    [Pure]
    public static ItemCount<TItem> Create<TItem>(KeyValuePair<TItem, long> kvp) => new(kvp.Key, kvp.Value);

    [Pure]
    public static ItemCount<TItem> Create<TItem>(KeyValuePair<TItem, int> kvp) => new(kvp.Key, kvp.Value);
}

public readonly struct ItemCount<TItem> : IEquatable<ItemCount<TItem>>
{
    public readonly TItem Item;
    public readonly long Count;

    public ItemCount(TItem item, long count)
    {
        Item = item;
        Count = count;
    }

    public int IntCount => checked((int)Count);

    public bool HasAny => Count != 0;

    [Pure]
    public override bool Equals(object obj) => obj is ItemCount<TItem> other && Equals(other);

    [Pure]
    public bool Equals(ItemCount<TItem> other)
        => Count == other.Count
        && EqualityComparer<TItem>.Default.Equals(Item, other.Item);

    [Pure]
    public override int GetHashCode()
        => unchecked(Count.GetHashCode() * 15 + (Item is { } ? Item.GetHashCode() : 0));

    [Pure]
    public override string ToString() => $"{Item}, Count = {Count}";
}
