using System;
using UnityEngine;

public class BlackAndWhiteGizmoDrawer : AShapeVisitor {
    private bool showRoomPerimeter;

    public BlackAndWhiteGizmoDrawer(bool showRoomPerimeter) {
        this.showRoomPerimeter = showRoomPerimeter;
    }

    public override void _visit(APolyShape shape) {
        int vZ = shape.topLeftVertex().row();
        int vX = shape.topLeftVertex().col();
        OIGrid pmap = shape.grid();

     
        pmap.forEach2((row, col, value) => {
            //0 Wall = BLACK
            //1 Floor = WHITE
            
            
            if (value == XTile.FLOOR) {
                Gizmos.color = Color.white;
                Vector3 pos = new Vector3(vX + col, 0, (vZ + row) * -1);
                Gizmos.DrawCube(pos, Vector3.one);
            } else if (value == XTile.WALL && showRoomPerimeter) {
                Gizmos.color = Color.black;
                Vector3 pos = new Vector3(vX + col, 0, (vZ + row) * -1);
                Gizmos.DrawCube(pos, Vector3.one);
            }
            

            //Gizmos.color = (value == 0) ? Color.black : Color.white;
            
            //Vector3 pos = new Vector3(vX + x, 0, (vY + y) * -1);
            //Gizmos.DrawCube(pos, Vector3.one);
        });
    }

    public override void _visit(FreeShape shape) {
        shape.forEachCell((x, y, sameShape) => {
            //FreeShape ogni cella vale 1
            Gizmos.color = Color.white;
            Vector3 pos = new Vector3(x, 0, y * -1);
            Gizmos.DrawCube(pos, Vector3.one);
        });
    }

}