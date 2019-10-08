using UnityEngine;

namespace Gameplay
{
    public class Cubelet : MonoBehaviour
    {
        public BoxCollider2D col;

        private Vector2 _cubeletPos;

        private void Start()
        {
            col = GetComponent<BoxCollider2D>();
            CubeletManager.AddCubelet(this);
        }

        public void MoveCubelet(Vector2 newPos)
        {
            _cubeletPos = newPos;
            UpdateCubelet();
        }

        private void UpdateCubelet()
        {
            transform.position = (Vector2) Player.Reference.transform.position + _cubeletPos;
        }
    }
}