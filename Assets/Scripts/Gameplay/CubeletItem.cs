using System;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class CubeletItem : MonoBehaviour
    {
        public GameObject cubelet;
        
        public CircleCollider2D attractionRadiusCol;
        public BoxCollider2D col;

        public float attractionSpeed;

        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (IsCloseToCubelet()) MoveTowardsPlayer();
            else return;
            if (IsTouchingCubelet()) MakeCubelet();
        }

        private bool IsCloseToCubelet()
        {
            var foundColliders = new Collider2D[10];
            var collidersNumber = attractionRadiusCol.OverlapCollider(new ContactFilter2D().NoFilter(), foundColliders);
            Debug.Log(collidersNumber);

            for (var i = 0; i < collidersNumber; i++)
            {
                var foundCol = foundColliders[i];
                if (foundCol.CompareTag("Cubelet")) return true;
            }

            return false;
        }

        private bool IsTouchingCubelet()
        {
            var foundColliders = new Collider2D[10];
            var collidersNumber = col.OverlapCollider(new ContactFilter2D().NoFilter(), foundColliders);

            for (var i = 0; i < collidersNumber; i++)
            {
                var foundCol = foundColliders[i];
                if (foundCol.CompareTag("Cubelet")) return true;
            }

            return false;
        }

        private void MoveTowardsPlayer()
        {
            _rb.velocity = -Player.Reference.transform.position.normalized * attractionSpeed;
        }

        private void MakeCubelet()
        {
            var newCubelet = Instantiate(cubelet, transform.position, Quaternion.identity);
            newCubelet.transform.SetParent(Player.Reference.transform, false);
            var newCubeletComponent = newCubelet.GetComponent<Cubelet>();
            newCubeletComponent.MoveCubelet(new Vector2(5,5));
            Destroy(gameObject);
        }
    }
}
