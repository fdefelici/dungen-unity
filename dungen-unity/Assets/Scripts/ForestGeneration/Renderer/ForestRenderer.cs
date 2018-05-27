using UnityEngine;
using System;
using DungeonGeneration.Generator.Pickers;

public class ForestRenderer : ScriptableObject {
    private ForestGeneratorBehaviour _behavior;
    private const int TREE = 1;
    private const int FLOOR = 0;
    private const int TILE_SIZE = 2;

    private ForestRenderer(ForestGeneratorBehaviour behaviour) {
        _behavior = behaviour;
    }

    public void render(OIGrid grid) {
        Destroy(GameObject.Find("BoardHolder"));

        GameObject boardHolder = new GameObject("BoardHolder");
        boardHolder.transform.parent = _behavior.transform;

        addFloor(boardHolder, grid);
        addTrees(boardHolder, grid);
        
        //boardHolder.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
        boardHolder.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    private void addFloor(GameObject boardHolder, OIGrid grid) {
        OIGrid floorGrid = grid.replace(TREE, FLOOR); //Metto floor sotto agli alberi
        GameObject floorPrefab = _behavior._floorMeshPrefab;
        GameObject created = istantiate(floorPrefab, 0, 0, 0, boardHolder, "Floor");

        Mesh floorMesh = MeshFactory.createPlane(floorGrid, TILE_SIZE, FLOOR);
        created.GetComponent<MeshFilter>().mesh = floorMesh;
        created.GetComponent<MeshCollider>().sharedMesh = floorMesh;
        created.transform.parent = boardHolder.transform;
    }

    private void addTrees(GameObject boardHolder, OIGrid grid) {
        GameObject treesContainer = new GameObject("Trees");
        treesContainer.transform.parent = boardHolder.transform;

        int highPolyTreeThreshold = _behavior._highPolyTreeChance;
        IPickerStrategy seededStrategy = new CustomSeededPickerStrategy(_behavior._seed);

        ChildInGameObjectPicker lowPolyTreePicker = new ChildInGameObjectPicker(_behavior._lowPolyTreePrefabs, seededStrategy);
        ChildInGameObjectPicker highPolyTreePicker = new ChildInGameObjectPicker(_behavior._highPolyTreePrefabs, seededStrategy);
        IntInRangePicker treeTypeChancePicker = new IntInRangePicker(0, 100, seededStrategy);
        FloatInRangePicker treeScalePicker = new FloatInRangePicker(0.2f, 0.5f, seededStrategy);
        
        float floorSpan = TILE_SIZE;
        grid.forEach2((rowZ, colX, value) => {
            if (value != TREE) return;

            float treeScale = treeScalePicker.draw();
            int treeChance = treeTypeChancePicker.draw();
            GameObject selectedTree;
            if (treeChance < highPolyTreeThreshold) {
                selectedTree = highPolyTreePicker.draw();
            } else {
                selectedTree = lowPolyTreePicker.draw();
            }
            float xPos = colX * floorSpan + 2;
            float zPos = -rowZ * floorSpan - 2;
            float yRot = xPos * zPos * 1000 % 360;
            GameObject created = istantiate(selectedTree, xPos, zPos, yRot, treesContainer, rowZ, colX, "Tree");
            created.transform.localScale = new Vector3(treeScale, treeScale, treeScale);
        });
    }

    public static ForestRenderer newInstance(ForestGeneratorBehaviour behaviour) {
        ForestRenderer renderer = ScriptableObject.CreateInstance<ForestRenderer>();
        renderer.setBehaviour(behaviour);
        return renderer;
    }

    private void setBehaviour(ForestGeneratorBehaviour dungeonGeneratorBehaviour) {
        _behavior = dungeonGeneratorBehaviour;
    }

    private GameObject istantiate(GameObject prefab, float xPos, float zPos, float yRot, GameObject parent, int rowZ, int colX, String name) {
        Vector3 position = new Vector3(xPos, 0, zPos);
        GameObject instance = (GameObject)Instantiate(prefab, position, Quaternion.identity);
        instance.transform.Rotate(0, yRot, 0);
        instance.name = "(" + rowZ + "," + colX + ") " + name;
        instance.transform.parent = parent.transform;
        return instance;
    }

    private GameObject istantiate(GameObject prefab, float xPos, float zPos, float yRot, GameObject parent, String name) {
        Vector3 position = new Vector3(xPos, 0, zPos);
        GameObject instance = (GameObject)Instantiate(prefab, position, Quaternion.identity);
        instance.transform.Rotate(0, yRot, 0);
        instance.name = name;
        instance.transform.parent = parent.transform;
        return instance;
    }

}
