using NUnit.Framework;

using DungeonGeneration.Generator.Domain;
using CaveGeneration.Generator;

public class ForestGenerationTest {

    [Test]
    public void forestgen_allSequence() {
        CaveGenerator gen = new CaveGenerator();
        gen.setMapSize(40, 40);
        gen.setRoomsNumberRange(3, 3);
        gen.setRoomSizeRange(12, 15);
        gen.setCorridorLengthRange(5, 7);
        gen.setCorridorWidthRange(1, 3);
        gen.setSeed(123456);
        gen.setCellularFillChance(80);
        gen.setCellularSmoothingSteps(5);
        gen.setMapMargin(2);
        gen.setPlotter(new Forest012Plotter());

        CaveBoard board = gen.asBoard();
        Assert.AreEqual(5, board.all().Length);
        Assert.AreEqual("RectShape", board.all()[0].GetType().ToString());
        Assert.AreEqual("FreeShape", board.all()[1].GetType().ToString());
        Assert.AreEqual("RectShape", board.all()[2].GetType().ToString());
        Assert.AreEqual("FreeShape", board.all()[3].GetType().ToString());
        Assert.AreEqual("ElliShape", board.all()[4].GetType().ToString());


    }
    
}