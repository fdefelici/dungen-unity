using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using DungeonGeneration.Generator.Domain;

public class RectShapeTest {

    [Test]
    public void countingFloorRegions() {
        RectShape shape = new RectShape(new Cell(0, 0), new OIGrid(10, 10));
        Assert.AreEqual(0, shape.regionsNumber());

        shape.setCellValue(1, 1, XTile.FLOOR);
        Assert.AreEqual(1, shape.regionsNumber());

        shape.setCellValue(3, 1, XTile.FLOOR);
        shape.setCellValue(3, 2, XTile.FLOOR);
        shape.setCellValue(3, 3, XTile.FLOOR);
        Assert.AreEqual(2, shape.regionsNumber());
    }

    [Test]
    public void deleteRegionsButTheBiggest_withOneRegion() {
        RectShape shape = new RectShape(new Cell(0, 0), new OIGrid(10, 10));
        //REG 1
        shape.setCellValue(1, 1, XTile.FLOOR);
        Assert.AreEqual(1, shape.regionsNumber());

        shape.deleteRegionsButTheBiggest();
        Assert.AreEqual(1, shape.regionsNumber());

        Assert.IsTrue(shape.hasCellValue(1, 1, XTile.FLOOR));
    }

    [Test]
    public void deleteRegionsButTheBiggest_withTwoRegions() {
        RectShape shape = new RectShape(new Cell(0, 0), new OIGrid(10, 10));
        //REG 1
        shape.setCellValue(1, 1, XTile.FLOOR);
        //REG 2s
        shape.setCellValue(3, 1, XTile.FLOOR);
        shape.setCellValue(3, 2, XTile.FLOOR);
        shape.setCellValue(3, 3, XTile.FLOOR);
        Assert.AreEqual(2, shape.regionsNumber());

        shape.deleteRegionsButTheBiggest();
        Assert.AreEqual(1, shape.regionsNumber());

        Assert.IsTrue(shape.hasCellValue(1, 1, XTile.WALL));
        Assert.IsTrue(shape.hasCellValue(3, 1, XTile.FLOOR));
        Assert.IsTrue(shape.hasCellValue(3, 2, XTile.FLOOR));
        Assert.IsTrue(shape.hasCellValue(3, 3, XTile.FLOOR));
    }

    [Test]
    public void deleteRegionsButTheBiggest_withSixRegionsRoom() {
        RectShape shape = new RectShape(new Cell(0, 0), new OIGrid(40, 40));
        ShapeCellularAutomaton filler = new ShapeCellularAutomaton(48, 50, 5);
        filler.applyOn(shape);

        Assert.AreEqual(6, shape.regionsNumber());
        shape.deleteRegionsButTheBiggest();
        Assert.AreEqual(1, shape.regionsNumber());
    }

    [Test]
    public void deleteRegionsButTheBiggest_bug() {
        RectShape shape = new RectShape(new Cell(0, 0), new OIGrid(10, 10));
        ShapeCellularAutomaton auto = new ShapeCellularAutomaton(1683686970, 58, 5);

        auto.applyOn(shape);
        Assert.AreEqual(1, shape.regionsNumber());

        shape.deleteRegionsButTheBiggest();
        Assert.AreEqual(1, shape.regionsNumber());
    }

    [Test]
    public void walkableCell_leftVertexAt00() {
        RectShape shape = new RectShape(new Cell(0, 0), new OIGrid(10, 10));
        shape.setCellValue(3, 1, XTile.FLOOR);
        shape.setCellValue(3, 2, XTile.FLOOR);
        shape.setCellValue(3, 3, XTile.FLOOR);

        Cell[] result = shape.walkableCells(false);
        Assert.AreEqual(3, result.Length);
        Assert.AreEqual(new Cell(3, 1), result[0]);
        Assert.AreEqual(new Cell(3, 2), result[1]);
        Assert.AreEqual(new Cell(3, 3), result[2]);
    }

    [Test]
    public void walkableCell_leftVertexAt11() {
        RectShape shape = new RectShape(new Cell(1, 1), new OIGrid(10, 10));
        shape.setCellValue(3, 1, XTile.FLOOR);
        shape.setCellValue(3, 2, XTile.FLOOR);
        shape.setCellValue(3, 3, XTile.FLOOR);

        Cell[] result = shape.walkableCells(false);
        Assert.AreEqual(3, result.Length);
        Assert.AreEqual(new Cell(4, 2), result[0]);
        Assert.AreEqual(new Cell(4, 3), result[1]);
        Assert.AreEqual(new Cell(4, 4), result[2]);
    }

    [Test]
    public void walkableCell_ExcludingCellNextToWall_leftVertexAt00() {
        RectShape shape = new RectShape(new Cell(0, 0), new OIGrid(5, 5));
        shape.setCellValue(1, 1, XTile.FLOOR);
        shape.setCellValue(1, 2, XTile.FLOOR);
        shape.setCellValue(1, 3, XTile.FLOOR);
        shape.setCellValue(2, 1, XTile.FLOOR);
        shape.setCellValue(2, 2, XTile.FLOOR);
        shape.setCellValue(2, 3, XTile.FLOOR);
        shape.setCellValue(3, 1, XTile.FLOOR);
        shape.setCellValue(3, 2, XTile.FLOOR);
        shape.setCellValue(3, 3, XTile.FLOOR);

        Cell[] result = shape.walkableCells(true);
        Assert.AreEqual(1, result.Length);
        Assert.AreEqual(new Cell(2, 2), result[0]);
    }

    [Test]
    public void walkableCell_ExcludingCellNextToWall_leftVertexAt11() {
        RectShape shape = new RectShape(new Cell(1, 1), new OIGrid(5, 5));
        shape.setCellValue(1, 1, XTile.FLOOR);
        shape.setCellValue(1, 2, XTile.FLOOR);
        shape.setCellValue(1, 3, XTile.FLOOR);
        shape.setCellValue(2, 1, XTile.FLOOR);
        shape.setCellValue(2, 2, XTile.FLOOR);
        shape.setCellValue(2, 3, XTile.FLOOR);
        shape.setCellValue(3, 1, XTile.FLOOR);
        shape.setCellValue(3, 2, XTile.FLOOR);
        shape.setCellValue(3, 3, XTile.FLOOR);

        Cell[] result = shape.walkableCells(true);
        Assert.AreEqual(1, result.Length);
        Assert.AreEqual(new Cell(3, 3), result[0]);
    }

    [Test]
    public void hasCellAbsValue() {
        RectShape shape = new RectShape(new Cell(2, 2), new OIGrid(5, 5));
        shape.setCellValue(1, 1, XTile.FLOOR);
        
        Assert.IsFalse(shape.hasCellAbsValue(new Cell(1, 1), XTile.FLOOR));
        Assert.IsTrue(shape.hasCellAbsValue(new Cell(3, 3), XTile.FLOOR));
    }

    [Test]
    public void absCellsFacingOutcoming_case1() {
        RectShape shape = new RectShape(new Cell(0, 0), new OIGrid(5, 5));
        shape.setCellValue(0, 0, XTile.FLOOR);
        shape.setCellValue(0, 1, XTile.FLOOR);
        shape.setCellValue(0, 2, XTile.FLOOR);
        shape.setCellValue(1, 0, XTile.FLOOR);
        shape.setCellValue(1, 1, XTile.FLOOR);
        shape.setCellValue(1, 2, XTile.FLOOR);
        shape.setCellValue(2, 0, XTile.FLOOR);
        shape.setCellValue(2, 1, XTile.FLOOR);
        shape.setCellValue(2, 2, XTile.FLOOR);

        FreeShape outShape = new FreeShape();
        outShape.setCellValue(0, 3, XTile.FLOOR);
        outShape.setCellValue(0, 4, XTile.FLOOR);
        outShape.setCellValue(0, 5, XTile.FLOOR);
        outShape.setCellValue(1, 3, XTile.FLOOR);
        outShape.setCellValue(1, 4, XTile.FLOOR);
        outShape.setCellValue(1, 5, XTile.FLOOR);

        shape.setOutcoming(outShape);

        Cell[] result = shape.absCellsFacingOutcoming();
        Assert.AreEqual(2, result.Length);
        Assert.AreEqual(new Cell(0, 2), result[0]);
        Assert.AreEqual(new Cell(1, 2), result[1]);
    }

    [Test]
    public void absCellsFacingIncoming_case1() {
        RectShape shape = new RectShape(new Cell(0, 0), new OIGrid(5, 5));
        shape.setCellValue(0, 0, XTile.FLOOR);
        shape.setCellValue(0, 1, XTile.FLOOR);
        shape.setCellValue(0, 2, XTile.FLOOR);
        shape.setCellValue(1, 0, XTile.FLOOR);
        shape.setCellValue(1, 1, XTile.FLOOR);
        shape.setCellValue(1, 2, XTile.FLOOR);
        shape.setCellValue(2, 0, XTile.FLOOR);
        shape.setCellValue(2, 1, XTile.FLOOR);
        shape.setCellValue(2, 2, XTile.FLOOR);

        FreeShape inShape = new FreeShape();
        inShape.setCellValue(0, 3, XTile.FLOOR);
        inShape.setCellValue(0, 4, XTile.FLOOR);
        inShape.setCellValue(0, 5, XTile.FLOOR);
        inShape.setCellValue(1, 3, XTile.FLOOR);
        inShape.setCellValue(1, 4, XTile.FLOOR);
        inShape.setCellValue(1, 5, XTile.FLOOR);

        shape.setIncoming(inShape);

        Cell[] result = shape.absCellsFacingIncoming();
        Assert.AreEqual(2, result.Length);
        Assert.AreEqual(new Cell(0, 2), result[0]);
        Assert.AreEqual(new Cell(1, 2), result[1]);
    }

    [Test]
    public void absCellsFacingIncoming_case2() {
        RectShape shape = new RectShape(new Cell(0, 5), new OIGrid(5, 5));
        shape.setCellValue(0, 0, XTile.FLOOR);
        shape.setCellValue(0, 1, XTile.FLOOR);
        shape.setCellValue(0, 2, XTile.FLOOR);
        shape.setCellValue(1, 0, XTile.FLOOR);
        shape.setCellValue(1, 1, XTile.FLOOR);
        shape.setCellValue(1, 2, XTile.FLOOR);
        shape.setCellValue(2, 0, XTile.FLOOR);
        shape.setCellValue(2, 1, XTile.FLOOR);
        shape.setCellValue(2, 2, XTile.FLOOR);

        FreeShape inShape = new FreeShape();
        inShape.setCellValue(0, 2, XTile.FLOOR);
        inShape.setCellValue(1, 2, XTile.FLOOR);
        inShape.setCellValue(0, 3, XTile.FLOOR);
        inShape.setCellValue(1, 3, XTile.FLOOR);
        inShape.setCellValue(0, 4, XTile.FLOOR);
        inShape.setCellValue(1, 4, XTile.FLOOR);
        
        shape.setIncoming(inShape);

        Cell[] result = shape.absCellsFacingIncoming();
        Assert.AreEqual(2, result.Length);
        Assert.AreEqual(new Cell(0, 5), result[0]);
        Assert.AreEqual(new Cell(1, 5), result[1]);
    }

    [Test]
    public void absCellsFacingIncoming_case3() {
        //RectShape roomA = new RectShape(new Cell(0, 0), new OIGrid(8, 8));
        //RectShape roomB = new RectShape(new Cell(10, 10), new OIGrid(8, 8));
        //XTestUtils.printForTest(25, roomA, roomB);

        RectShape room1 = new RectShape(new Cell(0, 0), new OIGrid(8, 8));
        room1.setCellValue(0, 3, XTile.FLOOR);
        room1.setCellValue(0, 4, XTile.FLOOR);
        room1.setCellValue(0, 5, XTile.FLOOR);
        room1.setCellValue(0, 6, XTile.FLOOR);
        room1.setCellValue(1, 2, XTile.FLOOR);
        room1.setCellValue(1, 3, XTile.FLOOR);
        room1.setCellValue(1, 4, XTile.FLOOR);
        room1.setCellValue(1, 5, XTile.FLOOR);
        room1.setCellValue(1, 6, XTile.FLOOR);
        room1.setCellValue(1, 7, XTile.FLOOR);
        room1.setCellValue(2, 1, XTile.FLOOR);
        room1.setCellValue(2, 2, XTile.FLOOR);
        room1.setCellValue(2, 3, XTile.FLOOR);
        room1.setCellValue(2, 4, XTile.FLOOR);
        room1.setCellValue(2, 5, XTile.FLOOR);
        room1.setCellValue(2, 6, XTile.FLOOR);
        room1.setCellValue(2, 7, XTile.FLOOR);
        room1.setCellValue(3, 0, XTile.FLOOR);
        room1.setCellValue(3, 1, XTile.FLOOR);
        room1.setCellValue(3, 2, XTile.FLOOR);
        room1.setCellValue(3, 3, XTile.FLOOR);
        room1.setCellValue(3, 4, XTile.FLOOR);
        room1.setCellValue(3, 5, XTile.FLOOR);
        room1.setCellValue(3, 6, XTile.FLOOR);
        room1.setCellValue(3, 7, XTile.FLOOR);
        room1.setCellValue(4, 0, XTile.FLOOR);
        room1.setCellValue(4, 1, XTile.FLOOR);
        room1.setCellValue(4, 2, XTile.FLOOR);
        room1.setCellValue(4, 3, XTile.FLOOR);
        room1.setCellValue(4, 4, XTile.FLOOR);
        room1.setCellValue(4, 5, XTile.FLOOR);
        room1.setCellValue(4, 6, XTile.FLOOR);
        room1.setCellValue(4, 7, XTile.FLOOR);
        room1.setCellValue(5, 0, XTile.FLOOR);
        room1.setCellValue(5, 1, XTile.FLOOR);
        room1.setCellValue(5, 2, XTile.FLOOR);
        room1.setCellValue(5, 3, XTile.FLOOR);
        room1.setCellValue(5, 4, XTile.FLOOR);
        room1.setCellValue(5, 5, XTile.FLOOR);
        room1.setCellValue(5, 6, XTile.FLOOR);
        room1.setCellValue(5, 7, XTile.FLOOR);
        room1.setCellValue(6, 0, XTile.FLOOR);
        room1.setCellValue(6, 1, XTile.FLOOR);
        room1.setCellValue(6, 2, XTile.FLOOR);
        room1.setCellValue(6, 3, XTile.FLOOR);
        room1.setCellValue(6, 4, XTile.FLOOR);
        room1.setCellValue(6, 5, XTile.FLOOR);
        room1.setCellValue(6, 6, XTile.FLOOR);
        room1.setCellValue(7, 1, XTile.FLOOR);
        room1.setCellValue(7, 2, XTile.FLOOR);
        room1.setCellValue(7, 3, XTile.FLOOR);
        room1.setCellValue(7, 4, XTile.FLOOR);
        room1.setCellValue(7, 5, XTile.FLOOR);
        FreeShape corr12 = new FreeShape();
        corr12.setCellValue(4, 8, XTile.FLOOR);
        corr12.setCellValue(5, 8, XTile.FLOOR);
        corr12.setCellValue(5, 9, XTile.FLOOR);
        corr12.setCellValue(6, 7, XTile.FLOOR);
        corr12.setCellValue(6, 8, XTile.FLOOR);
        corr12.setCellValue(7, 7, XTile.FLOOR);
        corr12.setCellValue(6, 9, XTile.FLOOR);
        corr12.setCellValue(6, 10, XTile.FLOOR);
        corr12.setCellValue(7, 8, XTile.FLOOR);
        corr12.setCellValue(7, 9, XTile.FLOOR);
        corr12.setCellValue(8, 8, XTile.FLOOR);
        corr12.setCellValue(7, 10, XTile.FLOOR);
        corr12.setCellValue(7, 11, XTile.FLOOR);
        corr12.setCellValue(8, 9, XTile.FLOOR);
        corr12.setCellValue(8, 10, XTile.FLOOR);
        corr12.setCellValue(9, 9, XTile.FLOOR);
        corr12.setCellValue(8, 11, XTile.FLOOR);
        corr12.setCellValue(8, 12, XTile.FLOOR);
        corr12.setCellValue(9, 10, XTile.FLOOR);
        corr12.setCellValue(9, 11, XTile.FLOOR);
        corr12.setCellValue(10, 10, XTile.FLOOR);
        corr12.setCellValue(9, 12, XTile.FLOOR);
        corr12.setCellValue(9, 13, XTile.FLOOR);
        corr12.setCellValue(10, 11, XTile.FLOOR);
        corr12.setCellValue(10, 12, XTile.FLOOR);
        corr12.setCellValue(11, 11, XTile.FLOOR);
        corr12.setCellValue(10, 13, XTile.FLOOR);
        corr12.setCellValue(11, 12, XTile.FLOOR);
        corr12.setCellValue(12, 12, XTile.FLOOR);
        corr12.setIncoming(room1);
        room1.setOutcoming(corr12);
        RectShape room2 = new RectShape(new Cell(10, 10), new OIGrid(8, 8));
        room2.setCellValue(0, 4, XTile.FLOOR);
        room2.setCellValue(0, 5, XTile.FLOOR);
        room2.setCellValue(1, 3, XTile.FLOOR);
        room2.setCellValue(1, 4, XTile.FLOOR);
        room2.setCellValue(1, 5, XTile.FLOOR);
        room2.setCellValue(1, 6, XTile.FLOOR);
        room2.setCellValue(2, 3, XTile.FLOOR);
        room2.setCellValue(2, 4, XTile.FLOOR);
        room2.setCellValue(2, 5, XTile.FLOOR);
        room2.setCellValue(2, 6, XTile.FLOOR);
        room2.setCellValue(3, 3, XTile.FLOOR);
        room2.setCellValue(3, 4, XTile.FLOOR);
        room2.setCellValue(3, 5, XTile.FLOOR);
        room2.setCellValue(3, 6, XTile.FLOOR);
        room2.setCellValue(3, 7, XTile.FLOOR);
        room2.setCellValue(4, 3, XTile.FLOOR);
        room2.setCellValue(4, 4, XTile.FLOOR);
        room2.setCellValue(4, 5, XTile.FLOOR);
        room2.setCellValue(4, 6, XTile.FLOOR);
        room2.setCellValue(4, 7, XTile.FLOOR);
        room2.setCellValue(5, 3, XTile.FLOOR);
        room2.setCellValue(5, 4, XTile.FLOOR);
        room2.setCellValue(5, 5, XTile.FLOOR);
        room2.setCellValue(5, 6, XTile.FLOOR);
        room2.setCellValue(5, 7, XTile.FLOOR);
        room2.setCellValue(6, 3, XTile.FLOOR);
        room2.setCellValue(6, 4, XTile.FLOOR);
        room2.setCellValue(6, 5, XTile.FLOOR);
        room2.setCellValue(6, 6, XTile.FLOOR);
        room2.setCellValue(6, 7, XTile.FLOOR);
        room2.setCellValue(7, 4, XTile.FLOOR);
        room2.setCellValue(7, 5, XTile.FLOOR);
        room2.setCellValue(7, 6, XTile.FLOOR);
        room2.setIncoming(corr12);
        corr12.setOutcoming(room2);


        Cell[] incomCells;
        Cell[] outcomCells;

        incomCells = room1.absCellsFacingIncoming();
        Assert.AreEqual(0, incomCells.Length);
        outcomCells = room1.absCellsFacingOutcoming();
        Assert.AreEqual(3, outcomCells.Length);
        Assert.AreEqual(new Cell(4, 7), outcomCells[0]);
        Assert.AreEqual(new Cell(5, 7), outcomCells[1]);
        Assert.AreEqual(new Cell(6, 6), outcomCells[2]);

        incomCells = room2.absCellsFacingIncoming();
        Assert.AreEqual(3, incomCells.Length);
        Assert.AreEqual(new Cell(10, 14), incomCells[0]);
        Assert.AreEqual(new Cell(11, 13), incomCells[1]);
        Assert.AreEqual(new Cell(12, 13), incomCells[2]);
        outcomCells = room2.absCellsFacingOutcoming();
        Assert.AreEqual(0, outcomCells.Length);
        
    }
    
}
