using NUnit.Framework;
using DungeonGeneration.Generator.Domain;

public class ZeroOneCavePlotterTest {

    [Test]
    public void plotting_boardWithOneRectShape() {
        CaveBoard board = new CaveBoard(6, 6);
        IXShape room = new RectShape(new Cell(2, 2), new OIGrid(2, 2));
            room.setCellValue(0, 0, XTile.FLOOR);
            room.setCellValue(0, 1, XTile.FLOOR);
            room.setCellValue(1, 0, XTile.FLOOR);
            room.setCellValue(1, 1, XTile.FLOOR);
        board.addRoom(room);


        int[,] expected = new int[6, 6] { { 0, 0, 0, 0, 0, 0},
                                          { 0, 0, 1, 1, 0, 0},
                                          { 0, 1, 0, 0, 1, 0},
                                          { 0, 1, 0, 0, 1, 0},
                                          { 0, 0, 1, 1, 0, 0},
                                          { 0, 0, 0, 0, 0, 0} };

        ZeroOneCavePlotter plotter = new ZeroOneCavePlotter();
        plotter.applyOn(board);

        Assert.IsTrue(XTestUtils.areEquals(expected, plotter.result()));        
    }
}
