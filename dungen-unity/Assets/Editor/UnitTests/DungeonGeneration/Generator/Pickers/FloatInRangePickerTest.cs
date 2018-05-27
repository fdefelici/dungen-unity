using NUnit.Framework;

namespace DungeonGeneration.Generator.Pickers {
    public class FloatInRangePickerTest {
        [Test]
        public void drawingFloatSequenceUsingSeededStrategy() {
            FloatInRangePicker picker = new FloatInRangePicker(0.1f, 0.5f, new RandomSeededPickerStrategy(123456));
            Assert.AreEqual(0.2f, picker.draw());
            Assert.AreEqual(0.1f, picker.draw());
            Assert.AreEqual(0.1f, picker.draw());
            Assert.AreEqual(0.2f, picker.draw());
            Assert.AreEqual(0.5f, picker.draw());
        }
    }
}