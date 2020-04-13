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

        public class TestItem
        {
            public int Value { get; set; }
        }


        public class ExampleItem
        {
            private static readonly ObjectPool<ExampleItem> pool = new ObjectPool<ExampleItem>(16);

            private ExampleItem() { }

            public int Value { get; set; }


            public static ExampleItem New()
            {
                var item = pool.Get(() => new ExampleItem());
                item.Value = 0;
                return item;
            }

            public static void Release(ExampleItem item) => pool.Release(item);
        }
    }
}
