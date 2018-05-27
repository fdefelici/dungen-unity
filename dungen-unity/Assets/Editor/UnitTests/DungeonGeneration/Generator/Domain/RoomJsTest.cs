using NUnit.Framework;
using DungeonGeneration.Generator.Plotters;

namespace DungeonGeneration.Generator.Domain {
    public class RoomJsTest {
        [Test]
        public void walkableCell_oneRoom3x3() {
            Room room = new Room(new Cell(0, 0), new Grid(3, 3));

            Cell[] result = room.walkableCells(false);
            Cell[] expected = new Cell[1] { new Cell(1,1) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void walkableCell_roomWithEastOutcomingCorridor() {
            Room room = new Room(new Cell(0, 0), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(0, 2), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);

            Cell[] result = room.walkableCells(false);
            Cell[] expected = new Cell[1] { new Cell(1, 1) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void walkableCell_roomWithSouthOutcomingCorridor() {
            Room room = new Room(new Cell(0, 0), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(2, 0), new Grid(3, 3), Corridor.Orientation.vertical);
            room.setCorridorOutcoming(corr);

            Cell[] result = room.walkableCells(false);
            Cell[] expected = new Cell[1] { new Cell(1, 1) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void walkableCell_roomWithWestOutcomingCorridor() {
            Room room = new Room(new Cell(0, 2), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);

            Cell[] result = room.walkableCells(false);
            Cell[] expected = new Cell[1] { new Cell(1, 3) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void walkableCell_roomWithNorthOuttcomingCorridor() {
            Room room = new Room(new Cell(2, 0), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);

            Cell[] result = room.walkableCells(false);
            Cell[] expected = new Cell[1] { new Cell(3, 1) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void walkableCell_roomWithEastIncomingCorridor() {
            Room room = new Room(new Cell(0, 0), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(0, 2), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorIncoming(corr);

            Cell[] result = room.walkableCells(false);
            Cell[] expected = new Cell[1] { new Cell(1, 1) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void walkableCell_roomWithSouthIncomingCorridor() {
            Room room = new Room(new Cell(0, 0), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(2, 0), new Grid(3, 3), Corridor.Orientation.vertical);
            room.setCorridorIncoming(corr);

            Cell[] result = room.walkableCells(false);
            Cell[] expected = new Cell[1] { new Cell(1, 1) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void walkableCell_roomWithWestIncomingCorridor() {
            Room room = new Room(new Cell(0, 2), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorIncoming(corr);

            Cell[] result = room.walkableCells(false);
            Cell[] expected = new Cell[1] { new Cell(1, 3) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void walkableCell_roomWithNorthIncomingCorridor() {
            Room room = new Room(new Cell(2, 0), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorIncoming(corr);

            Cell[] result = room.walkableCells(false);
            Cell[] expected = new Cell[1] { new Cell(3, 1) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }


        [Test]
        public void walkableCell_oneRoom5x3withOneEastOutcomingCorridor3x3() {
            Room room = new Room(new Cell(0, 0), new Grid(5, 3));
            Corridor corr = new Corridor(new Cell(1, 2), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);

            Cell[] result = room.walkableCells(false);
            Cell[] expected = new Cell[3] { new Cell(1, 1), new Cell(2, 1), new Cell(3, 1) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

        [Test]
        public void hasCorridor_atEast() {
            Room room = new Room(new Cell(0, 0), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(0, 2), new Grid(3, 3), Corridor.Orientation.horizontal);

            room.setCorridorOutcoming(corr);
            room.setCorridorIncoming(null);
            Assert.IsTrue(room.hasCorridorAtEast());
            Assert.IsFalse(room.hasCorridorAtSouth());
            Assert.IsFalse(room.hasCorridorAtWest());
            Assert.IsFalse(room.hasCorridorAtNorth());

            room.setCorridorOutcoming(null);
            room.setCorridorIncoming(corr);
            Assert.IsTrue(room.hasCorridorAtEast());
            Assert.IsFalse(room.hasCorridorAtSouth());
            Assert.IsFalse(room.hasCorridorAtWest());
            Assert.IsFalse(room.hasCorridorAtNorth());
        }

        [Test]
        public void hasCorridor_atSouth() {
            Room room = new Room(new Cell(0, 0), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(2, 0), new Grid(3, 3), Corridor.Orientation.vertical);

            room.setCorridorOutcoming(corr);
            room.setCorridorIncoming(null);
            Assert.IsFalse(room.hasCorridorAtEast());
            Assert.IsTrue(room.hasCorridorAtSouth());
            Assert.IsFalse(room.hasCorridorAtWest());
            Assert.IsFalse(room.hasCorridorAtNorth());

            room.setCorridorOutcoming(null);
            room.setCorridorIncoming(corr);
            Assert.IsFalse(room.hasCorridorAtEast());
            Assert.IsTrue(room.hasCorridorAtSouth());
            Assert.IsFalse(room.hasCorridorAtWest());
            Assert.IsFalse(room.hasCorridorAtNorth());
        }

        [Test]
        public void hasCorridor_atWest() {
            Room room = new Room(new Cell(0, 2), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 3), Corridor.Orientation.horizontal);

            room.setCorridorOutcoming(corr);
            room.setCorridorIncoming(null);
            Assert.IsFalse(room.hasCorridorAtEast());
            Assert.IsFalse(room.hasCorridorAtSouth());
            Assert.IsTrue(room.hasCorridorAtWest());
            Assert.IsFalse(room.hasCorridorAtNorth());

            room.setCorridorOutcoming(null);
            room.setCorridorIncoming(corr);
            Assert.IsFalse(room.hasCorridorAtEast());
            Assert.IsFalse(room.hasCorridorAtSouth());
            Assert.IsTrue(room.hasCorridorAtWest());
            Assert.IsFalse(room.hasCorridorAtNorth());
        }

        [Test]
        public void hasCorridor_atNorth() {
            Room room = new Room(new Cell(2, 0), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 3), Corridor.Orientation.horizontal);

            room.setCorridorOutcoming(corr);
            room.setCorridorIncoming(null);
            Assert.IsFalse(room.hasCorridorAtEast());
            Assert.IsFalse(room.hasCorridorAtSouth());
            Assert.IsFalse(room.hasCorridorAtWest());
            Assert.IsTrue(room.hasCorridorAtNorth());

            room.setCorridorOutcoming(null);
            room.setCorridorIncoming(corr);
            Assert.IsFalse(room.hasCorridorAtEast());
            Assert.IsFalse(room.hasCorridorAtSouth());
            Assert.IsFalse(room.hasCorridorAtWest());
            Assert.IsTrue(room.hasCorridorAtNorth());
        }

        [Test]
        public void cellFacingOutCorridor_atEast() {
            //     0 1 2 3 4 
            //
            // 0   1 1 1 1 1
            // 1   1 0 0 0 0
            // 2   1 1 1 1 1

            Room room = new Room(new Cell(0, 0), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(0, 2), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);
            room.setCorridorIncoming(null);

            Cell[] result = room.cellsFacingOutcomingCorridor();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(new Cell(1, 1), result[0]);
        }

        [Test]
        public void cellFacingOutCorridor_atWest() {
            //     0 1 2 3 4 
            //
            // 0   1 1 1 1 1
            // 1   0 0 0 0 1
            // 2   1 1 1 1 1

            Room room = new Room(new Cell(0, 2), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 3), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);
            room.setCorridorIncoming(null);

            Cell[] result = room.cellsFacingOutcomingCorridor();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(new Cell(1, 3), result[0]);
        }

        [Test]
        public void cellFacingOutCorridor_atSouth() {
            //     0 1 2  
            //
            // 0   1 1 1 
            // 1   1 0 1 
            // 2   1 0 1 
            // 3   1 0 1 
            // 4   1 0 1 

            Room room = new Room(new Cell(0, 0), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(2, 0), new Grid(3, 3), Corridor.Orientation.vertical);
            room.setCorridorOutcoming(corr);
            room.setCorridorIncoming(null);

            Cell[] result = room.cellsFacingOutcomingCorridor();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(new Cell(1, 1), result[0]);
        }

        [Test]
        public void cellFacingOutCorridor_atNorth() {
            //     0 1 2  
            //
            // 0   1 0 1 
            // 1   1 0 1 
            // 2   1 0 1 
            // 3   1 0 1 
            // 4   1 1 1 

            Room room = new Room(new Cell(2, 0), new Grid(3, 3));
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(3, 3), Corridor.Orientation.vertical);
            room.setCorridorOutcoming(corr);
            room.setCorridorIncoming(null);

            Cell[] result = room.cellsFacingOutcomingCorridor();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(new Cell(3, 1), result[0]);
        }

        [Test]
        public void bug() {
            //     0 1 2 3 4 5 6 7 8 
            //
            // 0   1 1 1 1 1 1 1 1 1
            // 1   1 0 0 0 0 0 0 0 1
            // 2   1 0 0 1 1 1 0 0 1
            // 3   1 1 1 1   1 1 1 1

            Room room1 = new Room(new Cell(0, 0), new Grid(4, 4));
            Corridor corr = new Corridor(new Cell(0, 3), new Grid(3, 3), Corridor.Orientation.horizontal);
            Room room2 = new Room(new Cell(0, 5), new Grid(4, 4));
            room1.setCorridorOutcoming(corr);
            room2.setCorridorIncoming(corr);

            Cell[] result = room1.cellsFacingOutcomingCorridor();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(new Cell(1, 2), result[0]);

            result = room2.cellsFacingIncomingCorridor();
            Assert.AreEqual(1, result.Length);
            Assert.AreEqual(new Cell(1, 6), result[0]);

            Assert.AreEqual(4, room1.walkableCells(false).Length);
            Assert.AreEqual(4, room2.walkableCells(false).Length);
            Assert.AreEqual(3, corr.walkableCells(false).Length);
        }

        [Test]
        public void walkableCell_ExcludingCellNextToWall_oneRoom3x3() {
            Room room = new Room(new Cell(0, 0), new Grid(3, 3));

            Cell[] result = room.walkableCells(true);
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void walkableCell_ExcludingCellNextToWall_oneRoom4x4() {
            Room room = new Room(new Cell(0, 0), new Grid(5, 5));

            Cell[] result = room.walkableCells(true);
            Cell[] expected = new Cell[1] { new Cell(2, 2) };
            Assert.IsTrue(XTestUtils.areEquals(expected, result));
        }

    }

}