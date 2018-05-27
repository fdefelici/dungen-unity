namespace DungeonGeneration.Generator.Plotters {
    public interface IDungeonBoardPlotter {
        void applyOnRoom(Room room, int[,] map);
        void applyOnCorridor(Corridor corridor, int[,] map);
    }
}