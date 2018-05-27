using System;
using System.Collections.Generic;
using DungeonGeneration.Generator.Domain;
using DungeonGeneration.Generator.Plotters;

public class Room : IShape {
    private Grid _grid;
    private Cell _topLeftVertex;
    private Cell _topRightVertex;
    private Cell _botLeftVertex;
    private Cell _botRightVertex;
    private Corridor _outcomingCorridor;
    private Corridor _incomingCorridor;

    public Room(Cell topLeftVertex, Grid size) { 
        _topLeftVertex = topLeftVertex;
        _topRightVertex = size.absTopRightVertexUsing(_topLeftVertex);
        _botLeftVertex = size.absBotLeftVertexUsing(_topLeftVertex);
        _botRightVertex = size.absBotRightVertexUsing(_topLeftVertex);
        _grid = size;
    }

    public void setCorridorIncoming(Corridor corr) {
        _incomingCorridor = corr;
    }

    public void setCorridorOutcoming(Corridor corr) {
        _outcomingCorridor = corr;
    }

    public Cell topRightVertex() {
        return _topRightVertex;
    }

    public Cell bottomLeftVertex() {
        return _botLeftVertex;
    }

    public Cell topLeftVertex() {
        return _topLeftVertex;
    }

    public Cell bottomRightVertex() {
        return _botRightVertex;
    }

    public override string ToString() {
        return "Room: " + topLeftVertex() + " " + _grid;
    }

    public void plotOn(int[,] map, IDungeonBoardPlotter plotter) {
        plotter.applyOnRoom(this, map);
        if (_outcomingCorridor != null) _outcomingCorridor.plotOn(map, plotter);
    }

    public bool hasCorridorSharingVertex(Cell vertex) {
        bool result = false;
        if (_incomingCorridor != null) {
            result = result || _incomingCorridor.isSharingVertex(vertex);
        }
        if (_outcomingCorridor != null) {
            result = result || _outcomingCorridor.isSharingVertex(vertex);
        }
        return result;
    }

    public int height() {
        return _grid.rows();
    }

    public int width() {
        return _grid.columns();
    }

    public bool isSharingVertex(Cell vertex) {
        if (vertex.isEqual(_topLeftVertex)) return true;
        if (vertex.isEqual(_topRightVertex)) return true;
        if (vertex.isEqual(_botLeftVertex)) return true;
        if (vertex.isEqual(_botRightVertex)) return true;
        return false;
    }

    public bool isWithin(Grid container) {
        return _grid.isWithin(container, _topLeftVertex);
    }

    public bool collidesWith(IShape other) {
        Cell[] cells = _topLeftVertex.cells(_botRightVertex);
        foreach(Cell each in cells) {
            if (other.containsCell(each)) return true;
        }      
        return false;
    }

    public bool containsCell(Cell aCell) {
        return aCell.isWithin(_topLeftVertex, _botRightVertex);
    }

    // Javascript API
    public Cell[] walkableCells(Boolean excludeCellNextToWall) {
        int cellExclusion = 1;
        if (excludeCellNextToWall) cellExclusion = 2;

        Cell innerTopLeft = topLeftVertex().plusCell(cellExclusion, cellExclusion);
        Cell innerBotRight = bottomRightVertex().minusCell(cellExclusion, cellExclusion);
        
        if (innerTopLeft.isGreatherThan(innerBotRight)) return new Cell[0];
        return innerTopLeft.cells(innerBotRight);
    }
    // Javascript API
    public bool hasCorridorAtEast() {
        return isEast(_incomingCorridor) || isEast(_outcomingCorridor);
    }
    // Javascript API
    public bool hasCorridorAtSouth() {
        return isSouth(_incomingCorridor) || isSouth(_outcomingCorridor);
    }
    // Javascript API
    public bool hasCorridorAtWest() {
        return isWest(_incomingCorridor) || isWest(_outcomingCorridor);
    }
    // Javascript API
    public bool hasCorridorAtNorth() {
        return isNorth(_incomingCorridor) || isNorth(_outcomingCorridor);
    }

    private bool isNorth(Corridor corr) {
        if (corr == null) return false;
        return corr.bottomLeftVertex().isWithin(topLeftVertex(), topRightVertex());
    }

    private bool isWest(Corridor corr) {
        if (corr == null) return false;
        return corr.topRightVertex().isWithin(topLeftVertex(), bottomLeftVertex());
    }

    private bool isSouth(Corridor corr) {
        if (corr == null) return false;
        return corr.topLeftVertex().isWithin(bottomLeftVertex(), bottomRightVertex());
    }

    private bool isEast(Corridor corr) {
        if (corr == null) return false;
        return corr.topLeftVertex().isWithin(topRightVertex(), bottomRightVertex());
    }

    public Cell[] cellsFacingOutcomingCorridor() {
        return cellFacingCorridor(_outcomingCorridor);
    }

    public Cell[] cellsFacingIncomingCorridor() {
        return cellFacingCorridor(_incomingCorridor);
    }

    private Cell[] cellFacingCorridor(Corridor corr) {
        if (corr == null) return new Cell[0];

        Cell vertex1 = null;
        Cell vertex2 = null;
        if (isEast(corr)) {
            vertex1 = corr.topLeftVertex().plusCell(1, -1);
            vertex2 = corr.bottomLeftVertex().minusCell(1, 1);
        } else if (isSouth(corr)) {
            vertex1 = corr.topLeftVertex().plusCell(-1, 1);
            vertex2 = corr.topRightVertex().minusCell(1, 1);
        } else if (isWest(corr)) {
            vertex1 = corr.topRightVertex().plusCell(1, 1);
            vertex2 = corr.bottomRightVertex().minusCell(1, -1);
        } else if (isNorth(corr)) {
            vertex1 = corr.bottomLeftVertex().plusCell(1, 1);
            vertex2 = corr.bottomRightVertex().plusCell(1, -1);
        }
        return vertex1.cells(vertex2);
    }

    public Grid grid() {
        return _grid;
    }
}
