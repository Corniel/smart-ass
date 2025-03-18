using SmartAss.Maps;

namespace TileQueue_specs;

public class None_empty_queue
{
    [Test]
    public void dequeue_returns_tail()
    {
        var queueu = new TileQueue<IntTile>(2).Enqueue(1).Enqueue(2);
        
        queueu.Dequeue().Should().Be(new IntTile(1));

        queueu.Should().BeEquivalentTo(new IntTile[] { 2 });
    }
}

internal sealed record IntTile(int Id) : Tile, IEquatable<IntTile>
{
    public IEnumerable<Tile> GetNeighbors() => [];
    public static implicit operator IntTile(int id) => new(id);
}
