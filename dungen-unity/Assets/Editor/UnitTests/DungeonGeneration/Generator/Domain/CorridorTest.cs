using NUnit.Framework;
using DungeonGeneration.Generator.Plotters;

namespace DungeonGeneration.Generator.Domain {

    public class CorridorTest {
        [Test]
        public void plotting_detailed_oneCorridor3x4Horizontally() {
            Corridor room = new Corridor(new Cell(0, 0), new Grid(3, 4), Corridor.Orientation.horizontal);

            int[,] result = new int[3, 4];
            room.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[3, 4] { { 13, 2, 2, 12},
                                            { 1, 1, 1, 1},
                                            { 10, 4, 4, 11}};

            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void plotting_oneCorridor4x3Vertically() {
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(4, 3), Corridor.Orientation.vertical);

            int[,] result = new int[4, 3];
            corr.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[4, 3] { { 11, 1, 10},
                                            { 5, 1, 3},
                                            { 5, 1, 3},
                                            { 12, 1, 13}
        };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void isWithin_oneCorridorWithinBounds() {
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(4, 3), Corridor.Orientation.vertical);
            Assert.IsTrue(corr.isWithin(new Grid(5, 5)));
        }

        [Test]
        public void isWithin_oneCorridorOutsideBounds() {
            Corridor corr = new Corridor(new Cell(-1, 0), new Grid(4, 3), Corridor.Orientation.vertical);
            Assert.IsFalse(corr.isWithin(new Grid(5, 5)));
        }
    }
}