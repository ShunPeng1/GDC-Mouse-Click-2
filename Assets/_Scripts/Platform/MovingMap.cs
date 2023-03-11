using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMap : MonoBehaviour
{
    [SerializeField, Min(0)] private float movingSpeed = 3, maxSpeed = 4f;
    private float _currentSpeed = 0;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float linearScale = 0.001f;
    
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _currentSpeed = 0;
    }

    public void StartMoving()
    {
        _currentSpeed = movingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2D.velocity = Vector2.up * _currentSpeed;
        _currentSpeed = Mathf.Min( _currentSpeed+ Time.deltaTime * linearScale, maxSpeed);
    }
}
