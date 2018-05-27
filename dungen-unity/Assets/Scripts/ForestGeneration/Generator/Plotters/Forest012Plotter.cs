using System;

//0 = FLOOR
//1 = TREE / WALL
//2 = OTHER
public class Forest012Plotter : ICaveBoardPlotter<int[,]> {
    protected OIGrid _grid;
    private int _treeTickness;

    public Forest012Plotter() {
        _grid = new OIGrid(0, 0);
        _treeTickness = 1;
    }

    public Forest012Plotter(int treeTickness) {
        _grid = new OIGrid(0, 0);
        _treeTickness = treeTickness;
    }

    public void applyOn(CaveBoard board) {
        _grid = new OIGrid(board.rows(), board.cols());

        OIGrid populated = new OIGrid(board.rows(), board.cols());
        foreach (IXShape eachShape in board.rooms()) {
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
        //1 DENTRO
        //0 FUORI


        OIGrid inverted = populated.invert();
        //0 DENTRO
        //1 FUORI
        inverted.forEach2((row, col, value) => {
            if (value == 1 && inverted.existsCellNeighborValue(row, col, 0)) {
                _grid.setCellValue(row, col, 3);
            } else {
                _grid.setCellValue(row, col, 1);
            }
        });
        //_grid
        //3 MURO
        //1 FUORI
        //1 DENTRO

        populated.forEach2((row, col, value) => {
            if (value == 1 && _grid.hasCellValue(row, col, 1)) {
                _grid.setCellValue(row, col, 0); //DENTRO
            } else if (value == 0 && _grid.hasCellValue(row, col, 3)) {
                _grid.setCellValue(row, col, 1); //MURO
            } else {
                _grid.setCellValue(row, col, 2); //FUORI
            }
        });

        int edgeSize = _treeTickness;
        for (int i = 1; i < edgeSize; i++) {
            OIGrid cloned = _grid.clone();
            cloned.forEach2((row, col, value) => {
                if (value == 2 && cloned.existsCellNeighborValue(row, col, 1)) {
                    _grid.setCellValue(row, col, 1); //MURO
                }
            });
        }
    }

    public int[,] result() {
        return _grid.asMatrix();
    }

    
}