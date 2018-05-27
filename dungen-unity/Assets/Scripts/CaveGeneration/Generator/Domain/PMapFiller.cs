public class OIGridFiller : AShapeVisitor {
    private OIGrid grid;

    public OIGridFiller(OIGrid grid) {
        this.grid = grid;
    }

    public override void _visit(IXShape shape) {
        shape.forEachCellAbs((x, y, value) => {
            this.grid.setCellValue(x, y, value);
        });
    }
}