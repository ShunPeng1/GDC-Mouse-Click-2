using System;
using System.Collections;
using System.Collections.Generic;
using GD.MinMaxSlider;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


namespace _Scripts.Manager
{
    public class PlatformManager : Singleton<PlatformManager>
    {
        [SerializeField] private Transform spawnerTransform;
        [SerializeField] private Transform platformParents;
        
        [Header("Random Spawn Properties")] 
        [SerializeField] private int maxPlatform = 10;
        [SerializeField, MinMaxSlider(-30,30)] private Vector2 xSpawnRange, ySpawnRange;
        [SerializeField] private float spawnCooldown = 1f;
        
        private int _currentNumberOfPlatform = 0;
        private bool _isSpawning = false;

        [Header("Simulate Scene")]
        private Scene _simulationScene;
        private PhysicsScene2D _physicsScene;
        private readonly Dictionary<int, GameObject> _spawnedGhostObjects = new();

        
        public void DestroyPlatform(GameObject platform)
        {
            _currentNumberOfPlatform--;
            Destroy(_spawnedGhostObjects[platform.GetInstanceID()]);
            _spawnedGhostObjects.Remove(platform.GetInstanceID());
            Destroy(platform);
        }
        public IEnumerator CreateNewPlatform()
        {
            if(_isSpawning) yield break;
            _isSpawning = true;
            
            _currentNumberOfPlatform++;

            var position = spawnerTransform.position;
            GameObject obj = Instantiate(ResourceManager.Instance.platformPrefab, new Vector3( 
                position.x+ Random.Range(xSpawnRange.x, xSpawnRange.y), 
                position.y+ Random.Range(ySpawnRange.x, ySpawnRange.y),
                position.z) , Quaternion.identity, platformParents);
            
            CreateGhostPlatform(obj);
            obj.GetComponent<Platform>().InitSpawnCollectible();

            yield return new WaitForSeconds(spawnCooldown);

            _isSpawning = false;
        }

        private void CreateGhostPlatform(GameObject obj)
        {
            Transform objTransform = obj.transform;
            GameObject ghostObj = Instantiate(obj.gameObject, objTransform.position, objTransform.rotation);

            SpriteRenderer spriteRenderer = ghostObj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) spriteRenderer.enabled = false;

            MoveObjectToSimulateScene(ghostObj);
            _spawnedGhostObjects.Add(obj.GetInstanceID(), ghostObj);
        }
        
        public void Simulate()
        {
            _physicsScene.Simulate(Time.fixedDeltaTime );
        }
        
        public void MoveObjectToSimulateScene(GameObject moveObject)
        {
            SceneManager.MoveGameObjectToScene(moveObject, _simulationScene);   
        }
        
        private void Start()
        {
            CreatePhysicsScene();   
        }
        
        private void CreatePhysicsScene() {
            Platform [] platforms = FindObjectsOfType<Platform>();
            _currentNumberOfPlatform+= platforms.Length;

            _simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics2D));
            _physicsScene = _simulationScene.GetPhysicsScene2D();
        
            foreach (Platform obj in platforms)
            {
                CreateGhostPlatform(obj.gameObject);
                obj.InitSpawnCollectible();
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
