using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class PrepareForTestShapeVisitor : AShapeVisitor {
    private string _varName;

    public PrepareForTestShapeVisitor(String varName) {
        _varName = varName;
    }

    public override void _visit(ElliShape aShape) {
        printInstance(aShape, "ElliShape");
        printSetMethod(aShape);
    }

    public override void _visit(RectShape aShape) {
        printInstance(aShape, "RectShape");
        printSetMethod(aShape);
    }

    public override void _visit(FreeShape aShape) {
        System.Console.WriteLine("FreeShape " + _varName + " = new FreeShape();");
        printSetMethod(aShape);
    }


    private void printInstance(APolyShape aShape, string className) {
        int row = aShape.topLeftVertex().row();
        int col = aShape.topLeftVertex().col();
        int rows = aShape.grid().rows();
        int cols = aShape.grid().columns();
        System.Console.WriteLine(className + " " + _varName + " = new " + className + "(new Cell(" + row + ", " + col + "), new OIGrid(" + rows + ", " + cols + "));");
    }

    private void printSetMethod(IXShape aShape) {
        aShape.forEachCell2((row, col, value) => {
            if (value == 1) {
                String result = _varName + ".setCellValue(" + row + ", " + col + ", XTile.FLOOR);";
                System.Console.WriteLine(result);
            }
        });
    }

   
}