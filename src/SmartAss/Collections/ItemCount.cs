namespace SmartAss.Collections;

public static class ItemCount
{
    public static ItemCount<TItem> Create<TItem>(TItem item, long count) => new(item, count);
    public static ItemCount<TItem> Create<TItem>(KeyValuePair<TItem, long> kvp) => new(kvp.Key, kvp.Value);
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
    public bool Any() => Count != 0;

    public override bool Equals(object obj) => obj is ItemCount<TItem> other && Equals(other);
    public bool Equals(ItemCount<TItem> other)
        => Count == other.Count
        && EqualityComparer<TItem>.Default.Equals(Item, other.Item);
    public override int GetHashCode()
        => unchecked(Count.GetHashCode() * 15 + (Item is { } ? Item.GetHashCode() : 0));

    public override string ToString() => $"{Item}, Count = {Count}";
}
