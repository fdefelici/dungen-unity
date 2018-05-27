using UnityEngine;
using DungeonGeneration.Generator;
using DungeonGeneration.Renderer;
using DungeonGeneration.Logging;
using DungeonGeneration.Generator.Plotters;

namespace DungeonGeneration { 

    public class DungeonGeneratorBehaviour : MonoBehaviour {
        public int _mapHeight = 100;
        public int _mapWidth = 100;
        public int _roomsNumberMin = 5;
        public int _roomsNumberMax = 15;
        public int _roomSizeMin = 5;
        public int _roomSizeMax = 20;
        public int _corridorLengthMin = 2;
        public int _corridorLengthMax = 7;
        public int _corridorWidthMin = 3;
        public int _corridorWidthMax = 3;
        public int _seed = 123456;

        public bool _devMode = false;
        public bool _devLog = false;
        public bool _randomSeed = false;

        public GameObject _floorPrefab;
        public GameObject _wallPrefab;
        public GameObject _wallSeparatorPrefab;
        public GameObject _cornerInnPrefab;
        public GameObject _cornerOutPrefab;

        [SerializeField] private int[,] _tilesMap;
        private DungeonGenerator _generator;
        private BRDungeonRenderer _renderer;

        void Awake() {
            //_generator = new DungeonGenerator();
            _generator = new ForcedDungeonGenerator(10);
            _renderer = BRDungeonRenderer.newInstance(this);
        }

        void Start() {
            if (_devMode) {
                devMode();
                return;
            }
        }

        private void devMode() {
            if (_randomSeed) {
                _seed = Time.time.ToString().GetHashCode();
            }
            generateDungeon();
        }
        
        void Update() {
            if (_devMode && Input.GetMouseButtonDown(0)) {
                devMode();
            }
        }
        
        private void generateDungeon() {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = Vector3.one;

            _generator.setMapSize(_mapHeight, _mapWidth);
            _generator.setRoomsNumberRange(_roomsNumberMin, _roomsNumberMax);
            _generator.setRoomSizeRange(_roomSizeMin, _roomSizeMax);
            _generator.setCorridorLengthRange(_corridorLengthMin, _corridorLengthMax);
            _generator.setCorridorWidthRange(_corridorWidthMin, _corridorWidthMax);
            _generator.setPlotter(new DetailedTilesPlotter());
            if (_devLog) _generator.setLogger(new UnityEngineLogger());
            _generator.setSeed(_seed);

            _tilesMap = _generator.asMatrix();
            _renderer.convertToMeshes(_tilesMap);

            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.localScale = new Vector3(-1, 1, -1);
            transform.position = new Vector3(0.5f, 0, 0.5f);
        }
    }
}