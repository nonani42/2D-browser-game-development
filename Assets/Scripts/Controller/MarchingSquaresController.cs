using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace PlatformerMVC
{
    public class MarchingSquaresController
    {
        private Tilemap _tilemap;
        private Tile _tile;
        private SquareGrid _grid;

        public void GenerateGrid(int[,] map, float squareSize)
        {
            _grid = new SquareGrid(map, squareSize);
        }

        private void DrawTile(bool active, Vector3 position)
        {
            if (active)
            {
                Vector3Int pos = new Vector3Int((int)position.x, (int)position.y, 0);
                _tilemap.SetTile(pos, _tile);
            }
        }

        public void DrawTiles(Tilemap tilemap, Tile tile)
        {
            if (_grid == null) return;
            _tilemap = tilemap;
            _tile = tile;

            for (int x = 0; x < _grid.Squares.GetLength(0); x++)
            {
                for (int y = 0; y < _grid.Squares.GetLength(1); y++)
                {
                    DrawTile(_grid.Squares[x, y].TopLeftNode.Active, _grid.Squares[x, y].TopLeftNode.Position);
                    DrawTile(_grid.Squares[x, y].TopRightNode.Active, _grid.Squares[x, y].TopRightNode.Position);
                    DrawTile(_grid.Squares[x, y].DownRightNode.Active, _grid.Squares[x, y].DownRightNode.Position);
                    DrawTile(_grid.Squares[x, y].DownLeftNode.Active, _grid.Squares[x, y].DownLeftNode.Position);
                }
            }
        }
    }

    public class Node
    {
        public Vector3 Position;

        public Node(Vector3 position)
        {
            Position = position;
        }
    }

    public class ControlNode: Node
    {
        public bool Active;

        public ControlNode(Vector3 position, bool active):base(position)
        {
            Active = active;
        }
    }

    public class Square
    {
        public ControlNode TopLeftNode;
        public ControlNode TopRightNode;
        public ControlNode DownRightNode;
        public ControlNode DownLeftNode;

        public Square(ControlNode topLeftNode, ControlNode topRightNode, ControlNode downRightNode, ControlNode downLeftNode)
        {
            TopLeftNode = topLeftNode;
            TopRightNode = topRightNode;
            DownRightNode = downRightNode;
            DownLeftNode = downLeftNode;
        }
    }

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
