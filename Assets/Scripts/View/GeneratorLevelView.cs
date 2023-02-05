using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PlatformerMVC
{
    public class GeneratorLevelView : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private Tile _tile;
        [SerializeField] private int _mapHeight;
        [SerializeField] private int _mapWidth;

        [Range(1, 100)] [SerializeField] private int _fillPercent;
        [Range(1, 100)] [SerializeField] private int _smoothPercent;

        [SerializeField] private bool _borders;
        [Tooltip("false for cellular automaton, true for marching squares")] [SerializeField] private bool _generationMode;


        public Tilemap Tilemap { get => _tilemap; set => _tilemap = value; }
        public Tile Tile { get => _tile; set => _tile = value; }
        public int MapHeight { get => _mapHeight; set => _mapHeight = value; }
        public int MapWidth { get => _mapWidth; set => _mapWidth = value; }
        public int FillPercent { get => _fillPercent; set => _fillPercent = value; }
        public int SmoothPercent { get => _smoothPercent; set => _smoothPercent = value; }
        public bool Borders { get => _borders; set => _borders = value; }
        public bool GenerationMode { get => _generationMode; set => _generationMode = value; }
    }
}
