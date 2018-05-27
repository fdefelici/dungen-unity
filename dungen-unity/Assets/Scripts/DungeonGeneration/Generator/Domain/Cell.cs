using System;

namespace DungeonGeneration.Generator.Domain {
    public class Cell {
        private int _row;
        private int _col;

        public Cell(int aRow, int aCol) {
            _row = aRow;
            _col = aCol;
        }

        public override bool Equals(object obj) {
            return isEqual((Cell)obj);
        }
        public override string ToString() {
            return "Cell: [" + _row + ", " + _col + "]";
        }
        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        public bool hasNegativeIndexes() {
            if (_row < 0) return true;
            if (_col < 0) return true;
            return false;
        }

        public Cell toNearestPositiveCell() {
            int row = _row < 0 ? 0 : _row;
            int col = _col < 0 ? 0 : _col;
            return new Cell(row, col);
        }

        public int distance(Cell other) {
            int rowDistance = Math.Abs(_row - other._row) + 1;

            int colDistance = Math.Abs(_col - other._col) + 1;
            return rowDistance * colDistance;
        }

        public bool hasSameRow(Cell other) {
            return _row == other._row;
        }

        public bool hasSameColumn(Cell other) {
            return _col == other._col;
        }

        public bool isWithin(Cell start, Cell end) {
            if (_row < start._row) return false;
            if (_col < start._col) return false;
            if (_row > end._row) return false;
            if (_col > end._col) return false;
            return true;
        }

        public bool isWithin(Grid aGrid) {
            return aGrid.hasCell(_row, _col);
        }

        public int row() {
            return _row;
        }

        public int col() {
            return _col;
        }

        public bool isEqual(Cell other) {
            if (other._row != _row) return false;
            if (other._col != _col) return false;
            return true;
        }

        public Cell plusCell(int rowCells, int colCells) {
            return new Cell(_row + rowCells, _col + colCells);
        }

        public Cell minusCell(int rowCells, int colCells) {
            return new Cell(_row - rowCells, _col - colCells);
        }

        public Cell minusSize(int rowSize, int colSize) {
            if (rowSize == 0) rowSize = 1;
            if (colSize == 0) colSize = 1;
            return new Cell(_row - (rowSize - 1), _col - (colSize - 1));
        }
        public Cell plusSize(int rowSize, int colSize) {
            if (rowSize == 0) rowSize = 1;
            if (colSize == 0) colSize = 1;
            return new Cell(_row + (rowSize - 1), _col + (colSize - 1));
        }

        public Cell[] cells(Cell other) {
            Cell min = this;
            Cell max = other;
            if (isGreatherThan(other)) {
                min = other;
                max = this;
            }
            int cellsNumber = distance(other);
            Cell[] result = new Cell[cellsNumber];

            int cellIndex = 0;
            for (int row = min._row; row <= max._row; row++) {
                for (int col = min._col; col <= max._col; col++) {
                    result[cellIndex] = new Cell(row, col);
                    cellIndex++;
                }
            }
            return result;
        }

        public bool isLesserThan(Cell other) {
            int distanceA = new Cell(0, 0).distance(this);
            int distanceB = new Cell(0, 0).distance(other);
            return distanceA < distanceB;
        }

        public bool isRowLesserThan(Cell other) {
            return row() < other.row();
        }
        public bool isColLesserThan(Cell other) {
            return col() < other.col();
        }

        public bool isGreatherThan(Cell other) {
            int distanceA = new Cell(0, 0).distance(this);
            int distanceB = new Cell(0, 0).distance(other);
            return distanceA > distanceB;
        }

        public bool isRowGreatherThan(Cell other) {
            return row() > other.row();
        }
        public bool isColGreatherThan(Cell other) {
            return col() > other.col();
        }

        public Cell minus(int v1, int v2) {
            return minusCell(v1, v2);
        }

        public double magnetude(Cell other) {
            int diffY = Math.Abs(row()) - Math.Abs(other.row());
            int diffX = Math.Abs(col()) - Math.Abs(other.col());
            int sum = (int)(Math.Pow(diffY, 2) + Math.Pow(diffX, 2));
            return Math.Round(Math.Sqrt(sum), 2);
        }

        public Cell plus(Cell cell) {
            return plusCell(cell.row(), cell.col());
        }

    }
}