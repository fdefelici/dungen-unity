using NUnit.Framework;

namespace DungeonGeneration.Generator.Domain {
    public class CellTest {
        [Test]
        public void checkCoordWithinOthersCoords() {
            Assert.IsTrue(new Cell(0, 0).isWithin(new Cell(0, 0), new Cell(0, 10)));
            Assert.IsTrue(new Cell(0, 5).isWithin(new Cell(0, 0), new Cell(0, 10)));
            Assert.IsFalse(new Cell(1, 5).isWithin(new Cell(0, 0), new Cell(0, 10)));

            Assert.IsTrue(new Cell(4, 5).isWithin(new Cell(0, 0), new Cell(10, 10)));
            Assert.IsFalse(new Cell(4, 11).isWithin(new Cell(0, 0), new Cell(10, 10)));

            Assert.IsTrue(new Cell(2, 4).isWithin(new Cell(0, 4), new Cell(4, 4)));
        }

        [Test]
        public void checkCellWithinGrid() {
            Assert.IsTrue(new Cell(0, 0).isWithin(new Grid(1, 1)));
            Assert.IsFalse(new Cell(-1, 0).isWithin(new Grid(1, 1)));
        }

        [Test]
        public void distanceBetweenCellsOnSameRow() {
            Cell cell1 = new Cell(0, 0);
            Cell cell2 = new Cell(0, 5);
            Assert.AreEqual(6, cell1.distance(cell2));
            Assert.AreEqual(6, cell2.distance(cell1));
        }

        [Test]
        public void distanceBetweenCellsOnSameColumn() {
            Cell cell1 = new Cell(0, 0);
            Cell cell2 = new Cell(5, 0);
            Assert.AreEqual(6, cell1.distance(cell2));
            Assert.AreEqual(6, cell2.distance(cell1));
        }

        [Test]
        public void distanceBetweenCellsOnDifferentRowAndColumn_case1() {
            Cell cell1 = new Cell(0, 0);
            Cell cell2 = new Cell(1, 1);
            Assert.AreEqual(4, cell1.distance(cell2));
            Assert.AreEqual(4, cell2.distance(cell1));
        }

        [Test]
        public void cellArrayBetweenTwoCells() {
            Cell cell1 = new Cell(0, 0);
            Cell cell2 = new Cell(1, 1);

            Cell[] result = cell1.cells(cell2);
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual(new Cell(0, 0), result[0]);
            Assert.AreEqual(new Cell(0, 1), result[1]);
            Assert.AreEqual(new Cell(1, 0), result[2]);
            Assert.AreEqual(new Cell(1, 1), result[3]);

            result = cell2.cells(cell1);
            Assert.AreEqual(4, result.Length);
            Assert.AreEqual(new Cell(0, 0), result[0]);
            Assert.AreEqual(new Cell(0, 1), result[1]);
            Assert.AreEqual(new Cell(1, 0), result[2]);
            Assert.AreEqual(new Cell(1, 1), result[3]);

        }

        [Test]
        public void distanceBetweenCellsOnDifferentRowAndColumn_case2() {
            Cell cell1 = new Cell(0, 0);
            Cell cell2 = new Cell(2, 2);
            Assert.AreEqual(9, cell1.distance(cell2));
            Assert.AreEqual(9, cell2.distance(cell1));
        }

        [Test]
        public void sumTwoRowsToCell() {
            Cell cell = new Cell(0, 0);
            Assert.AreEqual(new Cell(2, 0), cell.plusCell(2, 0));
        }

        [Test]
        public void isWithin_twoCellsWithinBounds() {
            Grid container = new Grid(40, 25);
            Cell topLeftVertex = new Cell(15, 22);
            Cell botLeftVertex = new Cell(20, 22);
            Assert.IsTrue(topLeftVertex.isWithin(container));
            Assert.IsTrue(botLeftVertex.isWithin(container));
        }

        [Test]
        public void isWithin_twoCellsOutsideBounds() {
            Grid container = new Grid(40, 25);
            Cell topRightVertex = new Cell(15, 26);
            Cell botRightVertex = new Cell(20, 26);
            Assert.IsFalse(topRightVertex.isWithin(container));
            Assert.IsFalse(botRightVertex.isWithin(container));
        }
    }
}