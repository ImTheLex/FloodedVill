using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CustomTools;

public class MapSystem : Tools
{

    #region Public

    public enum m_levelDesignEnum
    {
        Water,
        Sand,
        Pirate,
        Seed,
        Villager
                
    }
    
    public byte[,] m_staticGridState = new byte[,]
    {
        { 1, 0, 1, 1, 2},
        { 3, 1, 0, 0, 4},
        { 1, 1, 3, 1, 4},
        { 2, 1, 1, 1, 3},
        { 1, 4, 1, 4, 2},

    };
    private byte[,] m_nextStaticGridState = new byte[,]
    {
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},

    };
    
    public List<m_levelDesignEnum> m_levels = new List<m_levelDesignEnum>();
    public List<GameObject> goList;
    public List<Vector2> m_sandBlocksList;
    #endregion


    #region Unity API

    private void Awake()
    {
        m_sandBlocksList = new List<Vector2>();
        _staticGridSize = new Vector2Int(m_staticGridState.GetLength(0), m_staticGridState.GetLength(1));
    }

    void Start()
    {
        goList = new List<GameObject>();
        InstantiateCellsBasedOnLD();
        var water = GetWaterBlocksPosition();
        foreach (var VARIABLE in water)
        {
            Debug.Log("WATER " + VARIABLE);
        }
        
        var sand = GetSandBlocksPosition();
        
        foreach (var VARIABLE in sand)
        {
            Debug.Log("SAND " + VARIABLE);
        }

    }

    #endregion
    

    #region Utils

    //Instantiate cells
    private void InstantiateCellsBasedOnLD()
    {
        int i = 0;
        for (int sizeX = 0; sizeX < m_staticGridState.GetLength(0); sizeX++)
        {
            for (int sizeY = 0; sizeY < m_staticGridState.GetLength(1); sizeY++)
            {

                Vector2Int nextPosition = Get2DDimensionCoordinates(i);
                Info($"Length Item ?: {m_staticGridState[sizeX, sizeY]}");
                switch (m_staticGridState[sizeX, sizeY])
                {
                    case 0:
                        _cellPrefab = m_waterPrefab;
                        break;
                    case 1:
                        _cellPrefab = m_sandPrefab;
                        break;
                    case 2:
                        _cellPrefab = m_piratePrefab;
                        break;
                    case 3:
                        _cellPrefab = m_seedPrefab;
                        break;
                    case 4:
                        _cellPrefab = m_villagerPrefab;
                        break;
                    default:
                        _cellPrefab = m_sandPrefab;
                        break;
                }

                var go = Instantiate(_cellPrefab, new Vector2(nextPosition.x, nextPosition.y), Quaternion.identity);
                goList.Add(go);
                
                var currentCellList = GetNeighborsIndex(i);
                
                
                //Lost here
                Debug.Log($"List Count on cell : {i} + count : {currentCellList.Count}");
                /*for (int v = 0; v < currentCellList.Count; v++)
                {
                    if (IsDestructible((m_levelDesignEnum)currentCellList[v]))
                    {
                        m_staticGridState[sizeX, sizeY] = 0;
                    }
                }*/
                /*for (int VARIABLE = 0; VARIABLE < m_staticGridState.Length; VARIABLE++) 
                {
                    Debug.Log("CELL ? " + VARIABLE + "Count STatic : " +  m_staticGridState.Length +  m_staticGridState[VARIABLE,0]);
                }
                */
                
                i++;



            }
        }
    }

    private bool IsDestructible(m_levelDesignEnum level)
    {
        return level == m_levelDesignEnum.Sand;
    }

    private List<Vector2> GetWaterBlocksPosition()
    {
        List<Vector2> waterBlocksList = new List<Vector2>();
        for (int posX = 0; posX < m_staticGridState.GetLength(0); posX++)
        {
            for (int posY = 0; posY < m_staticGridState.GetLength(1); posY++)
            {
                if (m_staticGridState[posX, posY] == (int)m_levelDesignEnum.Water)
                {
                    waterBlocksList.Add(new Vector2(posX, posY));

                }
            }
        }
        return waterBlocksList;
    }
    
    private List<Vector2> GetSandBlocksPosition()
    {
        List<Vector2> sandBlocksList = new List<Vector2>();
        for (int posX = 0; posX < m_staticGridState.GetLength(0); posX++)
        {
            for (int posY = 0; posY < m_staticGridState.GetLength(1); posY++)
            {
                if (m_staticGridState[posX, posY] == (int)m_levelDesignEnum.Sand)
                {
                    sandBlocksList.Add(new Vector2(posX, posY));

                }
            }
        }
        return sandBlocksList;
    }
    
    
    private Vector2Int Get2DDimensionCoordinates(int index)
    {   
        return new Vector2Int(index% _staticGridSize.x, index/_staticGridSize.x);
    }
    
    //Not okay
    private int CountWaterBlocks(List<int> neigborsIndexList)
    {
      
        int count = 0;
        int indexY;
        int indexX;
        
        
        for (int i = 0; i < neigborsIndexList.Count; i++)
        {
            indexY = neigborsIndexList[i] % (_staticGridSize.y-1);
            indexX = neigborsIndexList[i] % (_staticGridSize.x-1);
            Info($"Water Blocks ?: Y : {indexY},  x : {indexX} Cell ?{m_staticGridState[indexX,indexY]}");

            if (m_staticGridState[indexX,indexY] == 0)
            {

                count++;
            }
        }
        return count;
    }
    
    // Getting neighboors indexes.
    private List<int> GetNeighborsIndex(int currentCellIndex)
    {
        List<int> neighborsIndexList = new List<int>();
        Vector2Int currentCellCoordinate = Get2DDimensionCoordinates(currentCellIndex);

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                Vector2Int vector2Int = new Vector2Int(x, y);
                Vector2Int neighborCoordinate = currentCellCoordinate + vector2Int;

                if(!CheckIfCoordinateAreInBound(neighborCoordinate)) continue;

                int neighborIndex = GetIndexFrom2dCoordinates(neighborCoordinate);
                neighborsIndexList.Add(neighborIndex);
            }
        }
        return neighborsIndexList;
    }
    
    //Stays in range of array.
    private bool CheckIfCoordinateAreInBound(Vector2Int neighborCoordinate)
    {
        if (neighborCoordinate.x >= _staticGridSize.x || neighborCoordinate.y >= _staticGridSize.y ||
            neighborCoordinate.x < 0 || neighborCoordinate.y < 0)
        {
            return false;
        }
        return true;
    }

    
    private int GetIndexFrom2dCoordinates(Vector2Int coordinates)
    {
        int size = _staticGridSize.x * _staticGridSize.y;
        return coordinates.x + (_staticGridSize.x*coordinates.y);
    }
    
    #endregion
    
    
    #region Privates

    [SerializeField] private GameObject m_waterPrefab;
    [SerializeField] private GameObject m_sandPrefab;
    [SerializeField] private GameObject m_piratePrefab;
    [SerializeField] private GameObject m_seedPrefab;
    [SerializeField] private GameObject m_villagerPrefab;
    
    private Vector2Int _staticGridSize;
    private GameObject[] _gridOfCells = new GameObject[25];
    private GameObject _cellPrefab;
    

    
    #endregion
}
