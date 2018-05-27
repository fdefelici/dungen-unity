using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using DungeonGeneration.Generator.Domain;

public class FreeShapeTest {

    [Test]
    public void edge_corr3x3() {
        //  F F F 
        //  F F F
        //  F F F
        FreeShape shape = new FreeShape();
        shape.add(new Cell(0, 0));
        shape.add(new Cell(0, 1));
        shape.add(new Cell(0, 2));
        shape.add(new Cell(1, 0));
        shape.add(new Cell(1, 1));
        shape.add(new Cell(1, 2));
        shape.add(new Cell(2, 0));
        shape.add(new Cell(2, 1));
        shape.add(new Cell(2, 2));

        List<Cell> result = shape.edge();
        Assert.AreEqual(8, result.Count);
        Assert.IsFalse(result.Contains(new Cell(1, 1)));
        
    }

    [Test]
    public void walkableCell_corr3x3() {
        //  F F F 
        //  F F F
        //  F F F
        FreeShape shape = new FreeShape();
        shape.add(new Cell(0, 0));
        shape.add(new Cell(0, 1));
        shape.add(new Cell(0, 2));
        shape.add(new Cell(1, 0));
        shape.add(new Cell(1, 1));
        shape.add(new Cell(1, 2));
        shape.add(new Cell(2, 0));
        shape.add(new Cell(2, 1));
        shape.add(new Cell(2, 2));

        Cell[] result = shape.walkableCells(false);
        Assert.AreEqual(9, result.Length);
    }

    [Test]
    public void walkableCell_ExcludingCellNextToWall_corr3x3() {
        FreeShape shape = new FreeShape();
        shape.add(new Cell(0, 0));
        shape.add(new Cell(0, 1));
        shape.add(new Cell(0, 2));
        shape.add(new Cell(1, 0));
        shape.add(new Cell(1, 1));
        shape.add(new Cell(1, 2));
        shape.add(new Cell(2, 0));
        shape.add(new Cell(2, 1));
        shape.add(new Cell(2, 2));

        Cell[] result = shape.walkableCells(true);
        Assert.AreEqual(1, result.Length);
        Assert.AreEqual(new Cell(1, 1), result[0]);
    }   
}
