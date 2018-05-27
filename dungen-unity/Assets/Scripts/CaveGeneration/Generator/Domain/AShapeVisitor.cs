using System;

public abstract class AShapeVisitor: IShapeVisitor {
    public virtual void visit(RectShape aShape) {
        _visit(aShape as RectShape);
        _visit(aShape as APolyShape);
        _visit(aShape as IXShape);
    }

    public virtual void visit(APolyShape aShape) {
        if (aShape is RectShape) visit(aShape as RectShape);
        else if (aShape is ElliShape) visit(aShape as ElliShape);
        else throw new NotImplementedException("Missing case for: " + aShape.GetType());
    }

    public virtual void visit(IXShape aShape) {
        if (aShape is APolyShape) visit(aShape as APolyShape);
        else if (aShape is FreeShape) visit(aShape as FreeShape);
        else throw new NotImplementedException("Missing case for: " + aShape.GetType());
    }

    public virtual void visit(ElliShape aShape) {
        _visit(aShape as ElliShape);
        _visit(aShape as APolyShape);
        _visit(aShape as IXShape);
    }
    public virtual void visit(FreeShape aShape) {
        _visit(aShape as FreeShape);
        _visit(aShape as IXShape);
    }

    public virtual void _visit(IXShape aShape) {}
    public virtual void _visit(APolyShape aShape) { }
    public virtual void _visit(RectShape aShape) { }
    public virtual void _visit(ElliShape aShape) { }
    public virtual void _visit(FreeShape aShape) { }

}