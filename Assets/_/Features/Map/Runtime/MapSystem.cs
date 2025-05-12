using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CustomTools;

public class MapSystem : Tools
{
    
    public enum m_levelDesignEnum
    {
        Sand,
        Water,
        Seed
                
    }
    
    public byte[,] m_staticGridState = new byte[,]
    {
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 3, 12, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},

    };
    
    /*public m_levelDesignEnum[,] m_staticGridState = new m_levelDesignEnum[,]
    {
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 2, 12, 0},
        { 0, 0, 0, 0, 0},
        { 0, 0, 0, 0, 0},

    };*/
    public List<m_levelDesignEnum> m_levels = new List<m_levelDesignEnum>();
    /*
    public Vector2 m_mapSize;
    */
        
    




    void Start()
    {
        /*
        Info($"Static Grid Size: {m_staticGridState[2,3]}");
        */
        /*
        Debug.Log($"Static Grid Size: {m_staticGridState.GetLength(0)}");
        */

        
        for (int sizeX = 0; sizeX < m_staticGridState.GetLength(0); sizeX++)
        {
            for (int sizeY = 0; sizeY < m_staticGridState.GetLength(1); sizeY++)
            {   
                Info($"Length Item ?: {m_staticGridState[sizeX,sizeY]}");
                Instantiate(_cellPrefab,new Vector2(sizeX,sizeY),Quaternion.identity);
            }
        }
    }

    #region Utils

    /*private void SetNextState()
    {
        for (int i = 0; i < m_gridState.Length; i++)
        {
            //Peut contenir des nombres allant de 0 à 24
            List<int> neigborsIndexList = GetNeighborsIndex(i);
            int aliveNeigh = CountAliveNeigh(neigborsIndexList);

            //Si 3 cellules en vie tu deviens en vie
            if (aliveNeigh == 3)
            {
                m_gridNextState[i] = 1;
            }
            //Si exactement 2 tu reste à ton etat.
            else if (aliveNeigh == 2)
            {
                m_gridNextState[i] = m_gridState[i];
            }
            //Else tu meurs
            //Si + de 3 tu meurs
            else
            {
                m_gridNextState[i] = 0;
            }
        }
    }

    private void UpdateCellColor()
    {
        for (int i = 0; i < m_gridState.Length; i++)
        {
            if (m_gridState[i] == 1)
            {
                _gridOfCells[i].gameObject.GetComponent<Renderer>().material.color = Color.green;

            }
            else
            {
                _gridOfCells[i].gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    private void ApplyNextState()
    {
        for (int i = 0; i < m_gridState.Length; i++)
        {
            m_gridState[i] = m_gridNextState[i];
        }
    }*/

    void Update()
    {
        /*if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SetNextState();
            ApplyNextState();
            UpdateCellColor();
        }*/
    }

    /*private int CountAliveNeigh(List<int> neigborsIndexList)
    {
        int count = 0;
        //Min 3
        //Max 8
        for (int i = 0; i < neigborsIndexList.Count; i++)
        {
            if (m_gridState[neigborsIndexList[i]] == 1)
            {
                count++;
            }
        }
        return count;
    }
    private List<int> GetNeighborsIndex(int currentCellIndex)
    {
        List<int> neighborsIndexList = new List<int>();
        
        Vector2Int currentCellCoordinate = Get2DCoordinates(currentCellIndex);

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

    private bool CheckIfCoordinateAreInBound(Vector2Int neighborCoordinate)
    {
        if (neighborCoordinate.x >= _gridSize.x || neighborCoordinate.y >= _gridSize.y ||
        neighborCoordinate.x < 0 || neighborCoordinate.y < 0)
        {
            return false;
        }
        return true;
    }*/

    #endregion
    
    
    #region Privates
    
    
    private GameObject[] _gridOfCells = new GameObject[25];
    private Vector2Int _gridSize = new Vector2Int(5,5);
    [SerializeField] private GameObject _cellPrefab;
    private Vector2Int Get2DCoordinates(int index)
    {   
        return new Vector2Int(index% _gridSize.x, index/_gridSize.x);
    }

    private int GetIndexFrom2dCoordinates(Vector2Int coordinates)
    {
        // coordinates 1 = 4,5
        // size = 5,6 = 30
        // 4 + (5*5) = 29
        
        // coordinates 2 = 2,3
        // size = 5,6 = 30
        // 2 + (5*3) = 17

        int size = _gridSize.x * _gridSize.y;
        return coordinates.x + (_gridSize.x*coordinates.y);
    }
    
    #endregion
}
