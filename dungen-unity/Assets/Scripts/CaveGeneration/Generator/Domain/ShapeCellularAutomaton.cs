using System;
using DungeonGeneration.Generator.Pickers;
using DungeonGeneration.Logging;


public class ShapeCellularAutomaton {
    private int randomFillPercent;
    private int seed;
    private int smoothSteps;
    private CustomSeededPickerStrategy intInRangePicker;
    private IXLogger _logger;

    //private System.Random intInRangePicker;

    public ShapeCellularAutomaton(int seed, int randomFillPercent, int smoothSteps) {
        this.seed = seed;
        this.randomFillPercent = randomFillPercent;
        this.smoothSteps = smoothSteps;

        //this.intInRangePicker = new System.Random(seed.GetHashCode());

        this.intInRangePicker = new CustomSeededPickerStrategy(seed); 
        
    }

    public void setLogger(IXLogger logger) {
        _logger = logger;
        this.intInRangePicker.setLogger(_logger);
    }

    public void applyOn(IXShape shape) {
        shape.forEachCell(RandomFillMap);
        for (int i = 0; i < smoothSteps; i++) {
          SmoothMap(shape);
        }
        
    }

    private void RandomFillMap(int row, int col, IXShape shape) {
        int value = (intInRangePicker.drawBetween(0, 100) < randomFillPercent) ? XTile.FLOOR : XTile.WALL;
        //int value = (intInRangePicker.Next(0, 100) < randomFillPercent) ? XTile.FLOOR : XTile.WALL;

        shape.setCellValue(row, col, value);
    }

    private void SmoothMap(IXShape shape) {
        shape.forEachCell((row, col, sameShape) => {
            int neighbourWallTiles = GetSurroundingWallCount(row, col, shape);
            if (neighbourWallTiles > 4) shape.setCellValue(row, col, XTile.WALL);
            else if (neighbourWallTiles < 4) shape.setCellValue(row, col, XTile.FLOOR);
        });
    }

    private int GetSurroundingWallCount(int row, int col, IXShape shape) {
        int wallCount = 0;
        for (int neighbourX = row - 1; neighbourX <= row + 1; neighbourX++) {
            for (int neighbourY = col - 1; neighbourY <= col + 1; neighbourY++) {
                if (shape.isCellValid(neighbourX, neighbourY)) {
                    if (neighbourX != row || neighbourY != col) {
                        if (shape.hasCellValue(neighbourX, neighbourY, XTile.WALL)) wallCount++;
                    }
                } else {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }
}