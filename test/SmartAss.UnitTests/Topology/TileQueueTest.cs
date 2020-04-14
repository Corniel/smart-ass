using NUnit.Framework;
using SmartAss.Collections;
using SmartAss.Topology;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartAss.UnitTests.Topology
{
    public class TileQueueTest
    {
        [Test]
        public void GetEnumerator()
        {
            var map = new DummyMap(4);
            var queue = new TileQueue<DummyTile>(map);
            foreach (var tile in map)
            {
                queue.Enqueue(tile);
            }
            queue.Dequeue();

            var left = queue.ToArray();

            var expected = new[] { new DummyTile(1), new DummyTile(2), new DummyTile(3) };

            Assert.AreEqual(expected, left);
        }


        private class DummyMap : Map<DummyTile>
        {
            public DummyMap(int size) : base(size)
            {
                for(var i = 0; i < size; i++)
                {
                    Tiles[i] = new DummyTile(i);
                }
            }
        }

        private class DummyTile : ITile<DummyTile>, IEquatable<DummyTile>
        {
            public DummyTile(int index) => Index = index;
            public int Index { get; }

            public bool Equals(DummyTile other)=> Index == other.Index;

            public override string ToString() => Index.ToString();

            public IEnumerable<ITile> GetNeighbors() => throw new NotImplementedException();
            public SimpleList<DummyTile> Neighbors => throw new NotImplementedException();

        }
    }
}
