using UnityEngine;


namespace PlatformerMVC
{
    public class SquareGrid
    {
        public Square[,] Squares;
        public ControlNode[,] ControlNodes;


        private int nodeCountX;
        private int nodeCountY;

        private float mapWidth;
        private float mapHeight;

        private float squareCenter;
        private float mapCenterWidth;
        private float mapCenterHeight;


        public SquareGrid(int[,] map, float squareSize)
        {
            nodeCountX = map.GetLength(0);
            nodeCountY = map.GetLength(1);

            mapWidth = (nodeCountX - 1) * squareSize;
            mapHeight = (nodeCountY - 1) * squareSize;

            squareCenter = squareSize / 2;
            mapCenterWidth = -mapWidth / 2;
            mapCenterHeight = -mapHeight / 2;

            ControlNodes = new ControlNode[nodeCountX, nodeCountY];

            for (int x = 0; x < nodeCountX; x++)
            {
                for (int y = 0; y < nodeCountY; y++)
                {
                    Vector3 pos = new Vector3(x * squareSize + mapCenterWidth + squareCenter, y * squareSize + mapCenterWidth + squareCenter, 0);
                    ControlNodes[x, y] = new ControlNode(pos, map[x, y] == 1);
                }
            }

            Squares = new Square[nodeCountX - 1, nodeCountY - 1];

            for (int x = 0; x < Squares.GetLength(0); x++)
            {
                for (int y = 0; y < Squares.GetLength(1); y++)
                {
                    Squares[x, y] = new Square(ControlNodes[x, y], ControlNodes[x + 1, y], ControlNodes[x + 1, y + 1], ControlNodes[x, y + 1]);
                }
            }
        }
    }
}
