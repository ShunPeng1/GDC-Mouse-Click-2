using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 _initialPos;
    private Rigidbody2D rb;
    public float push;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _initialPos = mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 backwardDirection =  (_initialPos - mousePosition).normalized;
            
            rb.AddForce(backwardDirection * push);
            rb.gravityScale = 1;
        }
    }
    
    
    
}
