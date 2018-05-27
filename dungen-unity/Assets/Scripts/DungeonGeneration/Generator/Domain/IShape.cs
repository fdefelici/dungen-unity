using DungeonGeneration.Generator.Plotters;
namespace DungeonGeneration.Generator.Domain {
    public interface IShape {
        bool isWithin(Grid mapGrid);
        bool collidesWith(IShape each);
        bool containsCell(Cell _topLeftVertex);
        void plotOn(int[,] map, IDungeonBoardPlotter plotter);
    }
}