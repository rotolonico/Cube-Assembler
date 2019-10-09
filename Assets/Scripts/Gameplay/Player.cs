using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Player : MonoBehaviour
    {
        public static Player Reference;
        public static PlayerController PlayerController;
        public static CubeletManager CubeletManager;
        public static PlayerGrid PlayerGrid;

        public Rigidbody2D rb;
        public List<Cubelet> cubelets = new List<Cubelet>();

        public bool isDraggingCubelet;
        public Cubelet draggedCubelet;

        private void Awake()
        {
            Reference = this;
            rb = GetComponent<Rigidbody2D>();
            PlayerController = GetComponent<PlayerController>();
            CubeletManager = GetComponent<CubeletManager>();
            PlayerGrid = GetComponentInChildren<PlayerGrid>();
            PlayerGrid.DeactivateGrid();
        }

        public void StartDraggingCubelet(Cubelet cubelet)
        {
            isDraggingCubelet = true;
            draggedCubelet = cubelet;
            PlayerGrid.ActivateGrid();
        }

        public void StopDraggingCubelet()
        {
            isDraggingCubelet = false;
            PlayerGrid.DeactivateGrid();
        }
    }
}
