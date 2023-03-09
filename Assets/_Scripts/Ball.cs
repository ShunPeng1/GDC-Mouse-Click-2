using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("GameObject Properties")]
    private Rigidbody2D rb;

    
    [Header("push Properties")]
    [SerializeField] private float pushForce;
    [SerializeField, Min(1)] private int maxPushEnergy = 1;
    
    private Vector3 _initialPos;
    private bool _isPushing;
    [SerializeField]private int _currentPushEnergy;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _currentPushEnergy = maxPushEnergy;
    }
    
    private void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            ExecutePush();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            InitPush();   
        }
    }

    private void ExecutePush()
    {
        if (!_isPushing) return;
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 backwardDirection =  (_initialPos - mousePosition).normalized;
        
        rb.AddForce(backwardDirection * pushForce);

        _isPushing = false;

    }

    private void InitPush()
    {
        if (_currentPushEnergy <= 0) return;
        _initialPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _isPushing = true;
        _currentPushEnergy--;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            _currentPushEnergy = maxPushEnergy;
        }
    }
    
    
}
