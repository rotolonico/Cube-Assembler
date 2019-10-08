using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour  
    {
        public float moveForce;
        public float jumpForce;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            CheckInputs();
        }

        private void CheckInputs()
        {
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            var verticalInput = Input.GetAxisRaw("Vertical");
            
            if (horizontalInput < -0.5f) MoveLeft();
            else if (horizontalInput > 0.5f) MoveRight();
            else StopMoving();
            if (verticalInput > 0.5f && OnGround()) Jump();
        }

        private void MoveLeft()
        {
            _rb.velocity = new Vector2(-moveForce, _rb.velocity.y);
        }
        
        private void MoveRight()
        {
            _rb.velocity = new Vector2(moveForce, _rb.velocity.y);
        }

        private void StopMoving()
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
        }

        private bool OnGround()
        {
            foreach (var cubelet in Player.Reference.cubelets)
            {
                var cubeletTransform = cubelet.transform;
                var cubeletScale = cubeletTransform.localScale;
                var colliders = Physics2D.OverlapBoxAll((Vector2) cubeletTransform.position - new Vector2(0, cubeletScale.y / 2), new Vector2(cubeletScale.x - 0.05f,0.01f), 0);
                if (colliders.Any(col => col.CompareTag("Wall"))) return true;
            }
            
            return false;
        }

        private void Jump()
        {
            _rb.AddForce(new Vector2(0, jumpForce));
        }
    }
}
