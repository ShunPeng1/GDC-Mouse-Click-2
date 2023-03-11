using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform collectiblePosition;
    [SerializeField] private List<SpawnableCollectibles> spawnableCollectiblesList;
    
    
    
    [Serializable]
    public class SpawnableCollectibles
    {
        public CollectibleType collectibleType;
        [Range(0,100)] public float spawnChances;
    }

    private void Start()
    {
        float totalChance = 0f;
        foreach (var collectible in spawnableCollectiblesList)
        {
            totalChance += collectible.spawnChances;
        }

        float itemAtChances = Random.Range(0, 100);
        float currentChances=0f;
        
        foreach (var collectible in spawnableCollectiblesList)
        {
            currentChances += collectible.spawnChances / totalChance;
            if (currentChances >= itemAtChances)
            {
                SpawnCollectible(collectible.collectibleType);
                break;
            }
        }
        
    }

    private void SpawnCollectible(CollectibleType collectibleType)
    {
        switch (collectibleType)
        {
            case CollectibleType.Carrot:
                Instantiate(ResourceManager.Instance.carrotPrefab, transform);
                break;
            case CollectibleType.Health:
                Instantiate(ResourceManager.Instance.carrotPrefab, transform);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(collectibleType), collectibleType, null);
        }
    }
}
