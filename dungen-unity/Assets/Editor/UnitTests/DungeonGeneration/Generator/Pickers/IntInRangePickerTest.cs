using NUnit.Framework;

namespace DungeonGeneration.Generator.Pickers {
    public class IntInRangePickerTest {
        [Test]
        public void drawingIntSequenceUsingSeededStrategy() {
            IntInRangePicker picker = new IntInRangePicker(1, 5, new RandomSeededPickerStrategy(123456));
            Assert.AreEqual(2, picker.draw());
            Assert.AreEqual(1, picker.draw());
            Assert.AreEqual(1, picker.draw());
            Assert.AreEqual(2, picker.draw());
            Assert.AreEqual(5, picker.draw());
        }
    }
}