using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class TrajectoryGhostObject : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private GameObject _poofPrefab;
    private bool _isGhost;

    private void Start()
    {
    }

    public void Init(Vector3 velocity, bool isGhost) {
        _isGhost = isGhost;
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(velocity, ForceMode2D.Impulse);
    }

    public void OnCollisionEnter(Collision col) {
        if (_isGhost) return;
        //Instantiate(_poofPrefab, col.contacts[0].point, Quaternion.Euler(col.contacts[0].normal));
        //_source.clip = _clips[Random.Range(0, _clips.Length)];
        //_source.Play();
    }
    
    
}
