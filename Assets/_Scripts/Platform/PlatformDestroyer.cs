using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Manager;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    public GameObject panel, button, text;
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

        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0;
            panel.SetActive(true);
            button.SetActive(true);
            text.SetActive(true);
        }
    }
    
}
