using NUnit.Framework;
using System;
using DungeonGeneration.Logging;
using DungeonGeneration.Generator.Plotters;

namespace DungeonGeneration.Generator {
    public class TilesMapGeneratorTest {
        [Test]
        public void scenario1_withDetailedTilesPlotter() {
            DungeonGenerator generator = new DungeonGenerator();
            generator.setMapSize(15, 15);
            generator.setRoomsNumberRange(2, 2);
            generator.setRoomSizeRange(5, 7);
            generator.setCorridorLengthRange(2, 4);
            generator.setSeed(1234567);
            generator.setPlotter(new DetailedTilesPlotter());
            //generator.setLogger(new ConsoleLogger());

            int[,] expected = {  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 6, 0, 2, 2, 0, 7, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 5, 1, 1, 1, 1, 3, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 5, 1, 1, 1, 1, 3, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 3, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 9, 0, 4, 11, 1, 3, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 5, 1, 3, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 5, 1, 13, 2, 0, 7, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 5, 1, 1, 1, 1, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 5, 1, 1, 1, 1, 3, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 9, 0, 4, 4, 0, 8, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
            Assert.AreEqual(expected, generator.asMatrix());
        }

        [Test]
        public void scenario2_withDetailedTilesPlotter() {
            DungeonGenerator generator = new DungeonGenerator();
            generator.setMapSize(15, 15);
            generator.setRoomsNumberRange(2, 2);
            generator.setRoomSizeRange(5, 7);
            generator.setCorridorLengthRange(2, 4);
            generator.setSeed(-1910733923);
            generator.setPlotter(new DetailedTilesPlotter());

            int[,] expected = {  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 6, 0, 2, 0, 7, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 5, 1, 1, 1, 3, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 1, 1, 1, 3, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 9, 0, 11, 1, 3, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 5, 1, 13, 0, 7, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 5, 1, 1, 1, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 5, 1, 1, 1, 3, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 9, 0, 4, 0, 8, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
            Assert.AreEqual(expected, generator.asMatrix());
        }

        [Test]
        public void scenario1_withZeroOneTilesPlotter() {
            DungeonGenerator generator = new DungeonGenerator();
            generator.setMapSize(15, 15);
            generator.setRoomsNumberRange(2, 2);
            generator.setRoomSizeRange(5, 7);
            generator.setCorridorLengthRange(2, 4);
            generator.setSeed(1234567);
            generator.setPlotter(new ZeroOneTilesPlotter());
            //generator.setLogger(new ConsoleLogger());

            int[,] expected = {  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
            Assert.AreEqual(expected, generator.asMatrix());
        }

        [Test]
        public void scenario2_withZeroOneTilesPlotter() {
            DungeonGenerator generator = new DungeonGenerator();
            generator.setMapSize(15, 15);
            generator.setRoomsNumberRange(2, 2);
            generator.setRoomSizeRange(5, 7);
            generator.setCorridorLengthRange(2, 4);
            generator.setSeed(-1910733923);
            generator.setPlotter(new ZeroOneTilesPlotter());

            int[,] expected = {  {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                                 {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};
           Assert.AreEqual(expected, generator.asMatrix());
        }
    }
}
