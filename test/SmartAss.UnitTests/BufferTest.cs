using NUnit.Framework;
using SmartAss.Buffering;

namespace Tests
{
    public class BufferTest
    {
        [Test]
        public void Populate_ShouldBeFull()
        {
            var buffer = new Buffer<TestItem>(16);
            buffer.Populate();

            Assert.AreEqual(16, buffer.Count);
        }

        private class TestItem : IBufferable
        {
            public int Value { get; set; }

            public void Reset()
            {
                Value = 0;
            }
        }
    }


}