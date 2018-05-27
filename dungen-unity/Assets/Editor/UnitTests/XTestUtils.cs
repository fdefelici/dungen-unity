using System;

public class XTestUtils {

    public static void printForTest(CaveBoard board) {
        ZeroOneTwoFillerCavePlotter plotter = new ZeroOneTwoFillerCavePlotter();
        plotter.applyOn(board);
        print(plotter.result(), true);

        String currName = null;
        String prevName = null;
        for (int i = 0; i < board.all().Length; i++) {
            IXShape current = board.all()[i];
            prevName = currName;
            if (i % 2 == 0) {
                int roomIndex = i / 2 + 1;
                currName = "room" + (roomIndex);
            } else {
                currName = "corr" + i + "" + (i + 1);
            }
            current.accept(new PrepareForTestShapeVisitor(currName));

            if (prevName != null) {
                System.Console.WriteLine(currName + ".setIncoming(" + prevName + ");");
                System.Console.WriteLine(prevName + ".setOutcoming(" + currName + ");");
            }
        }
    }

    public static void printForTest(int seed, params APolyShape[] rooms) {
        ShapeCellularAutomaton auto = new ShapeCellularAutomaton(seed, 75, 4);
        CaveBoard board = new CaveBoard(50, 50);
        for(int i=0; i < rooms.Length; i++) {
            APolyShape currRoom = rooms[i];
            auto.applyOn(currRoom);
            currRoom.deleteRegionsButTheBiggest();

            if (i > 0) {
                IXShape prevRoom = rooms[i - 1];
                IXShape corr = CaveCorridorFactory.createCorrShape(prevRoom, currRoom, 2);
                board.addCorridor(corr);
            }
            board.addRoom(currRoom);
        }

        printForTest(board);
    }

    public static bool areEquals(int[,] expected, int[,] result) {
        if (expected.GetLength(0) != result.GetLength(0)) return false;
        if (expected.GetLength(1) != result.GetLength(1)) return false;
        for (int i = 0; i < expected.GetLength(0); i++) {
            for (int j = 0; j < expected.GetLength(1); j++) {
                if (expected[i, j] != result[i, j]) return false;
            }
        }
        return true;
    }

    public static bool areEquals(Object[] expected, Object[] result) {
        if (expected.GetLength(0) != result.GetLength(0)) return false;
        for(int i=0; i<expected.GetLength(0); i++) {
            if (!expected[i].Equals(result[i])) return false;
        }
        return true;
    }

    public static void print(int[,] matrix) {
        print(matrix, false);
    }

    public static void print(int[,] matrix, bool showIndexes) {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);

        String result = rows + " x " + cols + "\n";
        if (showIndexes) {
            result += "\n";
            for (int n = 0; n < cols; n++) {
                if (n == 0) result += "     ";
                result += n + ",";
                if (n < 9) result += " ";
            }
        }
        result += "\n";
        for (int i = 0; i < rows; i++) {
            if (showIndexes && i<10) result += " " + i + " ";
            if (showIndexes && i >= 10) result += i + " ";

            for (int j = 0; j < cols; j++) {
                if (j == 0) result += " {";
                result += matrix[i, j];
                if (j != cols - 1) 
                    result += ", ";
            }
            result += "}";
            if (showIndexes) result += " " + i;
            if (i != rows - 1) result += ",\n";

        }
        if (showIndexes) {
            result += "\n";
            for (int m = 0; m<cols; m++) {
                if (m == 0) result += "     ";
                result += m + ",";
                if (m < 9) result += " ";
            }
        }
        Console.WriteLine(result);
    }

    public static void print(Object[] list) {
        int size = list.GetLength(0);

        String result = "[" + size + "] ";

        for (int i = 0; i < size; i++) {
            if (i == 0) result += " {";
            result += list[i].ToString();
            if (i != size - 1) result += ", ";
        }
        result += "}";
        Console.WriteLine(result);
    }
}