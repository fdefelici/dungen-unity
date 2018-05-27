using System;
using DungeonGeneration.Generator.Domain;
using DungeonGeneration.Generator.Plotters;

namespace DungeonGeneration.Generator.Samples {
    public class XDungeonSampleCases {
        public static void case_RoomWithRightSideCorridorHorizontal_plot(int[,] map) {
            Room room = new Room(new Cell(0, 0), new Grid(5, 5));
            Corridor corr = new Corridor(new Cell(0, 4), new Grid(5, 3), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            room.plotOn(map, new DetailedTilesPlotter());
        }

        public static void case_RoomWithBottomRightCorridorVertical_plot(int[,] map) {
            Room room = new Room(new Cell(0, 0), new Grid(5, 8));
            Corridor corr = new Corridor(new Cell(4, 5), new Grid(4, 3), Corridor.Orientation.vertical);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            room.plotOn(map, new DetailedTilesPlotter());
        }

        public static void case_RoomWithBottomRightCorridorHorizontal_plot(int[,] map) {
            Room room = new Room(new Cell(0, 0), new Grid(5, 8));
            Corridor corr = new Corridor(new Cell(2, 7), new Grid(3, 4), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            room.plotOn(map, new DetailedTilesPlotter());
        }

        public static void case_RoomWithTopRightCorridorHorizontal_plot(int[,] map) {
            Room room = new Room(new Cell(0, 0), new Grid(5, 8));
            Corridor corr = new Corridor(new Cell(0, 7), new Grid(3, 4), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            room.plotOn(map, new DetailedTilesPlotter());
        }

        public static void case_RoomWithTopRightCorridorVertical_plot(int[,] map) {
            Room room = new Room(new Cell(5, 0), new Grid(5, 8));
            Corridor corr = new Corridor(new Cell(2, 5), new Grid(4, 3), Corridor.Orientation.vertical);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            room.plotOn(map, new DetailedTilesPlotter());
        }

        public static void case_RoomWithBottomLeftCorridorVertical_plot(int[,] map) {
            Room room0 = new Room(new Cell(0, 0), new Grid(5, 8));
            Corridor corr0_down = new Corridor(new Cell(4, 0), new Grid(4, 3), Corridor.Orientation.vertical);
            room0.setCorridorOutcoming(corr0_down);
            corr0_down.setSourceRoom(room0);

            room0.plotOn(map, new DetailedTilesPlotter());
        }

        public static void case_RoomWithBottomLeftCorridorHorizontal_plot(int[,] map) {
            Room room = new Room(new Cell(0, 8), new Grid(5, 8));
            Corridor corr = new Corridor(new Cell(2, 5), new Grid(3, 4), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            room.plotOn(map, new DetailedTilesPlotter());
        }

        public static void case_RoomWithTopLeftCorridorHorizontal_plot(int[,] map) {
            Room room = new Room(new Cell(0, 8), new Grid(5, 8));
            Corridor corr = new Corridor(new Cell(0, 5), new Grid(3, 4), Corridor.Orientation.horizontal);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            room.plotOn(map, new DetailedTilesPlotter());
        }

        public static void case_RoomWithTopLeftCorridorVertical_plot(int[,] map) {
            Room room = new Room(new Cell(3, 0), new Grid(5, 8));
            Corridor corr = new Corridor(new Cell(0, 0), new Grid(4, 3), Corridor.Orientation.vertical);
            room.setCorridorOutcoming(corr);
            corr.setSourceRoom(room);

            room.plotOn(map, new DetailedTilesPlotter());
        }
    }
}

