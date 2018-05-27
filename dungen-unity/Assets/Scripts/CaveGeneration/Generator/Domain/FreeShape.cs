using System;
using System.Collections.Generic;
using DungeonGeneration.Generator.Domain;

public class FreeShape : IXShape {
    private List<Cell> _cells;
    private IXShape _incoming;
    private IXShape _outcoming;

    public FreeShape() {
        _cells = new List<Cell>();
    }

    public void add(List<Cell> vCells) {
        foreach(Cell each in vCells) {
            add(each);
        }
    }

    public void add(Cell each) {
        if (_cells.Contains(each)) return;
        _cells.Add(each);
    }

    public void accept(IShapeVisitor visitor) {
        visitor.visit(this);
    }


    public Cell bottomLeftVertex() {
        throw new NotImplementedException();
    }

    public List<Cell> edge() {
        List<Cell> result = new List<Cell>();
        forEachCell((x, y, shape) => {
            if (shape.hasCellValue(x, y, XTile.FLOOR)) {
                for (int neighbourX = x - 1; neighbourX <= x + 1; neighbourX++) {
                    for (int neighbourY = y - 1; neighbourY <= y + 1; neighbourY++) {
                        if (shape.isCellValid(neighbourX, neighbourY)) {
                            if (neighbourX != x || neighbourY != y) {
                                if (shape.hasCellValue(neighbourX, neighbourY, XTile.WALL)) {
                                    Cell cell = new Cell(x, y);
                                    if (!result.Contains(cell)) result.Add(cell);
                                }
                            }
                        } else {
                            Cell cell = new Cell(x, y);
                            if (!result.Contains(cell)) result.Add(cell);
                        }
                    }
                }
            }
        });
        return result;
    }

    public void forEachCell(Action<int, int, IXShape> doFunct) {
        foreach(Cell each in _cells) {
            doFunct(each.row(), each.col(), this);
        }
    }

    public void forEachCellAbs(Action<int, int, int> doFunct) {
        foreach (Cell each in _cells) {
            doFunct(each.row(), each.col(), XTile.FLOOR);
        }
    }

    public bool hasCellValue(int row, int col, int v) {
        if (!isCellValid(row, col)) return false;
        if (v == XTile.FLOOR) return true;
        else return false;
    }

    public bool isCellValid(int row, int col) {
        foreach(Cell each in _cells) {
            if (each.row() == row && each.col() == col) return true;
        }
        return false;
    }

    public OIGrid grid() {
        throw new NotImplementedException();
    }

    public void setCellValue(int cellX, int cellY, int v) {
        _cells.Add(new Cell(cellX, cellY));
    }

    public CellPair shortestCellPair(IXShape other) {
        throw new NotImplementedException();
    }

    public Cell topLeftVertex() {
        throw new NotImplementedException();
    }

    public Cell topRightVertex() {
        throw new NotImplementedException();
    }

    public bool isWithin(OIGrid grid) {
        throw new NotImplementedException();
        //TODO: COME FACCIO>
    }

    public bool collidesWith(IXShape each) {
        throw new NotImplementedException();
    }

    public bool containsCell(Cell each) {
        throw new NotImplementedException();
    }

    public void forEachEdgeCellAbs(Action<int, int, int> doFunct) {
        List<Cell> edge = this.edge();
        foreach (Cell each in edge) {
            //Nel FreeShape le cell sono gia a valore assoluto.
            //Cell abs = each.plus(topLeftVertex());
            doFunct(each.row(), each.col(), XTile.FLOOR);
        }
    }

    public Cell[] walkableCells(Boolean excludeCellNextToWall) {
        if (!excludeCellNextToWall) return _cells.ToArray();

        List<Cell> edgeCells = edge();
        List<Cell> result = new List<Cell>();
        foreach (Cell each in _cells) {
            if (!edgeCells.Contains(each)) result.Add(each);
        }
        return result.ToArray();
    }

    public void setIncoming(IXShape incoming) {
        _incoming = incoming;
    }

    public void setOutcoming(IXShape outcoming) {
        _outcoming = outcoming;
    }

    public Cell bottomRightVertex() {
        throw new NotImplementedException();
    }

    public bool hasCellAbsValue(Cell absCell, int v) {
        return hasCellValue(absCell.row(), absCell.col(), v);
    }

    public Cell absCellFacing(Cell aCell) {
        List<Cell> cellsOnEdge = edge();

        if (cellsOnEdge.Contains(aCell.plusCell(1, 0))) {
            return aCell.plusCell(1, 0);
        }
        if (cellsOnEdge.Contains(aCell.plusCell(0, 1))) {
            return aCell.plusCell(0, 1);
        }
        if (cellsOnEdge.Contains(aCell.minusCell(1, 0))) {
            return aCell.minusCell(1, 0);
        }
        if (cellsOnEdge.Contains(aCell.minusCell(0, 1))) {
            return aCell.minusCell(0, 1);
        }
        return null;
    }

    public bool hasAbsCellFacing(Cell cell) {
        return absCellFacing(cell) != null;
    }

   
    public IXShape getIncoming() {
        return _incoming;
    }

    public IXShape getOutcoming() {
        return _outcoming;
    }

    public void forEachCell2(Action<int, int, int> doFunct) {
        foreach (Cell each in _cells) {
            doFunct(each.row(), each.col(), XTile.FLOOR);
        }
    }
}