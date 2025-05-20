using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace Shared.Runtime
{
    public class GameManager : MonoBehaviour
    {
        
        #region Public

        public GameObject m_cellPrefab;
        public int m_width = 10;
        public int m_height = 10;
        public float m_propagationDelay = 0.2f;

        #endregion

        #region Unity API

        private void Start()
        {
            _cellParent = new GameObject("GeneratedMap");
            GenerateGrid();
            AddInitialWater();
        }

        #endregion


        #region Utils

        public void StartWaterPropagation(Vector2Int origin)
        {
            _waterFront.Clear();
            _waterFront.Add(_grid[origin.x, origin.y]);
            StartCoroutine(PropagateWater());
        }

        private IEnumerator PropagateWater()
        {
            while (_waterFront.Count > 0)
            {
                List<Cell> nextWave = new List<Cell>();
                foreach (var cell in _waterFront)
                {
                    Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
                    foreach (var direction in directions)
                    {
                        Vector2Int pos = cell.m_gridPosition + direction;
                        if (IsInsideGrid(pos))
                        {
                            var neighbor = _grid[pos.x, pos.y];
                            if (neighbor.m_type == Cell.CellType.Empty)
                            {
                                neighbor.SetType(Cell.CellType.Water);
                                nextWave.Add(neighbor);
                            }
                        }
                    }
                }
                _waterFront = nextWave;
                yield return new WaitForSeconds(m_propagationDelay);
            }
        }
        void AddInitialWater()
        {
            // Exemple : mettre de l'eau au milieu
            int cx = m_width / 2;
            int cy = m_height / 2;
            _grid[cx, cy].SetType(Cell.CellType.Water);
        }
        
        public void GenerateGrid()
        {
            _grid = new Cell[m_width, m_height];
            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_height; y++)
                {
                    var go = Instantiate(m_cellPrefab, new Vector3(x, y, 0), Quaternion.identity,_cellParent.transform);
                    var block = go.GetComponent<Cell>();
                    block.Initialize(new Vector2Int(x,y),Cell.CellType.Sand,this);
                    _grid[x, y] = block;
                }
            }
        }

        public void TryToConvertToEmpty(Cell cell)
        {
            cell.SetType(Cell.CellType.Empty);
            if (IsAdjacentToWater(cell.m_gridPosition))
            {
                cell.SetType(Cell.CellType.Water);
                StartWaterPropagation(cell.m_gridPosition);
            }
        }

        private bool IsAdjacentToWater(Vector2Int pos)
        {
            Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };

            foreach (var direction in directions)
            {
                Vector2Int neighPos = pos + direction;
                if (IsInsideGrid(neighPos))
                {
                    if (_grid[neighPos.x, neighPos.y].m_type == Cell.CellType.Water) return true;
                }
            }

            return false;
        }

        bool IsInsideGrid(Vector2Int pos)
        {
            return pos.x >= 0 && pos.x < m_width && pos.y >= 0 && pos.y < m_height;
        }
        #endregion
        #region Private

        private Cell[,] _grid;
        private GameObject _cellParent;
        private List<Cell> _waterFront = new List<Cell>();


        #endregion

    }
}
