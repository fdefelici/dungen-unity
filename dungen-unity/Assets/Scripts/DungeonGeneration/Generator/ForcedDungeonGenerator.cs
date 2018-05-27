using DungeonGeneration.Generator.Domain;
using DungeonGeneration.Generator.Pickers;
using DungeonGeneration.Logging;
using DungeonGeneration.Generator.Plotters;
using System;

namespace DungeonGeneration.Generator {

    /*
     * Considering the modification (Try to force Min Room Number) 
     * is more quick doing subclassing. 
     * Anyway, It should be better to convert sublclassing DungeonGeneator
     * with Strategy Pattern. To take into account for further DungeonGeneration
     * customization.
     * 
     */
    public class ForcedDungeonGenerator : DungeonGenerator {
        private int _maxAttempts;

        public ForcedDungeonGenerator(int maxAttempts) 
            : base() {
            _maxAttempts = maxAttempts;
        }

        
        public override Board asBoard() {
            if (!base.isBoardCleared()) return base.asBoard();

            Board bestBoard = null;
            int count = 1;
            while (count < _maxAttempts) {
                base.getLogger().info("Board generation attempt: " + count+"/"+_maxAttempts);

                Board board = base.asBoard();
                if (bestBoard == null) bestBoard = board;
                else if (board.roomSize() > bestBoard.roomSize()) bestBoard = board;

                if (bestBoard.roomSize() >= base.getMinRoomSize()) break;
               
                int seed = base.getSeed();
                if (seed >= 0) seed++;
                else seed--;
                base.setSeed(seed);

                count++;
            }
            base.getLogger().info("Board generation completed at attempt: " + count);
            base.setBoard(bestBoard);
            return bestBoard;
        }
        
    }
}
