using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class XTriangulator2 {

    private float _squareSize;
    private SquareGrid squareGrid;
    
    private List<Vector3> vertices;
    private List<int> triangles;

    private Dictionary<int, List<Triangle>> triangleDictionary = new Dictionary<int, List<Triangle>>();
    private List<List<int>> outlines = new List<List<int>>();
    private HashSet<int> checkedVertices = new HashSet<int>();
    private bool _zeroCentered;
    private int _wallValue;

    public XTriangulator2() {
        _squareSize = 1;
        _zeroCentered = true;
        _wallValue = 0;
    }

    public void setSquareSize(int squareSize) {
        _squareSize = squareSize;
    }

    public void setZeroBased() {
        _zeroCentered = false;
    }
    public void setZeroCentered() {
        _zeroCentered = true;
    }
    public void setCellValueToRender(int wallValue) {
        _wallValue = wallValue;
    }

    public Mesh triangolate(int[,] map) {
        Vector3 center = new Vector3();
        if (_zeroCentered) {
            center = Vector3.zero;
        } else {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            int rowFix = rows % 2 == 0 ? 2 : 1;
            int colFix = cols % 2 == 0 ? 2 : 1;
            float posX = cols/2 * _squareSize + _squareSize/colFix;
            float posZ = -rows/2 * _squareSize - _squareSize/rowFix;
            center = new Vector3(posX, 0f, posZ);
        }

        triangleDictionary.Clear();
        outlines.Clear();
        checkedVertices.Clear();

        squareGrid = new SquareGrid(map, _squareSize, center, _wallValue);

        vertices = new List<Vector3>();
        triangles = new List<int>();

        for (int x = 0; x < squareGrid.squares.GetLength(0); x++) {
            for (int y = 0; y < squareGrid.squares.GetLength(1); y++) {
                TriangulateSquare(squareGrid.squares[x, y]);
            }
        }

        Mesh mapMesh = new Mesh();
        mapMesh.vertices = vertices.ToArray();
        mapMesh.triangles = triangles.ToArray();
        //mapMesh.RecalculateNormals();
        //mapMesh.RecalculateBounds();
        return mapMesh;
    }


    public Mesh triangolate(OIGrid grid) {
        int[,] map = grid.asMatrix();
        return triangolate(map);
    }

    void TriangulateSquare(Square square) {
        switch (square.configuration) {
            case 0:
                break;

            // 1 points:
            case 1:
                MeshFromPoints(square.centreLeft, square.centreBottom, square.bottomLeft);
                break;
            case 2:
                MeshFromPoints(square.bottomRight, square.centreBottom, square.centreRight);
                break;
            case 4:
                MeshFromPoints(square.topRight, square.centreRight, square.centreTop);
                break;
            case 8:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreLeft);
                break;

            // 2 points:
            case 3:
                MeshFromPoints(square.centreRight, square.bottomRight, square.bottomLeft, square.centreLeft);
                break;
            case 6:
                MeshFromPoints(square.centreTop, square.topRight, square.bottomRight, square.centreBottom);
                break;
            case 9:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreBottom, square.bottomLeft);
                break;
            case 12:
                MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreLeft);
                break;
            case 5:
                MeshFromPoints(square.centreTop, square.topRight, square.centreRight, square.centreBottom, square.bottomLeft, square.centreLeft);
                break;
            case 10:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.bottomRight, square.centreBottom, square.centreLeft);
                break;

            // 3 point:
            case 7:
                MeshFromPoints(square.centreTop, square.topRight, square.bottomRight, square.bottomLeft, square.centreLeft);
                break;
            case 11:
                MeshFromPoints(square.topLeft, square.centreTop, square.centreRight, square.bottomRight, square.bottomLeft);
                break;
            case 13:
                MeshFromPoints(square.topLeft, square.topRight, square.centreRight, square.centreBottom, square.bottomLeft);
                break;
            case 14:
                MeshFromPoints(square.topLeft, square.topRight, square.bottomRight, square.centreBottom, square.centreLeft);
                break;

            // 4 point:
            case 15:
                MeshFromPoints(square.topLeft, square.topRight, square.bottomRight, square.bottomLeft);
                checkedVertices.Add(square.topLeft.vertexIndex);
                checkedVertices.Add(square.topRight.vertexIndex);
                checkedVertices.Add(square.bottomRight.vertexIndex);
                checkedVertices.Add(square.bottomLeft.vertexIndex);
                break;
        }

    }

    void MeshFromPoints(params Node[] points) {
        AssignVertices(points);

        if (points.Length >= 3)
            CreateTriangle(points[0], points[1], points[2]);
        if (points.Length >= 4)
            CreateTriangle(points[0], points[2], points[3]);
        if (points.Length >= 5)
            CreateTriangle(points[0], points[3], points[4]);
        if (points.Length >= 6)
            CreateTriangle(points[0], points[4], points[5]);

    }

    void AssignVertices(Node[] points) {
        for (int i = 0; i < points.Length; i++) {
            if (points[i].vertexIndex == -1) {
                points[i].vertexIndex = vertices.Count;
                vertices.Add(points[i].position);
            }
        }
    }

    void CreateTriangle(Node a, Node b, Node c) {
        triangles.Add(a.vertexIndex);
        triangles.Add(b.vertexIndex);
        triangles.Add(c.vertexIndex);

        Triangle triangle = new Triangle(a.vertexIndex, b.vertexIndex, c.vertexIndex);
        AddTriangleToDictionary(triangle.vertexIndexA, triangle);
        AddTriangleToDictionary(triangle.vertexIndexB, triangle);
        AddTriangleToDictionary(triangle.vertexIndexC, triangle);
    }

    void AddTriangleToDictionary(int vertexIndexKey, Triangle triangle) {
        if (triangleDictionary.ContainsKey(vertexIndexKey)) {
            triangleDictionary[vertexIndexKey].Add(triangle);
        } else {
            List<Triangle> triangleList = new List<Triangle>();
            triangleList.Add(triangle);
            triangleDictionary.Add(vertexIndexKey, triangleList);
        }
    }

    struct Triangle {
        public int vertexIndexA;
        public int vertexIndexB;
        public int vertexIndexC;
        int[] vertices;

        public Triangle(int a, int b, int c) {
            vertexIndexA = a;
            vertexIndexB = b;
            vertexIndexC = c;

            vertices = new int[3];
            vertices[0] = a;
            vertices[1] = b;
            vertices[2] = c;
        }

        public int this[int i] {
            get {
                return vertices[i];
            }
        }


        public bool Contains(int vertexIndex) {
            return vertexIndex == vertexIndexA || vertexIndex == vertexIndexB || vertexIndex == vertexIndexC;
        }
    }

    public class SquareGrid {
        public Square[,] squares;
        private int _wallValue;

        public SquareGrid(int[,] map, float squareSize, Vector3 center, int cellValueToRender) {
            int rows = map.GetLength(0);
            int cols = map.GetLength(1);
            float mapWidth = cols * squareSize;
            float mapHeight = rows * squareSize;
            
            ControlNode[,] controlNodes = new ControlNode[rows, cols];

            for (int row = 0; row < rows; row++) {
                for (int col = 0; col < cols; col++) {
                    //float posX = (-mapWidth / 2 + center.x) + (row * squareSize + squareSize / 2);
                    float posX = (-mapWidth / 2 + center.x) + (col * squareSize + squareSize / 2);
                    float posY = 0;
                    //float posZ = (-mapHeight / 2 + center.y) + (col * squareSize + squareSize / 2);
                    float posZ = (-mapHeight / 2 + center.z) + (row * squareSize + squareSize / 2);

                    Vector3 pos = new Vector3(posX, posY, posZ);
                    controlNodes[row, col] = new ControlNode(pos, map[row, col] == cellValueToRender, squareSize);
                }
            }

            squares = new Square[rows - 1, cols - 1];
            for (int row = 0; row < rows - 1; row++) {
                for (int col = 0; col < cols - 1; col++) {
                    //squares[row, col] = new Square(controlNodes[row, col + 1], controlNodes[row + 1, col + 1], controlNodes[row + 1, col], controlNodes[row, col]);
                    squares[row, col] = new Square(controlNodes[row, col], controlNodes[row+1, col], controlNodes[row+1, col+1], controlNodes[row, col+1]);    
                }
            }

        }
    }
    public class ControlNode : Node {

        public bool active;
        public Node above, right;

        public ControlNode(Vector3 _pos, bool _active, float squareSize) : base(_pos) {
            active = _active;
            above = new Node(position + Vector3.left * squareSize / 2f);
            right = new Node(position + Vector3.forward * squareSize / 2f);
        }

    }

    public class Square {

        public ControlNode topLeft, topRight, bottomRight, bottomLeft;
        public Node centreTop, centreRight, centreBottom, centreLeft;
        public int configuration;

        public Square(ControlNode _topLeft, ControlNode _topRight, ControlNode _bottomRight, ControlNode _bottomLeft) {
            topLeft = _topLeft;
            topRight = _topRight;
            bottomRight = _bottomRight;
            bottomLeft = _bottomLeft;

            centreTop = topLeft.right;
            centreRight = bottomRight.above;
            centreBottom = bottomLeft.right;
            centreLeft = bottomLeft.above;

            if (topLeft.active)
                configuration += 8;
            if (topRight.active)
                configuration += 4;
            if (bottomRight.active)
                configuration += 2;
            if (bottomLeft.active)
                configuration += 1;
        }

    }

    public class Node {
        public Vector3 position;
        public int vertexIndex = -1;

        public Node(Vector3 _pos) {
            position = _pos;
        }
    }

   
}