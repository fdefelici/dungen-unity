using System;
using System.Collections.Generic;
using DungeonGeneration.Generator.Domain;

public interface IXShape {
    Cell topLeftVertex();
    Cell topRightVertex();
    Cell bottomLeftVertex();
    Cell bottomRightVertex();
    OIGrid grid();
    void setCellValue(int cellX, int cellY, int v);

    void forEachCell(Action<int, int, IXShape> p);
    void forEachCell2(Action<int, int, int> p);
    void forEachCellAbs(Action<int, int, int> doFunct);
    bool isCellValid(int neighbourX, int neighbourY);
    bool hasCellValue(int neighbourX, int neighbourY, int v);
    bool hasCellAbsValue(Cell absCell, int v);
    
    bool isWithin(OIGrid grid);
    void accept(IShapeVisitor visitor);
    List<Cell> edge();
    CellPair shortestCellPair(IXShape other);
    bool collidesWith(IXShape each);
    bool containsCell(Cell each);
    void forEachEdgeCellAbs(Action<int, int, int> doFunct);


    void setIncoming(IXShape aRoom);
    void setOutcoming(IXShape aRoom);
    IXShape getIncoming();
    IXShape getOutcoming();


    // Javascript API
    Cell[] walkableCells(Boolean excludeCellNextToWall);
    Cell absCellFacing(Cell cell);
    bool hasAbsCellFacing(Cell cell);
}
