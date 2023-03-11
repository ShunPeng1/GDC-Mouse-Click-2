using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class TrajectoryGhostObject : MonoBehaviour
{
    private Rigidbody2D _rb;
    
    public void Init(Vector3 velocity) {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(velocity, ForceMode2D.Force);
    }


    
    
}
