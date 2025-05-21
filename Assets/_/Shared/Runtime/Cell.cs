using System;
using UnityEngine;

namespace Shared.Runtime
{
    public class Cell : MonoBehaviour
    {
        #region Public
        
        //Enum
        public enum CellType
        {
            Empty = -1,
            Sand,
            Water,
            Seed,
            Tree,
            Pirate,
            Villager,
            DrownVillager,
        }

        //Grid
        public Vector2Int m_gridPosition;
        public CellType m_type;

        [SerializeField]
        private Sprite _emptySprite;
        [SerializeField]
        private Sprite _sandSprite;
        [SerializeField]
        private Sprite _waterSprite;
        [SerializeField]
        private Sprite _seedSprite;
        [SerializeField]
        private Sprite _treeSprite;
        [SerializeField]
        private Sprite _pirateSprite;
        [SerializeField]
        private Sprite _villagerSprite;
        [SerializeField]
        private Sprite _drownSprite;
        

        #endregion

        #region Unity Api

        private void OnMouseDown()
        {
            if (m_type == CellType.Sand)
            {
                _gameManager.TryToConvertToEmpty(this);
            }
        }

        public void SetType(CellType newType)
        {
            m_type = newType;
            UpdateVisual();
        }
        #endregion

        #region Utils

        public void Initialize(Vector2Int gridPosition, CellType startType, GameManager manager)
        {
            m_gridPosition = gridPosition;
            m_type = startType;
            _gameManager = manager;
            UpdateVisual();
        }

        public void UpdateVisual()
        {
            var renderer = GetComponent<SpriteRenderer>();
            switch (m_type)
            {
                case CellType.Sand:
                    renderer.sprite = _sandSprite;
                    break;
                case CellType.Empty:
                    renderer.sprite = _emptySprite;
                    break;
                case CellType.Water:
                    renderer.sprite = _waterSprite;
                    break;
                case CellType.Tree:
                    renderer.sprite = _treeSprite;
                    break;
                case CellType.Seed:
                    renderer.sprite = _seedSprite;
                    break;
                case CellType.Villager:
                    renderer.sprite = _villagerSprite;
                    break;
                case CellType.DrownVillager:
                    renderer.sprite = _drownSprite;
                    break;
            }
        }

        
        #endregion

        
        #region Private

        private GameManager _gameManager;

        #endregion

    }
}
