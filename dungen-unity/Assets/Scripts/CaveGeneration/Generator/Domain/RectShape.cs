using System;
using System.Collections.Generic;
using DungeonGeneration.Generator.Domain;

public class RectShape : APolyShape {

    public RectShape(Cell topLeftVertex, OIGrid cells): 
        base(topLeftVertex, cells) {
    }

    public override bool isCellValid(int x, int y) {
        return x >= 0 && x <  grid().rows() && y >= 0 && y < grid().columns();
    }

}