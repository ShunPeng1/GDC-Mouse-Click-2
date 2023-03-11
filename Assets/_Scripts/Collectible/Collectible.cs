using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public abstract class Collectible : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("Collect");
            OnCollect(col.gameObject);
        } 
    }

    public abstract void OnCollect(GameObject player);

    public void DestroyCollectible()
    {
        Destroy(gameObject);
    }

}
