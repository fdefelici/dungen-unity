using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonGeneration.Generator.Domain;


public class ZeroOneFillerCavePlotter : ICaveBoardPlotter<int[,]> {
    private OIGrid _grid;
    
    public ZeroOneFillerCavePlotter() {
        _grid = new OIGrid(0, 0);
    }

    public void applyOn(CaveBoard board) {
        _grid = new OIGrid(board.rows(), board.cols());
        foreach(IXShape eachShape in board.rooms()) {
            eachShape.forEachCellAbs((row, col, value) => {
                _grid.setCellValue(row, col, value);
            });
        }
        foreach (IXShape eachShape in board.corridors()) {
            eachShape.forEachCellAbs((row, col, value) => {
                _grid.setCellValue(row, col, value);
            });
        }
    }

    public int[,] result() {
        return _grid.asMatrix();
    }
}

