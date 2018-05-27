using NUnit.Framework;
using DungeonGeneration.Generator.Plotters;

namespace DungeonGeneration.Generator.Domain {

    public class CorridorJsTest {
        [Test]
        public void walkableCell_horizontalCorridor() {
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 4), Corridor.Orientation.horizontal);

            Cell[] result = corr.walkableCells(false);
            Cell[] expected = new Cell[4] { new Cell(1, 0), new Cell(1, 1), new Cell(1, 2), new Cell(1, 3) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void walkableCell_verticalCorridor() {
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 4), Corridor.Orientation.vertical);

            Cell[] result = corr.walkableCells(false);
            Cell[] expected = new Cell[6] { new Cell(0, 1), new Cell(0, 2), new Cell(1, 1), new Cell(1, 2), new Cell(2, 1), new Cell(2, 2) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void walkableCell_ExcludingCellNextToWall_horizontalCorridor() {
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(5, 1), Corridor.Orientation.horizontal);

            Cell[] result = corr.walkableCells(true);
            Cell[] expected = new Cell[1] { new Cell(2, 0) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }
    }
}