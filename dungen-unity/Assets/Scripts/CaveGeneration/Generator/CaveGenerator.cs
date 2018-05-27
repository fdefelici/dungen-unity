using DungeonGeneration.Generator.Domain;
using DungeonGeneration.Generator;
using DungeonGeneration.Generator.Pickers;
using System.Collections.Generic;
using System;
using DungeonGeneration.Logging;

namespace CaveGeneration.Generator {

    public class CaveGenerator {
        private DungeonGenerator _dunGen;

        private int _cellularFillChance;
        private int _cellularSmoothingStep;
        private int _seed;
        private IXLogger _logger;
        protected ICaveBoardPlotter<int[,]> _plotter;
        private int _mapMargin;

        public CaveGenerator() {
            //_dunGen = new DungeonGenerator();
            _dunGen = new ForcedDungeonGenerator(10);
            _seed = 0;
            _logger = new NullLogger();
            _cellularFillChance = 50;
            _cellularSmoothingStep = 5;
            setMapMargin(1);
        }

        private void checkConstraints() {
            if (_mapMargin < 1) throw new FormatException("Invalid Map Margin: must be >= 1");
        }


        public void setPlotter(ICaveBoardPlotter<int[,]> plotter) {
            _plotter = plotter;
        }

        public void setLogger(IXLogger logger) {
            _logger = logger;
            _dunGen.setLogger(logger);
        }

        public void setCellularFillChance(int percentage) {
            _cellularFillChance = percentage;
        }

        public void setCellularSmoothingSteps(int steps) {
            _cellularSmoothingStep = steps;
        }

        public void setSeed(int seed) {
            _seed = seed;
            _dunGen.setSeed(seed);
        }

        public void setCorridorWidthRange(int min, int max) {
            _dunGen.setCorridorWidthRange(min, max);
        }

        public void setCorridorLengthRange(int min, int max) {
            _dunGen.setCorridorLengthRange(min, max);
        }

        public void setRoomSizeRange(int min, int max) {
            _dunGen.setRoomSizeRange(min, max);
        }

        public void setRoomsNumberRange(int min, int max) {
            _dunGen.setRoomsNumberRange(min, max);
        }

        public void setMapSize(int height, int width) {
            _dunGen.setMapSize(height, width);
        }

        public CaveBoard asBoard() {
            checkConstraints();
            
            Board board = _dunGen.asBoard();
            ShapeCellularAutomaton roomAlgo = new ShapeCellularAutomaton(_seed, _cellularFillChance, _cellularSmoothingStep);
            CustomSeededPickerStrategy shapePicker = new CustomSeededPickerStrategy(_seed);

            CaveBoard result = new CaveBoard(board.rows(), board.cols());
            List<IXShape> onlyRooms = new List<IXShape>();

            _logger.info("Rooms: " + board.rooms().Length);
            foreach (Room each in board.rooms()) {
                Cell leftVert = each.topLeftVertex();
                int rows = each.height();
                int cols = each.width();
                APolyShape currentRoom; //Select a shape for the Room
                if (shapePicker.drawBetween(0, 100) < 50) {
                    currentRoom = new RectShape(leftVert, new OIGrid(rows, cols));
                } else {
                    currentRoom = new ElliShape(leftVert, new OIGrid(rows, cols));
                }
                _logger.info("Shape type: " + currentRoom.GetType());
                roomAlgo.applyOn(currentRoom);

                _logger.info("Shape regions before clean: " + currentRoom.regionsNumber());
                if (!currentRoom.hasRegions()) {
                    _logger.warning("No Region found. Room will be skipped!!!");
                    continue;
                }
                currentRoom.deleteRegionsButTheBiggest();
                _logger.info("Shape regions after clean: " + currentRoom.regionsNumber());

                
                onlyRooms.Add(currentRoom);
                if (onlyRooms.Count > 1) {
                    IXShape previousRoom = onlyRooms[onlyRooms.Count - 2];
                    int corrIndex = onlyRooms.Count - 2;
                    Corridor corr = board.corridors()[corrIndex];
                    int corridorSection = corr.isVertical() ? corr.width() : corr.height();
                    result.addCorridor(CaveCorridorFactory.createCorrShape(previousRoom, currentRoom, corridorSection));
                }
                result.addRoom(currentRoom);

            }
            return result;
        }

        public void setMapCropEnabled(bool enabled) {
            _dunGen.setMapCropEnabled(enabled);
        }

        public void setMapMargin(int mapMargin) {
            _mapMargin = mapMargin;
            _dunGen.setMapMargin(mapMargin);
        }
   
        public OIGrid asOIGrid() {
            return new OIGrid(asMatrix());
        }

        public int[,] asMatrix() {
            _plotter.applyOn(asBoard());
            return _plotter.result();
        }

        
    }
}

