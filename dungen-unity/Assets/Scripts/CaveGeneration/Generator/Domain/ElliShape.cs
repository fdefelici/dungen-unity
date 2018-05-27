using System;
using DungeonGeneration.Generator.Domain;

public class ElliShape : APolyShape {
  
    public ElliShape(Cell topLeftVertex, OIGrid map): 
        base(topLeftVertex, map) {
    }

    public override bool isCellValid(int row, int col) {
        if (row < 0 || row >= grid().rows()
            || col < 0 || col >= grid().columns()) return false;

        /* CIRCLE CASE
        int center_x = grid().rows() / 2;    
        int center_y = grid().columns() / 2; 
        int radius = grid().rows() / 2;      

        int sqrX = (int)Math.Pow((x - center_x), 2);
        int sqrY = (int)Math.Pow((y - center_y), 2);
        int sqrR = (int)Math.Pow(radius, 2);

        return (sqrX + sqrY) <= sqrR;
        */

        //Point within Ellipse Functon F(x, y) = (x2 / A2) + (y2 / B2) <= 1.
        float center_row = (float)grid().rows() / 2f;        
        float center_col = (float)grid().columns() / 2f;
        float radius_horiz = center_col;
        float radius_vert = center_row;

        double sqrY = Math.Pow((row - center_row), 2);
        double sqrX = Math.Pow((col - center_col), 2);
        double sqrRadHor = Math.Pow(radius_horiz, 2);
        double sqrRadVer = Math.Pow(radius_vert, 2);

        double result = (sqrX / sqrRadHor + sqrY / sqrRadVer);
        return result <= 1f;
    }
}