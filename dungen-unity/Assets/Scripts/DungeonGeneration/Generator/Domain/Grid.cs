namespace DungeonGeneration.Generator.Domain {

    public class Grid {
        private int _columns;
        private int _rows;

        public Grid(int rows, int columns) {
            _rows = rows;
            _columns = columns;
        }

        public int columns() {
            return _columns;
        }

        public int rows() {
            return _rows;
        }

        public bool isWithin(Grid container, Cell topLeftVertex) {
            if (!absTopLeftVertexUsing(topLeftVertex).isWithin(container)) return false;
            if (!absTopRightVertexUsing(topLeftVertex).isWithin(container)) return false;
            if (!absBotRightVertexUsing(topLeftVertex).isWithin(container)) return false;
            if (!absBotLeftVertexUsing(topLeftVertex).isWithin(container)) return false;
            return true;
        }

        public Cell absTopLeftVertexUsing(Cell topLeftVertex) {
            return topLeftVertex.plusCell(0, 0);
        }

        public Cell absBotLeftVertexUsing(Cell topLeftVertex) {
            return topLeftVertex.plusCell(_rows-1, 0);
        }

        public Cell absBotRightVertexUsing(Cell topLeftVertex) {
            return topLeftVertex.plusCell(_rows-1, _columns-1);
        }

        public Cell absTopRightVertexUsing(Cell topLeftVertex) {
            return topLeftVertex.plusCell(0, _columns-1);
        }

        public bool hasCell(int rowIndex, int colIndex) {
            if (rowIndex < 0 || rowIndex >= _rows) return false;
            if (colIndex < 0 || colIndex >= _columns) return false;
            return true;
        }

        public Cell topLeftVertex() {
            return new Cell(0, 0);
        }
        public Cell topRightVertex() {
            return new Cell(0, _columns - 1);
        }
        public Cell bottomLeftVertex() {
            return new Cell(_rows - 1, 0);
        }
        public Cell bottomRightVertex() {
            return new Cell(_rows - 1, _columns - 1);
        }

        public override string ToString() {
            return "XGrid: [" + _rows + ", " + _columns + "]";
        }

    }

}