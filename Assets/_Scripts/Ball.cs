using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 InitialPos;

    private void Start()
    {
        InitialPos = transform.position;
    }

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            InitialPos = mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 vectorForce = mousePosition -  InitialPos;
            
            GetComponent<Rigidbody2D>().AddForce(vectorForce * 300);
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
