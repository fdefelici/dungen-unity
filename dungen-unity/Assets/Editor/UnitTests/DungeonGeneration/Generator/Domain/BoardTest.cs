using NUnit.Framework;

namespace DungeonGeneration.Generator.Domain {

    public class BoardTest {
        [Test]
        public void fitting_oneCorridorWithinBounds() {
            Board board = new Board(new Grid(10, 10));
            bool result = board.fitsIn(new Corridor(new Cell(0, 0), new Grid(4, 4), Corridor.Orientation.horizontal));
            Assert.IsTrue(result);
        }

        [Test]
        public void fitting_oneCorridorOutsideBounds() {
            Board board = new Board(new Grid(10, 10));
            bool result = board.fitsIn(new Corridor(new Cell(-1, 0), new Grid(4, 4), Corridor.Orientation.horizontal));
            Assert.IsFalse(result);
        }

        [Test]
        public void fitting_oneRoomWithinBounds() {
            Board board = new Board(new Grid(100, 100));
            bool result = board.fitsIn(new Room(new Cell(70, 66), new Grid(15, 6)));
            Assert.IsTrue(result);
        }

        [Test]
        public void fitting_case1() {
            Board board = new Board(new Grid(100, 100));
            Room room1 = new Room(new Cell(0, 0), new Grid(13, 5));
            Corridor corr1 = new Corridor(new Cell(4, 4), new Grid(3, 5), Corridor.Orientation.horizontal);
            Room room2 = new Room(new Cell(2, 8), new Grid(9, 5));
            Corridor corr2 = new Corridor(new Cell(1, 8), new Grid(2, 3), Corridor.Orientation.vertical);
            board.addRoom(room1);
            board.addCorridor(corr1);
            board.addRoom(room2);
            board.addCorridor(corr2);

            board.removeLast();

            Corridor corr3 = new Corridor(new Cell(4, 12), new Grid(3, 4), Corridor.Orientation.horizontal);
            Assert.IsTrue(board.fitsIn(corr3));
        }

        [Test]
        public void fitting_case2() {
            Board board = new Board(new Grid(100, 100));
            Corridor corr = new Corridor(new Cell(75, 61), new Grid(3, 6), Corridor.Orientation.horizontal);
            board.addCorridor(corr);

            Room room = new Room(new Cell(70, 66), new Grid(15, 6));
            Assert.IsTrue(board.fitsIn(room));
        }

        [Test]
        public void fitting_case3() {
            Board board = new Board(new Grid(40, 25));
            bool result = board.fitsIn(new Room(new Cell(15, 22), new Grid(6, 5)));
            Assert.IsFalse(result);
        }


        [Test]
        public void checkSizeAfterRemovingLast() {
            Board board = new Board(new Grid(100, 100));
            Room room1 = new Room(new Cell(0, 0), new Grid(13, 5));
            Corridor corr1 = new Corridor(new Cell(4, 4), new Grid(3, 5), Corridor.Orientation.horizontal);
            Room room2 = new Room(new Cell(2, 8), new Grid(9, 5));
            Corridor corr2 = new Corridor(new Cell(1, 8), new Grid(2, 3), Corridor.Orientation.vertical);
            board.addRoom(room1);
            board.addCorridor(corr1);
            board.addRoom(room2);
            board.addCorridor(corr2);

            Assert.AreEqual(4, board.numberOfRoomsAndCorridors());
            board.removeLast();

            Assert.AreEqual(3, board.numberOfRoomsAndCorridors());
        }

        [Test]
        public void cropping_toZero_boardWithOneRoom() {
            Board board = new Board(new Grid(10, 10));
            Room room1 = new Room(new Cell(2, 2), new Grid(4, 4));
            board.addRoom(room1);

            Board cropped = board.crop();

            Assert.AreEqual(4, cropped.rows());
            Assert.AreEqual(4, cropped.cols());
            Assert.AreEqual(new Cell(0, 0), cropped.rooms()[0].topLeftVertex());
        }

        [Test]
        public void cropping__toZero_boardWithTwoRooms() {
            Board board = new Board(new Grid(20, 20));
            Room room1 = new Room(new Cell(0, 0), new Grid(13, 5));
            Corridor corr1 = new Corridor(new Cell(4, 4), new Grid(3, 5), Corridor.Orientation.horizontal);
            Room room2 = new Room(new Cell(2, 8), new Grid(9, 5));
            board.addRoom(room1);
            board.addCorridor(corr1);
            board.addRoom(room2);

            Board cropped = board.crop();

            Assert.AreEqual(13, cropped.rows());
            Assert.AreEqual(13, cropped.cols());
            Assert.AreEqual(new Cell(0, 0), cropped.rooms()[0].topLeftVertex());
            Assert.AreEqual(new Cell(4, 4), cropped.corridors()[0].topLeftVertex());
            Assert.AreEqual(new Cell(2, 8), cropped.rooms()[1].topLeftVertex());
        }

        [Test]
        public void cropping_toFour_boardWithOneRoom() {
            Board board = new Board(new Grid(10, 10));
            Room room1 = new Room(new Cell(2, 2), new Grid(4, 4));
            board.addRoom(room1);

            Board cropped = board.crop(4);

            Assert.AreEqual(12, cropped.rows());
            Assert.AreEqual(12, cropped.cols());
            Assert.AreEqual(new Cell(4, 4), cropped.rooms()[0].topLeftVertex());
        }
    }
   
}