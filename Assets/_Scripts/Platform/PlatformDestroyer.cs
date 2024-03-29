using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Manager;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit "+ other.gameObject.name );
        if (other.CompareTag($"Platform"))
        {
            PlatformManager.Instance.DestroyPlatform(other.gameObject);
        }

        if (other.CompareTag($"Collectible"))
        {
            other.GetComponent<Collectible>().DestroyCollectible();
        }

        if (other.gameObject.CompareTag($"Player"))
        {
            GameManager.Instance.ChangeGameState(GameManager.GameState.GameOver);
        }
    }
    
}
