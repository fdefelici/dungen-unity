using NUnit.Framework;

using DungeonGeneration.Generator.Domain;
using DungeonGeneration.Logging;
using DungeonGeneration.Generator.Pickers;

public class OIGridTest {

    [Test]
    public void rotate90_grid4x3() {
        OIGrid grid = new OIGrid(2, 3);
        grid[0, 0] = 1;
        grid[0, 1] = 1;
        grid[0, 2] = 1;
        grid[1, 0] = 0;
        grid[1, 1] = 0;
        grid[1, 2] = 0;

        OIGrid expected = new OIGrid(3, 2);
        expected[0, 0] = 0;
        expected[0, 1] = 1;
        expected[1, 0] = 0;
        expected[1, 1] = 1;
        expected[2, 0] = 0;
        expected[2, 1] = 1;

        OIGrid rotated = grid.rotate90();
        Assert.AreEqual(expected, rotated);
    }

}
