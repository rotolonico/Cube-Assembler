using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay
{
    public class Cubelet : MonoBehaviour
    {
        public BoxCollider2D col;
        public bool movable;

        private Vector2 _cubeletPos;

        public bool activatedNearbyCells;

        private void Start()
        {
            col = GetComponent<BoxCollider2D>();
            CubeletManager.AddCubelet(this);
        }

        private void Update()
        {
            if (movable && Inputs.ClickStarted && Inputs.IsClickingObject && Inputs.ClickedObject == gameObject) StartDraggingCubelet();
            if (Player.Reference.isDraggingCubelet && Player.Reference.draggedCubelet == this) DragCubelet();
            if (Player.Reference.isDraggingCubelet && Inputs.ClickEnded && Player.Reference.draggedCubelet == this) StopDraggingCubelet();
        }

        public void EnableCollider()
        {
            col.enabled = true;
        }
        
        public void DisableCollider()
        {
            col.enabled = false;
        }

        public void MoveCubelet(Vector2 newPos)
        {
            _cubeletPos = newPos;
        }

        public void UpdateCubelet()
        {
            transform.position = (Vector2) Player.Reference.transform.position + _cubeletPos;
        }
        
        public void StartDraggingCubelet()
        {
            Player.Reference.StartDraggingCubelet(this);
            DisableCollider();
        }

        public void DragCubelet()
        {
            transform.position = Inputs.ClickPosition;
        }
        
        public void StopDraggingCubelet()
        {
            var cells = new Collider2D[5];
            Physics2D.OverlapCircleNonAlloc(transform.position, 0.01f, cells);
            foreach (var cell in cells)
            {
                if (cell == null || !cell.CompareTag("PlayerGridCell") || !cell.GetComponent<PlayerGridCell>().isActive) continue;
                MoveCubelet(cell.transform.position - Player.Reference.transform.position);
                break;
            }

            UpdateCubelet();
            Player.Reference.StopDraggingCubelet();
            EnableCollider();
        }

        public void ActivateNearbyCells()
        {
            activatedNearbyCells = true;
            if (Player.Reference.isDraggingCubelet && Player.Reference.draggedCubelet == this) return;
            
            var colliders = new Collider2D[10];
            Physics2D.OverlapCircleNonAlloc(transform.position, 0.51f, colliders);
            foreach (var col in colliders)
            {
                if (col == null) continue;
                if (col.CompareTag("Cubelet") && col.gameObject != gameObject)
                {
                    var cubelet = col.GetComponent<Cubelet>();
                    if (!cubelet.activatedNearbyCells) cubelet.ActivateNearbyCells();
                }
                if (col.CompareTag("PlayerGridCell"))
                    col.GetComponent<PlayerGridCell>().Activate();
            }
        }
    }
}