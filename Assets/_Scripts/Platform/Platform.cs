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
        [Range(0,100)] public int spawnChances;
    }

    public void InitSpawnCollectible()
    {
        int totalChance = 0;
        foreach (var collectible in spawnableCollectiblesList)
        {
            totalChance += collectible.spawnChances;
        }

        totalChance = Mathf.Max(100, totalChance);
        
        int itemAtChances = Random.Range(0, 100);
        int currentChances = 0;
        
        foreach (var collectible in spawnableCollectiblesList)
        {
            currentChances +=  (int) (100f * (float)collectible.spawnChances / (float)totalChance);
            //Debug.Log(currentChances + " <= "+ itemAtChances);
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
                Instantiate(ResourceManager.Instance.carrotPrefab, collectiblePosition.transform);
                break;
            case CollectibleType.Health:
                Instantiate(ResourceManager.Instance.carrotPrefab,  collectiblePosition.transform);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(collectibleType), collectibleType, null);
        }
    }
}
