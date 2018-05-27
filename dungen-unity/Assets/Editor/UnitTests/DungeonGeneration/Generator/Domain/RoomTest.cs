using NUnit.Framework;
using DungeonGeneration.Generator.Plotters;

namespace DungeonGeneration.Generator.Domain {

    public class RoomTest {
        [Test]
        public void plotting_detailed_oneRoom5x5() {
            Room room = new Room(new Cell(0, 0), new Grid(5, 5));

            int[,] result = new int[5, 5];
            room.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[5, 5] { { 6, 0, 2, 0, 7},
                                            { 0, 1, 1, 1, 0},
                                            { 5, 1, 1, 1, 3},
                                            { 0, 1, 1, 1, 0},
                                            { 9, 0, 4, 0, 8}};
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void plotting_detailed_oneRoom4x8() {
            Room room = new Room(new Cell(0, 0), new Grid(4, 8));
            int[,] result = new int[4, 8];
            room.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[4, 8] { { 6,0,2,2,2,2,0,7 },
                                            { 0,1,1,1,1,1,1,0},
                                            { 0,1,1,1,1,1,1,0},
                                            { 9,0,4,4,4,4,0,8}};
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }


        public void plotting_detailed_twoRoomsWithoutOverlapping() {
            Room room5x5 = new Room(new Cell(0, 0), new Grid(5, 5));
            Room room4x4 = new Room(new Cell(6, 0), new Grid(4, 4));

            int[,] result = new int[10, 5];
            room5x5.plotOn(result, new DetailedTilesPlotter());
            room4x4.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[10, 5]{ { 6, 2, 2, 0, 7},
                                              { 0, 1, 1, 1, 0},
                                              { 5, 1, 1, 1, 3},
                                              { 0, 1, 1, 1, 0},
                                              { 9, 0, 4, 0, 8},
                                              { 0, 0, 0, 0, 0},
                                              { 6, 0, 0, 7, 0},
                                              { 0, 1, 1, 0, 0},
                                              { 0, 1, 1, 0, 0},
                                              { 9, 0, 0, 8, 0}};
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void plotting_detailed_oneRoomAndOneCorridorHorizontalSharingBottomRightRoomVertex() {
            Room room = new Room(new Cell(0, 0), new Grid(5, 4));
            Corridor corr = new Corridor(new Cell(2, 3), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            int[,] result = new int[5, 6];
            room.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[5, 6]{ { 6, 0, 0, 7, 0, 0  },
                                            { 0, 1, 1, 0, 0, 0  },
                                            { 5, 1, 1,13, 2, 12 },
                                            { 0, 1, 1, 1, 1, 1  },
                                            { 9, 0, 4, 4, 4, 11 }};
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void plotting_detailed_oneRoomAndOneCorridorVerticalSharingBottomRightRoomVertex() {
            Room room = new Room(new Cell(0, 0), new Grid(4, 5));
            Corridor corr = new Corridor(new Cell(3, 2), new Grid(3, 3), Corridor.Orientation.vertical);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            int[,] result = new int[6, 5];
            room.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[6, 5]{ { 6, 0, 2, 0, 7 },
                                            { 0, 1, 1, 1, 0 },
                                            { 0, 1, 1, 1, 3 },
                                            { 9, 0,11, 1, 3 },
                                            { 0, 0, 5, 1, 3 },
                                            { 0, 0, 12, 1,13 }};
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void plotting_detailed_oneRoomAndOneCorridorVerticalSharingBottomLeftRoomVertex() {
            Room room = new Room(new Cell(0, 0), new Grid(5, 5));
            Corridor corr = new Corridor(new Cell(4, 0), new Grid(3, 3), Corridor.Orientation.vertical);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            int[,] result = new int[7, 5];
            room.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[7, 5]{ {  6, 0,  2, 0, 7 },
                                            {  0, 1,  1, 1, 0 },
                                            {  5, 1,  1, 1, 3 },
                                            {  5, 1,  1, 1, 0 },
                                            {  5, 1, 10, 0, 8 },
                                            {  5, 1,  3, 0, 0 },
                                            { 12, 1, 13, 0, 0 }
        };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void plotting_detailed_oneRoomAndOneCorridorHorizontalSharingBottomLeftRoomVertex() {
            Room room = new Room(new Cell(0, 2), new Grid(5, 5));
            Corridor corr = new Corridor(new Cell(2, 0), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            int[,] result = new int[5, 7];
            room.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[5, 7]{ {0,  0,  6, 0, 2, 0, 7},
                                            {0,  0,  0, 1, 1, 1, 0},
                                            {13, 2, 12, 1, 1, 1, 3},
                                            {1,  1,  1, 1, 1, 1, 0},
                                            {10, 4,  4, 4, 4, 0, 8}};
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void plotting_oneRoomAndOneCorridorHorizontalSharingTopLeftRoomVertex() {
            Room room = new Room(new Cell(0, 4), new Grid(5, 5));
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            int[,] result = new int[5, 9];
            room.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[5, 9]{ {13, 2, 12, 0, 6, 0, 2, 0, 7},
                                            {1, 1, 1, 0, 0, 1, 1, 1, 0},
                                            {10, 4, 11, 0, 5, 1, 1, 1, 3},
                                            {0, 0, 0, 0, 0, 1, 1, 1, 0},
                                            {0, 0, 0, 0, 9, 0, 4, 0, 8}
        };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void plotting_oneRoomAndOneCorridorVerticalSharingTopLeftRoomVertex() {
            Room room = new Room(new Cell(2, 0), new Grid(5, 5));
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 3), Corridor.Orientation.vertical);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            int[,] result = new int[7, 5];
            room.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[7, 5]{ {11, 1, 10, 0, 0},
                                            {5, 1, 3, 0, 0},
                                            {5, 1, 13, 0, 7},
                                            {5, 1, 1, 1, 0},
                                            {5, 1, 1, 1, 3},
                                            {0, 1, 1, 1, 0},
                                            {9, 0, 4, 0, 8}
        };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void plotting_oneRoomAndOneCorridorVerticalSharingTopRightRoomVertex() {
            Room room = new Room(new Cell(3, 0), new Grid(5, 5));
            Corridor corr = new Corridor(new Cell(0, 2), new Grid(3, 3), Corridor.Orientation.vertical);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            int[,] result = new int[8, 5];
            room.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[8, 5]{  {0, 0, 11, 1, 10},
                                            {0, 0, 5,  1, 3},
                                            {0, 0, 12, 1, 13},
                                            {6, 0,  2,  0, 7},
                                            {0, 1, 1, 1, 0},
                                            {5, 1, 1, 1, 3},
                                            {0, 1, 1, 1, 0},
                                            {9, 0, 4, 0, 8}
        };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void plotting_oneRoomAndOneCorridorHorizontalSharingTopRightRoomVertex() {
            Room room = new Room(new Cell(0, 0), new Grid(5, 5));
            Corridor corr = new Corridor(new Cell(0, 4), new Grid(3, 3), Corridor.Orientation.vertical);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            int[,] result = new int[5, 8];
            room.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[5, 8]{  {6, 0, 2, 2, 5, 1, 10, 0},
                                            {0, 1, 1, 1, 5, 1, 3, 0},
                                            {5, 1, 1, 1, 12, 1, 13, 0},
                                            {0, 1, 1, 1, 0, 0, 0, 0},
                                            {9, 0, 4, 0, 8, 0, 0, 0}};
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void plotting_detailed_oneRoomAndOneIncomingHorizontalCorridorSharingTopRightRoomVertex() {
            Room room = new Room(new Cell(0, 2), new Grid(5, 5));
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorIncoming(corr);
            corr.setDestinationRoom(room);

            int[,] result = new int[5, 7];
            corr.plotOn(result, new DetailedTilesPlotter());

            int[,] expected = new int[5, 7]{  {13, 2, 2, 2, 2, 0, 7},
                                            {1, 1, 1, 1, 1, 1, 0},
                                            {10, 4, 11, 1, 1, 1, 3},
                                            {0, 0, 0, 1, 1, 1, 0},
                                            {0, 0, 9, 0, 4, 0, 8}};
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void isWithin_aRoomWithinBounds() {
            Room room = new Room(new Cell(46, 62), new Grid(14, 14));
            Grid container = new Grid(100, 100);

            Assert.IsTrue(room.isWithin(container));
        }

        [Test]
        public void collision_twoRoomsCollideOnCellsWhichAreNotVertexes() {
            Room room = new Room(new Cell(0, 4), new Grid(10, 10));
            Room room2 = new Room(new Cell(3, 0), new Grid(4, 20));

            Assert.IsTrue(room.collidesWith(room2));
        }

        [Test]
        public void collision_twoRoomsCollideOnASide() {
            Room room = new Room(new Cell(0, 0), new Grid(5, 5));
            Room room2 = new Room(new Cell(0, 4), new Grid(4, 4));

            Assert.IsTrue(room.collidesWith(room2));
        }

        [Test]
        public void isWithin_aRoomOutsideBounds() {
            Grid bounds = new Grid(40, 25);
            Room room = new Room(new Cell(15, 22), new Grid(6, 5));
            Assert.IsFalse(room.isWithin(bounds));
        }
    }

}