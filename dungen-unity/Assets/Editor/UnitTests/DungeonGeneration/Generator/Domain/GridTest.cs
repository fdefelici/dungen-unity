using NUnit.Framework;

namespace DungeonGeneration.Generator.Domain {
    public class GridTest {
        [Test]
        public void vertex_checkAbsoluteVertexes() {
            Grid container = new Grid(100, 100);
            Cell absTopLeftVertex = new Cell(46, 62);

            Assert.AreEqual(new Cell(46, 161), container.absTopRightVertexUsing(absTopLeftVertex));
            Assert.AreEqual(new Cell(145, 161), container.absBotRightVertexUsing(absTopLeftVertex));
            Assert.AreEqual(new Cell(145, 62), container.absBotLeftVertexUsing(absTopLeftVertex));
        }

        [Test]
        public void isWithin_gridWithinBounds_case1() {
            Grid containee = new Grid(4, 4);
            Grid container = new Grid(4, 4);
            Assert.IsTrue(containee.isWithin(container, new Cell(0, 0)));
        }

        [Test]
        public void isWithin_gridWithinBounds_case2() {
            Grid containee = new Grid(14, 14);
            Grid container = new Grid(100, 100);
            Assert.IsTrue(containee.isWithin(container, new Cell(46, 62)));
        }

        [Test]
        public void isWithin_gridOutsideBounds_case1() {
            Grid containee = new Grid(4, 4);
            Grid container = new Grid(4, 4);
            Assert.IsFalse(containee.isWithin(container, new Cell(-1, 0)));
        }

        [Test]
        public void isWithin_gridOutsideBounds_case2() {
            Grid containee = new Grid(6, 5);
            Grid container = new Grid(40, 25);
            Assert.IsFalse(containee.isWithin(container, new Cell(15, 22)));
        }

        [Test]
        public void checkIfCellExistInGrid() {
            Grid container = new Grid(4, 4);
            Assert.IsTrue(container.hasCell(0, 0));
            Assert.IsFalse(container.hasCell(-1, 0));
        }

        [Test]
        public void absoluteVertexes_case1() {
            Grid container = new Grid(6, 5);
            Cell topLeftVertex = new Cell(15, 22);

            Assert.AreEqual(new Cell(15, 22), container.absTopLeftVertexUsing(topLeftVertex));
            Assert.AreEqual(new Cell(15, 26), container.absTopRightVertexUsing(topLeftVertex));
            Assert.AreEqual(new Cell(20, 26), container.absBotRightVertexUsing(topLeftVertex));
            Assert.AreEqual(new Cell(20, 22), container.absBotLeftVertexUsing(topLeftVertex));
        }

        [Test]
        public void hasCell_gridNoContainCell() {
            Grid container = new Grid(40, 25);
            Assert.IsFalse(container.hasCell(15, 26));

            //if (15 < 0 || 15 >= 40) return false;
            //if (26 < 0 || 26 >= 25) return false;

        }
    }
}