using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectible : MonoBehaviour
{
    [SerializeField] private UnityEvent onCollect;
    
    
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            onCollect.Invoke();
            CollectAnimation();
        } 
    }

    public virtual void CollectAnimation()
    {
        Destroy(gameObject);
    }

    public virtual void DestroyAnimation()
    {
        Destroy(gameObject);    
    }
    
}
