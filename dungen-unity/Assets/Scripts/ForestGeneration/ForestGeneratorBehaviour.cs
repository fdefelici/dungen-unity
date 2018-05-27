using UnityEngine;
using DungeonGeneration.Logging;
using CaveGeneration.Generator;

public class ForestGeneratorBehaviour : MonoBehaviour {

    public GameObject _lowPolyTreePrefabs;
    public GameObject _highPolyTreePrefabs;
    public GameObject _floorMeshPrefab;
    [Range(0, 100)]
    public int _highPolyTreeChance = 20;
    [Range(1, 10)]
    public int _treesTickness = 5;

    [Range(50, 300)]
    public int _mapMaxWidth = 200;
    [Range(50, 300)]
    public int _mapMaxHeight = 200;
    [Range(1, 10)]
    public int _mapInnerMargin = 4;
    public bool _mapCropEnabled = true;

    [Range(50, 100)]
    public int _cellularFillChance = 58;
    [Range(0, 10)]
    public int _cellularSmoothSteps = 5;

    [Range(0, 20)]
    public int _roomsNumberMin = 5;
    [Range(0, 20)]
    public int _roomsNumberMax = 9;

    [Range(20, 100)]
    public int _roomSizeMin = 30;
    [Range(20, 100)]
    public int _roomSizeMax = 40;

    [Range(1, 20)]
    public int _corridorLengthMin = 10;
    [Range(1, 20)]
    public int _corridorLengthMax = 15;
    [Range(1, 20)]
    public int _corridorWidthMin = 20;
    [Range(1, 20)]
    public int _corridorWidthMax = 25;

    public int _seed = 48;
    public bool _randomSeed = false;

    private CaveGenerator _generator;
    private ForestRenderer _renderer;

    void Awake() {
        _generator = new CaveGenerator();
        _renderer = ForestRenderer.newInstance(this);
    }

    void Start () {
        generate();
    }
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            generate();
        }
	}

    private void generate() {  
        _generator.setMapSize(_mapMaxHeight, _mapMaxWidth);
        _generator.setRoomsNumberRange(_roomsNumberMin, _roomsNumberMax);
        _generator.setRoomSizeRange(_roomSizeMin, _roomSizeMax);
        _generator.setCorridorLengthRange(_corridorLengthMin, _corridorLengthMax);
        _generator.setCorridorWidthRange(_corridorWidthMin, _corridorWidthMax);
        //_generator.setLogger(new UnityEngineLogger());
        if (_randomSeed) {
            _seed = Time.time.ToString().GetHashCode();
        }
        _generator.setSeed(_seed);

        _generator.setCellularFillChance(_cellularFillChance);
        _generator.setCellularSmoothingSteps(_cellularSmoothSteps);

        _generator.setMapMargin(_mapInnerMargin);
        _generator.setMapCropEnabled(_mapCropEnabled);

        _generator.setPlotter(new Forest012Plotter(_treesTickness));

        if (_generator.asBoard().isEmpty()) {            
            //_floorMeshFilter.mesh = null;
            //_floorMeshFilter.gameObject.GetComponent<MeshCollider>().sharedMesh = null;
            return;
        }
        _renderer.render(_generator.asOIGrid());
    }
}