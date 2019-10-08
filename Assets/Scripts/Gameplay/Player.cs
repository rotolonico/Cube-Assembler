using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Player : MonoBehaviour
    {
        public static Player Reference;
        public static PlayerController PlayerController;
        public static CubeletManager CubeletManager;
        
        public List<Cubelet> cubelets = new List<Cubelet>();

        private void Awake()
        {
            Reference = this;
            PlayerController = GetComponent<PlayerController>();
            CubeletManager = GetComponent<CubeletManager>();
        }
    }
}
