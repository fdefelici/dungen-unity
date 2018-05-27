using DungeonGeneration.Generator.Domain;
using System;
using System.Collections.Generic;

public class CaveCorridorFactory {
    public static FreeShape createCorrShape(IXShape roomA, IXShape roomB, int corrWidth) {
        CellPair pair = roomA.shortestCellPair(roomB);
        List<Cell> line = GetLine(pair.cell1, pair.cell2);
        FreeShape corrAtoB = new FreeShape();
        foreach (Cell each in line) {
            List<Cell> vCells = DrawCircle(each, corrWidth);
            //To avoid cell overlapping between rooms with corridor
            foreach (Cell vEach in vCells) {
                if (!roomA.hasCellAbsValue(vEach, XTile.FLOOR) &&
                    !roomB.hasCellAbsValue(vEach, XTile.FLOOR)) {
                    corrAtoB.add(vEach);
                }
            }
        }
        return corrAtoB;
    }

    private static List<Cell> GetLine(Cell from, Cell to) {
        List<Cell> line = new List<Cell>();

        int x = from.row();
        int y = from.col();

        int dx = to.row() - from.row();
        int dy = to.col() - from.col();

        bool inverted = false;
        int step = Math.Sign(dx);
        int gradientStep = Math.Sign(dy);

        int longest = Math.Abs(dx);
        int shortest = Math.Abs(dy);

        if (longest < shortest) {
            inverted = true;
            longest = Math.Abs(dy);
            shortest = Math.Abs(dx);

            step = Math.Sign(dy);
            gradientStep = Math.Sign(dx);
        }

        int gradientAccumulation = longest / 2;
        for (int i = 0; i < longest; i++) {
            line.Add(new Cell(x, y));

            if (inverted) {
                y += step;
            } else {
                x += step;
            }

            gradientAccumulation += shortest;
            if (gradientAccumulation >= longest) {
                if (inverted) {
                    x += gradientStep;
                } else {
                    y += gradientStep;
                }
                gradientAccumulation -= longest;
            }
        }
        return line;
    }


    private static List<Cell> DrawCircle(Cell cell, int sectionSize) {
        List<Cell> result = new List<Cell>();
        for (int row = -sectionSize; row <= sectionSize; row++) {
            for (int col = -sectionSize; col <= sectionSize; col++) {
                if (row * row + col * col <= sectionSize * sectionSize) {
                    int drawX = cell.row() + row;
                    int drawY = cell.col() + col;
                    /*
                    if (IsInMapRange(drawX, drawY)) {
                        map[drawX, drawY] = 0;
                    }
                    */
                    result.Add(new Cell(drawX, drawY));
                }
            }
        }
        return result;
    }
}
