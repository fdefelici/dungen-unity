using NUnit.Framework;

namespace DungeonGeneration.Generator.Domain {

    /* Test per API Javascript
     * used by Spawning Algorithm written in Javascript
     *  */
    public class BoardJsTest {        
        [Test]
        public void sizing_boardWithOneRoom() {
            Board board = new Board(new Grid(10, 10));
            board.addRoom(new Room(new Cell(0, 0), new Grid(2, 2)));
            Assert.AreEqual(1, board.rooms().Length);
            Assert.AreEqual(0, board.corridors().Length);
        }
        [Test]
        public void sizing_boardWithOneCorridor() {
            Board board = new Board(new Grid(10, 10));
            board.addCorridor(new Corridor(new Cell(0, 0), new Grid(2, 2), Corridor.Orientation.horizontal));
            Assert.AreEqual(0, board.rooms().Length);
            Assert.AreEqual(1, board.corridors().Length);
        }

        [Test]
        public void sizing_boardWithTwoRoomsAndOneCorridor() {
            Board board = new Board(new Grid(10, 10));
            board.addRoom(new Room(new Cell(0, 0), new Grid(2, 2)));
            board.addCorridor(new Corridor(new Cell(0, 0), new Grid(2, 2), Corridor.Orientation.horizontal));
            board.addRoom(new Room(new Cell(0, 0), new Grid(2, 2)));
            Assert.AreEqual(2, board.rooms().Length);
            Assert.AreEqual(1, board.corridors().Length);
        }
        
    }

}