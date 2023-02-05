using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlatformerMVC
{
    public class GeneratorLevelController
    {
        private Tilemap _tilemap;
        private Tile _tile;
        private int _mapHeight;
        private int _mapWidth;

        private int _fillPercent;
        private int _smoothPercent;

        private bool _borders;
        private int[,] _map;

        private MarchingSquaresController _marchingSquaresController;
        private bool _mode;

        public GeneratorLevelController(GeneratorLevelView view)
        {
            _tilemap = view.Tilemap;
            _tile = view.Tile;
            _mapHeight = view.MapHeight;
            _mapWidth = view.MapWidth;

            _fillPercent = view.FillPercent;
            _smoothPercent = view.SmoothPercent;

            _borders = view.Borders;

            _map = new int[_mapHeight, _mapWidth];

            _mode = view.GenerationMode;
        }

    public void Start()
        {
            FillMap();

            for (int i = 0; i < _smoothPercent; i++)
            {
                SmoothMap();
            }

            if (_mode)
            {
                _marchingSquaresController = new MarchingSquaresController();
                _marchingSquaresController.GenerateGrid(_map, 1f);
                _marchingSquaresController.DrawTiles(_tilemap, _tile);
            }
            else
            {
                DrawTiles();
            }
        }

        private void FillMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (_borders && (x == 0 || y == 0 || x == _mapWidth - 1 || y == _mapHeight - 1)) 
                    {
                        _map[x, y] = 1;
                    }
                    else
                    {
                        _map[x, y] = Random.Range(0, 100) < _fillPercent ? 1 : 0;
                    }
                }
            }
        }

        private void SmoothMap()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    int neigbour = GetNeigbour(x,y);
                    if (neigbour > 4)
                    {
                        _map[x, y] = 1;
                    }
                    else if(neigbour < 4)
                    {
                        _map[x, y] = 0;
                    }
                }
            }
        }

        private int GetNeigbour(int x, int y)
        {
            int neigbour = 0;
            for (int xGrid = x - 1; xGrid <= x + 1; xGrid++)
            {
                for (int yGrid = y - 1; yGrid <= y + 1; yGrid++)
                {
                    if (xGrid >= 0 && xGrid < _mapWidth && yGrid >= 0 && yGrid < _mapHeight)
                    {
                        if (xGrid != x || yGrid != y)
                        {
                            neigbour += _map[xGrid, yGrid];
                        }
                    }
                    else
                    {
                        neigbour++;
                    }
                }
            }
            return neigbour;
        }

        private void DrawTiles()
        {
            for (int x = 0; x < _mapWidth; x++)
            {
                for (int y = 0; y < _mapHeight; y++)
                {
                    if (_map[x, y] == 1)
                    {
                        Vector3Int _tilePosition = new Vector3Int(-_mapWidth / 2 + x, -_mapHeight / 2 + y, 0);
                        _tilemap.SetTile(_tilePosition, _tile);
                    }
                    else
                    {
                        _map[x, y] = 0;
                    }
                }
            }
        }
    }
}
