using NUnit.Framework;
using System;

namespace DungeonGeneration.Generator.Pickers {
    public class CustomSeededPickerStrategyTest {
        [Test]
        public void seed_0_range_1_10() {
            CustomSeededPickerStrategy strategy = new CustomSeededPickerStrategy(0);
            Assert.AreEqual(1, strategy.drawBetween(1, 10), "Draw #1");
            Assert.AreEqual(7, strategy.drawBetween(1, 10), "Draw #2");
            Assert.AreEqual(10, strategy.drawBetween(1, 10), "Draw #3");
            Assert.AreEqual(2, strategy.drawBetween(1, 10), "Draw #4");
            Assert.AreEqual(10, strategy.drawBetween(1, 10), "Draw #5");
        }

        [Test]
        public void seed_neg_1_range_1_10() {
            CustomSeededPickerStrategy strategy = new CustomSeededPickerStrategy(-1);
            Assert.AreEqual(3, strategy.drawBetween(1, 10));
            Assert.AreEqual(1, strategy.drawBetween(1, 10));
            Assert.AreEqual(8, strategy.drawBetween(1, 10));
        }

        [Test]
        public void seed_MaxInt_range_1_10() {
            CustomSeededPickerStrategy strategy = new CustomSeededPickerStrategy(Int32.MaxValue);
            Assert.AreEqual(8, strategy.drawBetween(1, 10));
            Assert.AreEqual(1, strategy.drawBetween(1, 10));
            Assert.AreEqual(2, strategy.drawBetween(1, 10));
        }

        [Test]
        public void seed_MinInt_range_1_10() {
            CustomSeededPickerStrategy strategy = new CustomSeededPickerStrategy(Int32.MinValue);
            Assert.AreEqual(1, strategy.drawBetween(1, 10));
            Assert.AreEqual(8, strategy.drawBetween(1, 10));
            Assert.AreEqual(6, strategy.drawBetween(1, 10));
        }

        [Test]
        public void seed_0_range_inverted_10_1() {
            CustomSeededPickerStrategy strategy = new CustomSeededPickerStrategy(0);
            Assert.AreEqual(10, strategy.drawBetween(10, 1));
            Assert.AreEqual(4, strategy.drawBetween(10, 1));
            Assert.AreEqual(1, strategy.drawBetween(10, 1));
        }

        [Test]
        public void seed_0_range_collapsed_5_5() {
            CustomSeededPickerStrategy strategy = new CustomSeededPickerStrategy(0);
            Assert.AreEqual(5, strategy.drawBetween(5, 5));
            Assert.AreEqual(5, strategy.drawBetween(5, 5));
            Assert.AreEqual(5, strategy.drawBetween(5, 5));
        }
    }
}