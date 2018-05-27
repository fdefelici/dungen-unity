using UnityEngine;

public struct CaveMesh {
    public readonly Mesh map;
    public readonly Mesh wall;
    public readonly Mesh floor;

    public CaveMesh(Mesh mapMesh, Mesh wallMesh, Mesh floorMesh) {
        this.map = mapMesh;
        this.wall = wallMesh;
        this.floor = floorMesh;
    }

}
