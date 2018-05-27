using System.Collections.Generic;
using DungeonGeneration.Generator.Plotters;
using System;

public class CaveBoard {
    private OIGrid _grid;
    private List<IXShape> _rooms;
    private List<IXShape> _corrs;
    private List<IXShape> _roomsAndCorrs;

    public CaveBoard(int rows, int columns)
            : this(new OIGrid(rows, columns)) {
    }

    public CaveBoard(OIGrid mapGrid) {
        _grid = mapGrid;
        _rooms = new List<IXShape>();
        _corrs = new List<IXShape>();
        _roomsAndCorrs = new List<IXShape>();
    }

    public void addRoom(IXShape aRoom) {
        if (_roomsAndCorrs.Count != 0) {
            IXShape prevCorr = _roomsAndCorrs[_roomsAndCorrs.Count - 1];
            prevCorr.setOutcoming(aRoom);
            aRoom.setIncoming(prevCorr);
        }
        _rooms.Add(aRoom);
        _roomsAndCorrs.Add(aRoom);
    }

    public void addCorridor(IXShape corr) {
        if (_roomsAndCorrs.Count != 0) {
            IXShape prevRoom = _roomsAndCorrs[_roomsAndCorrs.Count - 1];
            prevRoom.setOutcoming(corr);
            corr.setIncoming(prevRoom);
        }
        _corrs.Add(corr);
        _roomsAndCorrs.Add(corr);
    }

    public int numberOfRoomsAndCorridors() {
        return _roomsAndCorrs.Count;
    }

    public int[,] asTilesMatrix(IDungeonBoardPlotter plotter) {
        int[,] result = _grid.toIntMatrix();

        if (_roomsAndCorrs.Count > 0) {
            //_roomsAndCorridors[0].plotOn(result, plotter);
            throw new NotImplementedException();
        }
        return result;
    }

    public IXShape[] all() {
        return _roomsAndCorrs.ToArray();
    }

    //Added for Javascript
    public IXShape[] rooms() {
        return _rooms.ToArray();
    }

    //Added for Javascript
    public IXShape[] corridors() {
        return _corrs.ToArray();
    }

    public int rows() {
        return _grid.rows();
    }

    public int cols() {
        return _grid.columns();
    }

    public bool isEmpty() {
        return numberOfRoomsAndCorridors() == 0;
    }

    public void accept(IShapeVisitor visitor) {
        foreach (IXShape each in _roomsAndCorrs) {
            each.accept(visitor);
        }
    }
}
