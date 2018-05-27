using UnityEngine;

public class CaveRenderer : ScriptableObject {
    private CaveGeneratorBehaviour _behavior;

    private CaveRenderer(CaveGeneratorBehaviour behaviour) {
        _behavior = behaviour;
    }

    public void convertToMeshes(OIGrid grid) {
        int wallHeight = _behavior._wallHeight;
        int seed = _behavior._seed;

        IXHeightMap ceilHeight;
        if (_behavior._ceilHeightVariation == 0) {
            ceilHeight = new XFlatHeightMap(wallHeight); 
        } else {
            int ceilMinHeight = wallHeight;
            int ceilMaxHeight = wallHeight + _behavior._ceilHeightVariation;
            ceilHeight = new XPerlinHeightMap(ceilMinHeight, ceilMaxHeight, seed);
        }

      
        CaveMeshFactory ms = new CaveMeshFactory(ceilHeight);
        CaveMesh result = ms.create(grid, wallHeight);

        _behavior._ceilMeshFilter.mesh = result.map;

        _behavior._wallMeshFilter.mesh = result.wall;
        _behavior._wallMeshFilter.gameObject.GetComponent<MeshCollider>().sharedMesh = result.wall;

        _behavior._floorMeshFilter.mesh = result.floor;
        _behavior._floorMeshFilter.gameObject.GetComponent<MeshCollider>().sharedMesh = result.floor;
    }

        
    public static CaveRenderer newInstance(CaveGeneratorBehaviour behaviour) {
        CaveRenderer renderer = ScriptableObject.CreateInstance<CaveRenderer>();
        renderer.setBehaviour(behaviour);
        return renderer;
    }

    private void setBehaviour(CaveGeneratorBehaviour behav) {
        _behavior = behav;
    }    
}
