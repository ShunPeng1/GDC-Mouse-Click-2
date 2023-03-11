using UnityEngine;

namespace _Scripts.Manager
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        [Header("Prefabs")] 
        public GameObject platformPrefab;
        public GameObject ghostTrajectoryGameObject;
        public GameObject carrotPrefab;

    }
    
    
}
