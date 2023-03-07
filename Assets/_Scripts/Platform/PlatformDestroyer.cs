using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Manager;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Rigidbody2D _rigidbody2D;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = Vector2.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        Debug.Log("Hit "+ other.gameObject.name );
        if (other.CompareTag($"Platform"))
        {
            PlatformManager.Instance.CreateNewPlatform();
            Destroy(other.gameObject);
        }
    }

}
