using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("GameObject Properties")]
    private Rigidbody2D rb;

    
    [Header("Push Properties")]
    [SerializeField] private float pushForce;
    [SerializeField, Min(1)] private int maxPushEnergy = 1;
    [SerializeField] private float collideCheckDuration = 0.3f;
    
    private Vector3 _initialPos;
    private bool _isPushing;
    private bool _isColliding;
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
            // GetComponent<LineRenderer>.enabled = false;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            InitPush();
            // GetComponent<LineRenderer>.enabled = true;        
        }
    }

    private void ExecutePush()
    {
        if (!_isPushing) return;
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 backwardDirection =  (_initialPos - mousePosition).normalized;
        
        rb.velocity = Vector2.zero;
        rb.AddForce(backwardDirection * pushForce);

        _isPushing = false;
        _currentPushEnergy--;
    }

    private void InitPush()
    {
        if (_currentPushEnergy <= 0) return;
        _initialPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        _isPushing = true;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            StartCoroutine(IgnoreCollisionShortly(col));
        }
    }

    private IEnumerator IgnoreCollisionShortly(Collision2D col)
    {
        if(_isColliding) yield break;

        _isColliding = true;
        _currentPushEnergy = maxPushEnergy;
        yield return new WaitForSeconds(collideCheckDuration);
        

        _isColliding = false;
    }
    
}
