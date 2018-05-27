using NUnit.Framework;

using DungeonGeneration.Generator.Domain;
using CaveGeneration.Generator;

public class CaveBoardTest {

    [Test]
    public void linkingRoomAndCorridor_case1() {
        CaveBoard board = new CaveBoard(40, 40);

        IXShape room1 = new RectShape(new Cell(0, 0), new OIGrid(5, 5));
        IXShape room2 = new RectShape(new Cell(0, 0), new OIGrid(5, 5));
        IXShape corr12 = new FreeShape();
        board.addRoom(room1);
        board.addCorridor(corr12);
        board.addRoom(room2);

        Assert.IsNull(room1.getIncoming());
        Assert.AreEqual(room1.getOutcoming(), corr12);

        Assert.AreEqual(corr12.getIncoming(), room1);
        Assert.AreEqual(corr12.getOutcoming(), room2);

        Assert.AreEqual(room2.getIncoming(), corr12);
        Assert.IsNull(room2.getOutcoming());
    }

    [Test]
    public void linkingRoomAndCorridor_case2() {
        CaveBoard board = new CaveBoard(40, 40);

        IXShape room1 = new ElliShape(new Cell(0, 0), new OIGrid(5, 5));
        IXShape room2 = new ElliShape(new Cell(0, 0), new OIGrid(5, 5));
        IXShape corr12 = new FreeShape();
        board.addRoom(room1);
        board.addCorridor(corr12);
        board.addRoom(room2);

        Assert.IsNull(room1.getIncoming());
        Assert.AreEqual(room1.getOutcoming(), corr12);

        Assert.AreEqual(corr12.getIncoming(), room1);
        Assert.AreEqual(corr12.getOutcoming(), room2);

        Assert.AreEqual(room2.getIncoming(), corr12);
        Assert.IsNull(room2.getOutcoming());
    }

}