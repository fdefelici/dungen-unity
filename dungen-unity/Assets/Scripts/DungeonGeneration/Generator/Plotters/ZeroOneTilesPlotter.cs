
using System;
using DungeonGeneration.Generator.Domain;

namespace DungeonGeneration.Generator.Plotters {

    public class ZeroOneTilesPlotter : IDungeonBoardPlotter {

        public void applyOnCorridor(Corridor corridor, int[,] map) {
            for (int row = 0; row < corridor.height(); row++) {
                for (int col = 0; col < corridor.width(); col++) {
                    Cell pos = corridor.topLeftVertex().plusCell(row, col);
                    int rowPos = pos.row();
                    int colPos = pos.col();

                    if (pos.isEqual(corridor.topLeftVertex())) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isEqual(corridor.topRightVertex())) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isEqual(corridor.bottomRightVertex())) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isEqual(corridor.bottomLeftVertex())) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isWithin(corridor.topLeftVertex(), corridor.topRightVertex()) && corridor.isOrizontal()) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isWithin(corridor.topRightVertex(), corridor.bottomRightVertex()) && corridor.isVertical()) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isWithin(corridor.bottomLeftVertex(), corridor.bottomRightVertex()) && corridor.isOrizontal()) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isWithin(corridor.topLeftVertex(), corridor.bottomLeftVertex()) && corridor.isVertical()) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Empty;
                    }
                }
            }
        }

        public void applyOnRoom(Room room, int[,] map) {
            for (int row = 0; row < room.height(); row++) {
                for (int col = 0; col < room.width(); col++) {
                    Cell pos = room.topLeftVertex().plusCell(row, col);
                    int rowPos = pos.row();
                    int colPos = pos.col();

                    if (pos.isEqual(room.topLeftVertex())) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isEqual(room.topRightVertex())) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isEqual(room.bottomRightVertex())) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isEqual(room.bottomLeftVertex())) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isWithin(room.topLeftVertex(), room.topRightVertex())) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isWithin(room.topRightVertex(), room.bottomRightVertex())) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isWithin(room.bottomLeftVertex(), room.bottomRightVertex())) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else if (pos.isWithin(room.topLeftVertex(), room.bottomLeftVertex())) {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Wall;
                    } else {
                        map[rowPos, colPos] = (int)ZeroOneTileType.Empty;
                    }
                }
            }
        }
    }
}