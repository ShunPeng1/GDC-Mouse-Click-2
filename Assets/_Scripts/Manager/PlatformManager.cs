using UnityEngine;

namespace _Scripts.Manager
{
    public class PlatformManager : Singleton<PlatformManager>
    {
        [SerializeField] private Transform spawnerTransform;
        [SerializeField] private float leftXPosition, rightXPosition;

        public void CreateNewPlatform()
        {
            var position = spawnerTransform.position;
            Instantiate(ResourceManager.Instance.platformPrefab, new Vector3( position.x+ Random.Range(leftXPosition, rightXPosition), position.y, position.z) , Quaternion.identity);
        }
    
    }
}
