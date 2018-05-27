using DungeonGeneration.Generator.Domain;
public class CellPair {
    public Cell cell1;
    public Cell cell2;

    public CellPair(Cell cell1Sel, Cell cell2Sel) {
        this.cell1 = cell1Sel;
        this.cell2 = cell2Sel;
    }

    public override string ToString() {
        return "Pair: " + cell1 + " " + cell2;
    }
}