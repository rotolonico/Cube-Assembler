using UnityEngine;

namespace Gameplay
{
    public class PlayerGridCell : MonoBehaviour
    {
        public SpriteRenderer sr;
        public bool isActive;
        
        public void Activate()
        {
            isActive = true;
            sr.enabled = true;
        }

        public void Deactivate()
        {
            isActive = false;
            sr.enabled = false;
        }
    }
}
