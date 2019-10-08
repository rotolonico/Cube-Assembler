using System;
using UnityEngine;

namespace Gameplay
{
    public class CubeletManager : MonoBehaviour
    {
        public static void AddCubelet(Cubelet cubelet)
        {
            Player.Reference.cubelets.Add(cubelet);
        }
        
        public void RemoveCubelet(Cubelet cubelet)
        {
            Player.Reference.cubelets.Remove(cubelet);
        }
    }
}
