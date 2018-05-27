using UnityEngine;

public class MeshFactory {

    public static Mesh createPlane(OIGrid grid, int tileSize, int cellValueToRender) {
        grid = grid.mirrorOnRows(); //Altrimenti la mesh viene fatta capoversa per come e' implementato il Triangulator

        XTriangulator2 triangulator = new XTriangulator2();
        triangulator.setSquareSize(tileSize);
        triangulator.setZeroBased();
        triangulator.setCellValueToRender(cellValueToRender);

        //Mesh mesh = new XTriangulator2(tileSize, false).triangolate(grid);
        Mesh mesh = triangulator.triangolate(grid);
        Vector3[] vert = mesh.vertices; //NOTA: mesh.vertices ritorna un CLONE

        mesh.uv = flatUVfor(vert);
        mesh.vertices = vert;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        //TANGENTS ????        
        return mesh;
    }

    private static Vector2[] flatUVfor(Vector3[] vertices) {
        const float UVSCALE = 50f;
        var uv = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++) {
            uv[i].x = vertices[i].x / UVSCALE;
            uv[i].y = vertices[i].z / UVSCALE;
        }
        return uv;
    }


}
