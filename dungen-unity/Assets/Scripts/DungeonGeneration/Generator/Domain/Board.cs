using System;
using System.Collections.Generic;
using DungeonGeneration.Generator.Plotters;

namespace DungeonGeneration.Generator.Domain {

    public class Board {
        private Grid _grid;
        private List<IShape> _roomsAndCorridors;
        private int _margin;

        public Board(int rows, int columns)
            : this(rows, columns, 0) {
        }
        public Board(int rows, int columns, int margin)
            : this(new Grid(rows, columns), margin) {
        }
        public Board(Grid mapGrid)
            : this(mapGrid, 0) {
        }
        public Board(Grid mapGrid, int margin) {
            _grid = mapGrid;
            _roomsAndCorridors = new List<IShape>();
            _margin = margin;
        }

     
        public bool fitsIn(IShape aSquare) {
            if (!aSquare.isWithin(_grid)) return false;
            //Evito di controllare la collisione con l'ultimo, poiche' e' necessaria
            for (int i = 0; i < _roomsAndCorridors.Count - 1; i++) {
                IShape each = _roomsAndCorridors[i];
                if (aSquare.collidesWith(each)) return false;
            }
            return true;
        }

        public void addRoom(Room aRoom) {
            if (_roomsAndCorridors.Count != 0) {
                Corridor corr = (Corridor)_roomsAndCorridors[_roomsAndCorridors.Count - 1];
                corr.setDestinationRoom(aRoom);
                aRoom.setCorridorIncoming(corr);
            }
            _roomsAndCorridors.Add(aRoom);
        }

        public int[,] asTilesMatrix(IDungeonBoardPlotter plotter) {
            int[,] result = new int[_grid.rows(), _grid.columns()];

            if (_roomsAndCorridors.Count > 0) {
                _roomsAndCorridors[0].plotOn(result, plotter);
            }
            return result;
        }

        public void addCorridor(Corridor corr) {
            if (_roomsAndCorridors.Count != 0) {
                Room room = (Room)_roomsAndCorridors[_roomsAndCorridors.Count - 1];
                room.setCorridorOutcoming(corr);
                corr.setSourceRoom(room);
            }
            _roomsAndCorridors.Add(corr);
        }

        public void removeLast() {
            if (_roomsAndCorridors.Count == 0) return;

            IShape last = _roomsAndCorridors[_roomsAndCorridors.Count - 1];
            IShape beforeLast = null;
            if (_roomsAndCorridors.Count >= 2) {
                beforeLast = _roomsAndCorridors[_roomsAndCorridors.Count - 2];
            }

            if (last is Room) {
                Room room = (Room)last;
                room.setCorridorIncoming(null);
                if (beforeLast != null) {
                    Corridor corr = (Corridor)beforeLast;
                    corr.setDestinationRoom(null);
                }
            } else {
                Corridor corr = (Corridor)last;
                corr.setSourceRoom(null);
                if (beforeLast != null) {
                    Room room = (Room)_roomsAndCorridors[_roomsAndCorridors.Count - 2];
                    room.setCorridorOutcoming(null);
                }
            }
            _roomsAndCorridors.RemoveAt(_roomsAndCorridors.Count - 1);
        }

        public Board resize(int mapMargin) {
            if (mapMargin < 0) return this;

            Board resized = new Board(rows() + mapMargin*2, cols() + mapMargin * 2);
            foreach (IShape each in _roomsAndCorridors) {
                if (each is Room) {
                    Room r = (Room)each;
                    Cell vert = r.topLeftVertex().plusCell(mapMargin, mapMargin);
                    Room relocated = new Room(vert, r.grid());
                    resized.addRoom(relocated);
                } else {
                    Corridor c = (Corridor)each;
                    Cell vert = c.topLeftVertex().plusCell(mapMargin, mapMargin);
                    Corridor.Orientation orient = c.isOrizontal() ? Corridor.Orientation.horizontal : Corridor.Orientation.vertical;
                    Corridor relocated = new Corridor(vert, c.grid(), orient);
                    resized.addCorridor(relocated);
                }
            }
            return resized;
        }

        public Board crop() {
            return crop(0);
        }

        public Board crop(int marginToAddAfterCrop) {
            Cell upperTopLeftVert = null;
            Cell righterBottomRightVert = null;
            Cell downerBottomRightVert = null;
            Cell lefterTopLeftVert = null;
            foreach (Room each in rooms()) {
                if (upperTopLeftVert == null) {
                    upperTopLeftVert = each.topLeftVertex();
                    righterBottomRightVert = each.bottomRightVertex();
                    downerBottomRightVert = righterBottomRightVert;
                    lefterTopLeftVert = upperTopLeftVert;
                } else {
                    if (each.topLeftVertex().isRowLesserThan(upperTopLeftVert)) {
                        upperTopLeftVert = each.topLeftVertex();
                    }
                    if (each.bottomRightVertex().isColGreatherThan(righterBottomRightVert)) {
                        righterBottomRightVert = each.bottomRightVertex();
                    }
                    if (each.bottomRightVertex().isRowGreatherThan(downerBottomRightVert)) {
                        downerBottomRightVert = each.bottomRightVertex();
                    }
                    if (each.topLeftVertex().isColLesserThan(lefterTopLeftVert)) {
                        lefterTopLeftVert = each.topLeftVertex();
                    }
                }
            }

            int rows = downerBottomRightVert.row() - upperTopLeftVert.row() + 1 + marginToAddAfterCrop*2;
            int cols = righterBottomRightVert.col() - lefterTopLeftVert.col() + 1 + marginToAddAfterCrop * 2;

            int cropUp = upperTopLeftVert.row() - marginToAddAfterCrop;
            int cropLeft = lefterTopLeftVert.col() - marginToAddAfterCrop;

            Board cropped = new Board(rows, cols);
            foreach(IShape each in _roomsAndCorridors) {
                if (each is Room) {
                    Room r = (Room)each;
                    Cell vert = r.topLeftVertex().minus(cropUp, cropLeft);
                    Room relocated = new Room(vert, r.grid());
                    cropped.addRoom(relocated);
                } else {
                    Corridor c = (Corridor)each;
                    Cell vert = c.topLeftVertex().minus(cropUp, cropLeft);
                    Corridor.Orientation orient = c.isOrizontal() ? Corridor.Orientation.horizontal : Corridor.Orientation.vertical;
                    Corridor relocated = new Corridor(vert, c.grid(), orient);
                    cropped.addCorridor(relocated);
                }
            }
            return cropped;
        }

        public int cols() {
            return _grid.columns();
        }

        public int rows() {
            return _grid.rows();
        }

        public int numberOfRoomsAndCorridors() {
            return _roomsAndCorridors.Count;
        }

        //Added for Javascript
        public Room[] rooms() {
            List<Room> result = new List<Room>();
            foreach(IShape each in _roomsAndCorridors) {
                if (each is Room) result.Add((Room)each);
            }
            return result.ToArray();
        }

        //Added for Javascript
        public Corridor[] corridors() {
            List<Corridor> result = new List<Corridor>();
            foreach (IShape each in _roomsAndCorridors) {
                if (each is Corridor) result.Add((Corridor)each);
            }
            return result.ToArray();
        }

        public int roomSize() {
            return rooms().Length;
        }
    }
}