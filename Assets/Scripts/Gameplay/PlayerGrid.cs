using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class PlayerGrid : MonoBehaviour
    {
        public GameObject playerGridCell;
        
        public int gridSize;
        
        private readonly List<PlayerGridCell> _cells = new List<PlayerGridCell>();

        private void Start()
        {
            DrawGrid();
        }
        
        public void ActivateGrid()
        {
            gameObject.SetActive(true);
            foreach (var cell in _cells) cell.Deactivate();
            foreach (var cubelet in Player.Reference.cubelets) cubelet.activatedNearbyCells = false;
            foreach (var cubelet in Player.Reference.cubelets) if (!cubelet.movable) cubelet.ActivateNearbyCells();
        }

        public void DeactivateGrid()
        {
            gameObject.SetActive(false);
        }

        private void DrawGrid()
        {
            foreach (var cell in _cells) Destroy(cell.gameObject);
            for (var y = 0; y < gridSize; y++)
            {
                for (var x = 0; x < gridSize; x++)
                {
                    var newCell = Instantiate(playerGridCell, transform.position, Quaternion.identity);
                    newCell.transform.SetParent(transform, false);
                    newCell.transform.position = transform.position + new Vector3(x, y);
                    _cells.Add(newCell.GetComponent<PlayerGridCell>());
                }
            }
        }
    }
}
