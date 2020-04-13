using NUnit.Framework;
using SmartAss.Pooling;

namespace SmartAss.Tests.Pooling
{
    public class ObjectPoolTest
    {
        [Test]
        public void Populate_ShouldBeFull()
        {
            var buffer = new ObjectPool<TestItem>(16);
            buffer.Populate(() => new TestItem());

            Assert.AreEqual(16, buffer.Count);
        }


        [Test]
        public void Reuse_Example()
        {
            using (var resuable = ExampleItem.New())
            {
                var example = resuable;
            }

            Assert.AreEqual(1, ExampleItem.pool.Count);
        }


        public class TestItem
        {
            public int Value { get; set; }
        }


        public class ExampleItem
        {
            public static readonly ObjectPool<ExampleItem> pool = new ObjectPool<ExampleItem>(16);

            private ExampleItem() { }

            public int Value { get; set; }


            public static Reusable<ExampleItem> New()
            {
                var reusable = pool.Get(() => new ExampleItem());
                reusable.Item.Value = 0;
                return reusable;
            }
        }
    }
}
