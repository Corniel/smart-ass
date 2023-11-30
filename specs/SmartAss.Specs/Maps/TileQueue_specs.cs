using SmartAss.Maps;

namespace TileQueue_specs;

public class None_empty_queue
{
    [Test]
    public void dequeue_returns_tail()
    {
        var queueu = new TileQueue<IntTile>(2).Enqueue(1).Enqueue(2);
        Assert.AreEqual(new IntTile(1), queueu.Dequeue());
        Assert.AreEqual(new IntTile[] { 2 }, queueu.ToArray());
    }
}

internal sealed class IntTile : Tile, IEquatable<IntTile>
{
    public IntTile(int id) => Id = id;
    public int Id { get; }
    public bool Equals(IntTile other) => Id == other.Id;
    public override string ToString() => Id.ToString();
    public IEnumerable<Tile> GetNeighbors() => Array.Empty<Tile>();
    public static implicit operator IntTile(int id) => new(id);
}
