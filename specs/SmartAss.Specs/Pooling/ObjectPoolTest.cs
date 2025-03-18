using SmartAss.Pooling;

namespace Pooling_specs;

public class Object_pool
{
    [Test]
    public void Populates_fills_totally()
    {
        var buffer = new ObjectPool<TestItem>(16);
        buffer.Populate(() => new TestItem(), 16);

        buffer.Should().HaveCount(16);
    }


    [Test]
    public void Reuse_Example()
    {
        using (var resuable = ExampleItem.New())
        {
            ExampleItem example = resuable;
            example.Value = 17;
        }

        ExampleItem.pool.Should().HaveCount(1);
        ExampleItem.New().Item.Value.Should().Be(0);
    }


    public class TestItem
    {
        public int Value { get; set; }
    }


    public class ExampleItem
    {
        public static readonly ObjectPool<ExampleItem> pool = new(16);

        private ExampleItem() { }

        public int Value { get; set; }


        public static Reusable<ExampleItem> New()
        {
            var reusable = pool.Reusable(() => new ExampleItem());
            reusable.Item.Value = 0;
            return reusable;
        }
    }
}
