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
}
