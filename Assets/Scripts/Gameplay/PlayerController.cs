using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay
{
    public class PlayerController : MonoBehaviour  
    {
        public float moveForce;
        public float jumpForce;

        private void FixedUpdate()
        {
            if (!Player.Reference.isDraggingCubelet) CheckInputs();
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
            Player.Reference.rb.velocity = new Vector2(-moveForce, Player.Reference.rb.velocity.y);
        }
        
        private void MoveRight()
        {
            Player.Reference.rb.velocity = new Vector2(moveForce, Player.Reference.rb.velocity.y);
        }

        private void StopMoving()
        {
            Player.Reference.rb.velocity = new Vector2(0, Player.Reference.rb.velocity.y);
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
            Player.Reference.rb.AddForce(new Vector2(0, jumpForce));
        }
    }
}
