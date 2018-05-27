public interface IShapeVisitor {
    void visit(IXShape aShape);
    void visit(APolyShape aShape);
    void visit(RectShape aShape);
    void visit(ElliShape aShape);
    void visit(FreeShape aShape);
}