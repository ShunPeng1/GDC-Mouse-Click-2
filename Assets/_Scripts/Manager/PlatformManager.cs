using System;
using System.Collections;
using GD.MinMaxSlider;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Manager
{
    public class PlatformManager : Singleton<PlatformManager>
    {
        [SerializeField] private Transform spawnerTransform;

        [Header("Random Spawn Properties")] 
        [SerializeField] private int maxPlatform = 10;
        [SerializeField, MinMaxSlider(-30,30)] private Vector2 xSpawnRange, ySpawnRange;
        [SerializeField] private float spawnCooldown = 1f;
        
        private int _currentNumberOfPlatform = 0;
        private bool _isSpawning = false;

        public void DestroyPlatform(GameObject platform)
        {
            _currentNumberOfPlatform--;
            Destroy(platform);
        }
        public IEnumerator CreateNewPlatform()
        {
            if(_isSpawning) yield break;
            _isSpawning = true;
            
            _currentNumberOfPlatform++;

            var position = spawnerTransform.position;
            Instantiate(ResourceManager.Instance.platformPrefab, new Vector3( 
                position.x+ Random.Range(xSpawnRange.x, xSpawnRange.y), 
                position.y+ Random.Range(ySpawnRange.x, ySpawnRange.y),
                position.z) , Quaternion.identity);
            
            
            yield return new WaitForSeconds(spawnCooldown);

            _isSpawning = false;
        }

        private void Start()
        {
            Platform [] platforms = FindObjectsOfType<Platform>();

            foreach (var platform in platforms)
            {
                _currentNumberOfPlatform++;
            }
        }
        private void Update()
        {
            if (_currentNumberOfPlatform < maxPlatform)
            {
                StartCoroutine(CreateNewPlatform());
                
            }
        }
    }
}
