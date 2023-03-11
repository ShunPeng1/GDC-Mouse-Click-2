using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            OnCollect();
        } 
    }

    public virtual void OnCollect()
    {
        Destroy(gameObject);
    }

    public virtual void DnRemove()
    {
        Destroy(gameObject);    
    }
    
}
