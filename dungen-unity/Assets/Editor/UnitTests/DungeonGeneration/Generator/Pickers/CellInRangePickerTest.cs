using NUnit.Framework;
using DungeonGeneration.Generator.Domain;

namespace DungeonGeneration.Generator.Pickers {
    public class CellInRangePickerTest {
        [Test]
        public void pickingCellBetweenCellsOnSameRow() {
            CellInRangePicker picker = new CellInRangePicker(new MockPickerStrategy(4));
            Cell result = picker.drawBetween(new Cell(0, 0), new Cell(0, 5));
            Assert.AreEqual(new Cell(0, 3), result);
        }

        [Test]
        public void pickingCellBetweenCellsOnSameColumn() {
            CellInRangePicker picker = new CellInRangePicker(new MockPickerStrategy(4));
            Cell result = picker.drawBetween(new Cell(0, 0), new Cell(5, 0));
            Assert.AreEqual(new Cell(3, 0), result);
        }

        [Test]
        public void drawInRangeCollapsedOnOneCell() {
            CellInRangePicker picker = new CellInRangePicker(new MockPickerStrategy(1));
            Cell result = picker.drawBetween(new Cell(0, 0), new Cell(0, 0));
            Assert.AreEqual(new Cell(0, 0), result);
        }

        [Test]
        public void drawInRangeOnConsecutiveCell() {
            CellInRangePicker picker = new CellInRangePicker(new MockPickerStrategy(1));
            Cell result = picker.drawBetween(new Cell(0, 0), new Cell(0, 1));
            Assert.AreEqual(new Cell(0, 0), result);
        }

        [Test]
        public void drawInRangeOnConsecutiveCellsBothExcluded() {
            CellInRangePicker picker = new CellInRangePicker(new MockPickerStrategy(1));
            Cell result = picker.drawBetweenWithExclusion(new Cell(0, 0), new Cell(0, 1), new Cell(0, 0), new Cell(0, 1));
            Assert.AreEqual(new Cell(0, 0), result);
        }


        [Test]
        public void drawWithOneExclusionSkippedOnMinValue() {
            CellInRangePicker picker = new CellInRangePicker(new MockPickerStrategy(1));
            Cell result = picker.drawBetweenWithExclusion(new Cell(0, 0), new Cell(0, 5), new Cell(0, 0));
            Assert.AreEqual(new Cell(0, 1), result);
        }

        [Test]
        public void drawWithTwoExclusionsSkippedOnMinValue() {
            CellInRangePicker picker = new CellInRangePicker(new MockPickerStrategy(1));
            Cell result = picker.drawBetweenWithExclusion(new Cell(0, 0), new Cell(0, 5), new Cell(0, 0), new Cell(0, 1));
            Assert.AreEqual(new Cell(0, 2), result);
        }
    }
}