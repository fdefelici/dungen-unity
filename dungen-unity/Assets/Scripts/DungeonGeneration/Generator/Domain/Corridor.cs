using System;
using DungeonGeneration.Generator.Domain;
using DungeonGeneration.Generator.Plotters;

public class Corridor : IShape {
    private Cell _topLeftVertex;
    private Cell _topRightVertex;
    private Cell _botLeftVertex;
    private Cell _botRightVertex;
    private Grid _grid;
    private Orientation _orientation;
    private Room _sourceRoom;
    private Room _destRoom;

    public enum Orientation {
        horizontal,
        vertical
    }

    public Corridor(Cell topLeftVertex, Grid size, Orientation orientation) {
        _topLeftVertex = topLeftVertex;
        _topRightVertex = size.absTopRightVertexUsing(_topLeftVertex);
        _botLeftVertex = size.absBotLeftVertexUsing(_topLeftVertex);
        _botRightVertex = size.absBotRightVertexUsing(_topLeftVertex);
        _grid = size;
        _orientation = orientation;
    }

    public bool isCellPerimetral(Cell pos) {
        if (pos.isWithin(topLeftVertex(), topRightVertex())) return true;
        if (pos.isWithin(topRightVertex(), bottomRightVertex())) return true;
        if (pos.isWithin(bottomLeftVertex(), bottomRightVertex())) return true;
        if (pos.isWithin(topLeftVertex(), bottomLeftVertex())) return true;
        return false;
    }

    public void plotOn(int[,] map, IDungeonBoardPlotter plotter) {
        if (hasDestRoom()) destRoom().plotOn(map, plotter);
        plotter.applyOnCorridor(this, map);
    }

    public Cell bottomRightVertex() {
        return _botRightVertex;
    }

    public Cell topRightVertex() {
        return _topRightVertex;
    }

    public Cell topLeftVertex() {
        return _topLeftVertex;
    }

    public void setDestinationRoom(Room room) {
        _destRoom = room;
    }

    public int width() {
        return _grid.columns();
    }

    public Cell bottomLeftVertex() {
        return _botLeftVertex;
    }

    public void setSourceRoom(Room room) {
        _sourceRoom = room;
    }

    public int height() {
        return _grid.rows();      
    }

    public bool isVertical() {
        return _orientation == Orientation.vertical;
    }
    public bool isOrizontal() {
        return _orientation == Orientation.horizontal;
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

    public bool isSharingBottomLeftVertexWithSourceRoom() {
        if (!hasSourceRoom()) return false;
        return sourceRoom().isSharingVertex(bottomLeftVertex());
    }

    public bool isSharingBottomRightVertexWithSourceRoom() {
        if (!hasSourceRoom()) return false;
        return sourceRoom().isSharingVertex(bottomRightVertex());
    }

    public bool isSharingTopLeftVertexWithSourceRoom() {
        if (!hasSourceRoom()) return false;
        return sourceRoom().isSharingVertex(topLeftVertex());
    }

    public bool isSharingTopRightVertexWithSourceRoom() {
        if (!hasSourceRoom()) return false;
        return sourceRoom().isSharingVertex(topRightVertex());
    }

    public bool isSharingBottomLeftVertexWithDestRoom() {
        if (!hasDestRoom()) return false;
        return destRoom().isSharingVertex(bottomLeftVertex());
    }

    public bool isSharingBottomRightVertexWithDestRoom() {
        if (!hasDestRoom()) return false;
        return destRoom().isSharingVertex(bottomRightVertex());
    }

    public bool isSharingTopLeftVertexWithDestRoom() {
        if (!hasDestRoom()) return false;
        return destRoom().isSharingVertex(topLeftVertex());
    }

    public bool isSharingTopRightVertexWithDestRoom() {
        if (!hasDestRoom()) return false;
        return destRoom().isSharingVertex(topRightVertex());
    }

    private bool hasSourceRoom() {
        return sourceRoom() != null;
    }

    private bool hasDestRoom() {
        return destRoom() != null;
    }

    private Room sourceRoom() {
        return _sourceRoom;
    }

    private Room destRoom() {
        return _destRoom;
    }

    public bool collidesWith(IShape each) {
        if (each.containsCell(_topLeftVertex)) return true;
        if (each.containsCell(_topRightVertex)) return true;
        if (each.containsCell(_botRightVertex)) return true;
        if (each.containsCell(_botLeftVertex)) return true;
        return false;
    }

    public bool containsCell(Cell aCell) {
        return aCell.isWithin(_topLeftVertex, _botRightVertex);
    }

    public override string ToString() {
        return "Corridor: " + topLeftVertex() + " " + _grid;
    }

    // Javascript API
    public Cell[] walkableCells(Boolean excludeCellNextToWall) {
        Cell innerA = null;
        Cell innerB = null;

        int cellExclusion = 1;
        if (excludeCellNextToWall) cellExclusion = 2;

        if (isOrizontal()) {
            innerA = topLeftVertex().plusCell(cellExclusion, 0);
            innerB = bottomRightVertex().minusCell(cellExclusion, 0);
        } else {
            innerA = topLeftVertex().plusCell(0, cellExclusion);
            innerB = bottomRightVertex().minusCell(0, cellExclusion);
        }

        if (innerA.isGreatherThan(innerB)) return new Cell[0];

        return innerA.cells(innerB);
    }

    public Grid grid() {
        return _grid;
    }
}