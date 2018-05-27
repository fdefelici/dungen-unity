using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonGeneration.Generator.Domain;


public class ZeroOneCavePlotter : ICaveBoardPlotter<int[,]> {
    private OIGrid _grid;
    //private int _OTHER_0;
    //private int _WALL_1;

    public ZeroOneCavePlotter() {
        _grid = new OIGrid(0, 0);
        //_OTHER_0 = 0;
        //_WALL_1 = 1;
    }

    public void applyOn(CaveBoard board) {
        _grid = new OIGrid(board.rows(), board.cols());

        OIGrid populated = new OIGrid(board.rows(), board.cols());
        foreach(IXShape eachShape in board.rooms()) {
            eachShape.forEachCellAbs((row, col, value) => {
                populated.setCellValue(row, col, value);
            });
        }
        //Cosi i corridoi si sovrappongono alle celle 0 delle room.
        foreach (IXShape eachShape in board.corridors()) {
            eachShape.forEachCellAbs((row, col, value) => {
                populated.setCellValue(row, col, value);
            });
        }
            
        OIGrid inverted = populated.invert();
        inverted.forEach2((row, col, value) => {
            if (value == 1 && inverted.existsCellNeighborValue(row, col, 0)) {
                _grid.setCellValue(row, col, 1);
            } else { 
                _grid.setCellValue(row, col, 0);
            }
        });
    }

    public int[,] result() {
        return _grid.asMatrix();
    }

}

