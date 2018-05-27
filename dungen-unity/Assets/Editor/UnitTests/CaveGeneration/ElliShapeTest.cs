using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using DungeonGeneration.Generator.Domain;

public class ElliShapeTest {

    [Test]
    public void cellValidity_withinCircle() {
        ElliShape shape = new ElliShape(new Cell(0, 0), new OIGrid(10, 10));
        Assert.IsTrue(shape.isCellValid(5, 5));
        Assert.IsTrue(shape.isCellValid(5, 9));
        Assert.IsTrue(shape.isCellValid(0, 5));

        Assert.IsFalse(shape.isCellValid(5, 10));
        Assert.IsFalse(shape.isCellValid(9, 9));
        Assert.IsFalse(shape.isCellValid(10, 10));
    }

    [Test]
    public void cellValidity_withinEllipse() {
        ElliShape shape = new ElliShape(new Cell(0, 0), new OIGrid(3, 5));
        Assert.IsTrue(shape.isCellValid(1, 1));
        Assert.IsTrue(shape.isCellValid(1, 4));
    }

    [Test]
    public void deleteRegionsButTheBiggest_Ellibug() {
        ElliShape shape = new ElliShape(new Cell(0, 0), new OIGrid(10, 10));
        shape.setCellValue(5, 9, XTile.FLOOR);
        Assert.AreEqual(1, shape.regionsNumber());

        shape.deleteRegionsButTheBiggest();
        Assert.AreEqual(1, shape.regionsNumber());
    }

    [Test]
    public void walkableCell_leftVertexAt00() {
        // "-" excluded by ellipse
        //   0 1 2 3 4
        //
        //0  _ _ _ _ _
        //1  _ 1 1 1 1
        //2  _ 1 1 1 1
        //3  _ 1 1 1 1
        //4  _ 1 1 1 1

        ElliShape shape = new ElliShape(new Cell(0, 0), new OIGrid(5, 5));
        shape.setCellValue(0, 0, XTile.FLOOR);
        shape.setCellValue(1, 0, XTile.FLOOR);
        shape.setCellValue(2, 0, XTile.FLOOR);
        shape.setCellValue(3, 0, XTile.FLOOR);
        shape.setCellValue(4, 0, XTile.FLOOR);
        shape.setCellValue(0, 1, XTile.FLOOR);
        shape.setCellValue(1, 1, XTile.FLOOR);
        shape.setCellValue(2, 1, XTile.FLOOR);
        shape.setCellValue(3, 1, XTile.FLOOR);
        shape.setCellValue(4, 1, XTile.FLOOR);
        shape.setCellValue(0, 2, XTile.FLOOR);
        shape.setCellValue(1, 2, XTile.FLOOR);
        shape.setCellValue(2, 2, XTile.FLOOR);
        shape.setCellValue(3, 2, XTile.FLOOR);
        shape.setCellValue(4, 2, XTile.FLOOR);
        shape.setCellValue(0, 3, XTile.FLOOR);
        shape.setCellValue(1, 3, XTile.FLOOR);
        shape.setCellValue(2, 3, XTile.FLOOR);
        shape.setCellValue(3, 3, XTile.FLOOR);
        shape.setCellValue(4, 3, XTile.FLOOR);
        shape.setCellValue(0, 4, XTile.FLOOR);
        shape.setCellValue(1, 4, XTile.FLOOR);
        shape.setCellValue(2, 4, XTile.FLOOR);
        shape.setCellValue(3, 4, XTile.FLOOR);
        shape.setCellValue(4, 4, XTile.FLOOR);

        Cell[] result = shape.walkableCells(false);
        Assert.AreEqual(16, result.Length);
        
    }

    [Test]
    public void walkableCell_ExcludingCellNextToWall_leftVertexAt00() {
        // "-" excluded by ellipse
        // "." excluded by flag = true (excludingCellNextToWall)
        
        //   0 1 2 3 4
        //
        //0  _ _ _ _ _
        //1  _ . . . .
        //2  _ . 1 1 .
        //3  _ . 1 1 .
        //4  _ . . . .

        ElliShape shape = new ElliShape(new Cell(0, 0), new OIGrid(5, 5));
        shape.setCellValue(0, 0, XTile.FLOOR);
        shape.setCellValue(1, 0, XTile.FLOOR);
        shape.setCellValue(2, 0, XTile.FLOOR);
        shape.setCellValue(3, 0, XTile.FLOOR);
        shape.setCellValue(4, 0, XTile.FLOOR);
        shape.setCellValue(0, 1, XTile.FLOOR);
        shape.setCellValue(1, 1, XTile.FLOOR);
        shape.setCellValue(2, 1, XTile.FLOOR);
        shape.setCellValue(3, 1, XTile.FLOOR);
        shape.setCellValue(4, 1, XTile.FLOOR);
        shape.setCellValue(0, 2, XTile.FLOOR);
        shape.setCellValue(1, 2, XTile.FLOOR);
        shape.setCellValue(2, 2, XTile.FLOOR);
        shape.setCellValue(3, 2, XTile.FLOOR);
        shape.setCellValue(4, 2, XTile.FLOOR);
        shape.setCellValue(0, 3, XTile.FLOOR);
        shape.setCellValue(1, 3, XTile.FLOOR);
        shape.setCellValue(2, 3, XTile.FLOOR);
        shape.setCellValue(3, 3, XTile.FLOOR);
        shape.setCellValue(4, 3, XTile.FLOOR);
        shape.setCellValue(0, 4, XTile.FLOOR);
        shape.setCellValue(1, 4, XTile.FLOOR);
        shape.setCellValue(2, 4, XTile.FLOOR);
        shape.setCellValue(3, 4, XTile.FLOOR);
        shape.setCellValue(4, 4, XTile.FLOOR);

        Cell[] result = shape.walkableCells(true);
        Assert.AreEqual(4, result.Length);
        Assert.AreEqual(new Cell(2, 2), result[0]);
        Assert.AreEqual(new Cell(2, 3), result[1]);
        Assert.AreEqual(new Cell(3, 2), result[2]);
        Assert.AreEqual(new Cell(3, 3), result[3]);
    }
}
