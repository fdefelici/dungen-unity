using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveMeshFactory {
    private IXHeightMap _ceilHeightMap;

    public CaveMeshFactory(IXHeightMap ceilHeightMap) {
        _ceilHeightMap = ceilHeightMap;
    }
  
    public CaveMesh create(OIGrid grid, int wallHeight) {
        //grid = grid.mirror();
        grid = grid.rotate90();

        IXHeightMap floorHeight = new XFlatHeightMap(0);
        //IXHeightMap ceilHeight = new XFlatHeightMap(wallHeight);
        IXHeightMap ceilHeight = _ceilHeightMap;

        Mesh floorMesh = createPlaneMesh(grid.invert(), floorHeight);
        Mesh ceilMesh = createPlaneMesh(grid, ceilHeight);
        Mesh wallMesh = createWallMesh(grid, floorHeight, ceilHeight);

        return new CaveMesh(ceilMesh, wallMesh, floorMesh);
    }

    private Mesh createWallMesh(OIGrid grid, IXHeightMap floorHeighMap, IXHeightMap ceilHeightMap) {
        int[,] map = grid.asMatrix();

        Vector3 center = new Vector3(-map.GetLength(1) / 2f + 0.5f, 0, -map.GetLength(0) / 2f + 0.5f);
        int squareSize = 1;

        List<Vector3[]> outlines = XOutlineGenerator.Generate(grid, center, squareSize);
        XMeshAdapter meshAdapter = new XMeshAdapter();
        meshAdapter.vertices = XWallBuilder.GetVertices(outlines, floorHeighMap, ceilHeightMap);
        meshAdapter.uv = XWallBuilder.GetUVs(outlines, meshAdapter.vertices);
        meshAdapter.triangles = XWallBuilder.GetTriangles(outlines);

        return meshAdapter.asMesh();
    }


    static Mesh createPlaneMesh(OIGrid grid, IXHeightMap heightMap) {
        Mesh mesh = new XTriangulator().triangolate(grid);
        Vector3[] vert = mesh.vertices; //NOTA: mesh.vertices ritorna un CLONE

        mesh.uv = flatUVfor(vert);

        applyHeightMap(vert, heightMap);
        mesh.vertices = vert;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        //TANGENTS ????        
        return mesh;
    }

    static Vector2[] flatUVfor(Vector3[] vertices) {
        const float UVSCALE = 50f;
        var uv = new Vector2[vertices.Length];
        for (int i = 0; i < vertices.Length; i++) {
            uv[i].x = vertices[i].x / UVSCALE;
            uv[i].y = vertices[i].z / UVSCALE;
        }
        return uv;
    }

    static void applyHeightMap(Vector3[] vertices, IXHeightMap heightMap) {
        for (int i = 0; i < vertices.Length; i++) {
            Vector3 vertex = vertices[i];
            vertices[i].y = heightMap.yFor(vertex.x, vertex.z);
        }
    }
}