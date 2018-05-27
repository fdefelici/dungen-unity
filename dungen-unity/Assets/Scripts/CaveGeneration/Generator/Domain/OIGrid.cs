using System;
using DungeonGeneration.Generator.Domain;

public class OIGrid : Grid {
    private int[,] _cells;

    public int[,] toIntMatrix() {
        return (int[,])_cells.Clone();
    }

    // Inverto le colonne della matrice, cosi riesco a vedere a video la mesh finale
    // nello stesso verso in cui e' la matrice (map) originale.
    public OIGrid mirrorOnColumns() {
        OIGrid result = new OIGrid(rows(), columns());
        for (int x = 0; x < rows(); x++) {
            for (int y = 0; y < columns(); y++) {
                //int invertedY = _height - 1 - y;
                int invertedY = columns() - 1 - y;
                result[x, invertedY] = _cells[x, y];
            }
        }
        return result;
    }

    public OIGrid replace(int toBeReplace, int newValue) {
        OIGrid result = new OIGrid(rows(), columns());
        forEach2((row, col, value) => {
            if (value == toBeReplace) result.setCellValue(row, col, newValue);
            else result.setCellValue(row, col, value);
        });
        return result;
    }

    public OIGrid mirrorOnRows() {
        OIGrid result = new OIGrid(rows(), columns());
        for (int x = 0; x < rows(); x++) {
            for (int y = 0; y < columns(); y++) {
                int inverted = rows() - 1 - x;
                result[inverted, y] = _cells[x, y];
            }
        }
        return result;
    }

    public OIGrid rotate90() {
        OIGrid result = new OIGrid(columns(), rows());
        for (int row = 0; row < result.rows(); row++) {
            for (int col = 0; col < result.columns(); col++) {
                int cRow = rows() - 1 - col;
                int cCol = row;
                result[row, col] = _cells[cRow, cCol];
            }
        }
        return result;
    }

    public bool existsCellNeighborValue(int row, int col, int value) {
        if (isCellValid(row-1, col) && hasCellValue(row-1, col, value)) {
            return true;
        }
        if (isCellValid(row + 1, col) && hasCellValue(row + 1, col, value)) {
            return true;
        }
        if (isCellValid(row, col-1) && hasCellValue(row, col-1, value)) {
            return true;
        }
        if (isCellValid(row, col + 1) && hasCellValue(row, col + 1, value)) {
            return true;
        }
        return false;
    }

    public OIGrid clone() {
        OIGrid result = new OIGrid(rows(), columns());
        forEach2((x, y, value) => {
            result[x, y] = value;
        });
        return result;
    }

    //0 1
    public OIGrid invert() {
        OIGrid result = new OIGrid(rows(), columns());
        forEach2((x, y, value) => {
            int valueInverted = value == 0 ? 1 : 0;
            result[x, y] = valueInverted;
        });
        return result;
    }


    public OIGrid(int rows, int columns)
        : this(new int[rows, columns]) {
    }

    public OIGrid(int[,] cells) 
        : base(cells.GetLength(0), cells.GetLength(1)) { 
        _cells = cells;
    }

    public int this[int x, int y] {
        get { return _cells[x, y]; }
        set { _cells[x, y] = value; }
    }

    public int valueForCell(int x, int y) {
        if (x < 0 || x >= rows()) return Int32.MinValue;
        if (y < 0 || y >= columns()) return Int32.MinValue;
        return _cells[x, y];
    }

    public override bool Equals(object obj) {
        OIGrid other = obj as OIGrid;
        if (rows() != other.rows()) return false;
        if (columns() != other.columns()) return false;
        bool allEquals = true;
        forEach((x, y, map) => {
            if (!other.hasCellValue(x, y, _cells[x, y])) {
                allEquals = false;
                return;
            };
        });
        return allEquals;
    }

    public void forEach(Action<int, int, OIGrid> doFunct) {
        for (int x = 0; x < rows(); x++) {
            for (int y = 0; y < columns(); y++) {
                doFunct(x, y, this);
            }
        }
    }

    public void forEach2(Action<int, int, int> doFunct) {
        for (int x = 0; x < rows(); x++) {
            for (int y = 0; y < columns(); y++) {
                doFunct(x, y, _cells[x, y]);
            }
        }
    }

    public bool isCellOnEdge(int cellX, int cellY) {
        return cellX == 0 || cellX == rows() - 1 || cellY == 0 || cellY == columns() - 1;
    }

    public bool isCellValid(int cellX, int cellY) {
        return cellX >= 0 && cellX <= rows() - 1 && cellY >= 0 && cellY <= columns() - 1;
    }

    /*
    public void printOnUnityConsole() {
        Debug.Log("Size: [" + rows() + ", " + columns()  + "]");
        String row = "";
        for (int x = 0; x < rows(); x++) {
            //row = "";
            for (int y = 0; y < columns(); y++) {
                row += _cells[x, y];
            }
            row += "\n";
        }
        Debug.Log(row);
    }
    */

    public void printOnConsole() {
        System.Console.WriteLine("Size: [" + rows() + ", " + columns() + "]");
        String row = "";
        for (int eachRow = 0; eachRow < rows(); eachRow++) {
            row = "";
            for (int eachCol = 0; eachCol < columns(); eachCol++) {
                row += _cells[eachRow, eachCol];
            }
            System.Console.WriteLine(row);
        }
    }

    public int[,] asMatrix() {
        return _cells;
    }

    public void setCellValue(int cellX, int cellY, int value) {
        if (!isCellValid(cellX, cellY)) return;
        _cells[cellX, cellY] = value;
    }

    public bool hasCellValue(int cellX, int cellY, int value) {
        if (!isCellValid(cellX, cellY)) return false;
        return _cells[cellX, cellY] == value;
    }
}
