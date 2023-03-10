using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("GameObject Properties")]
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    [Header("Audio Clips")]
    [SerializeField] AudioClip soundJump;
    [SerializeField] AudioClip soundHit;
    [SerializeField] AudioClip soundLanded;
    [SerializeField] AudioClip soundDeath;
    
    [Header("Push Properties")]
    [SerializeField] private float pushForce;
    [SerializeField, Min(1)] private int maxPushEnergy = 1;
    [SerializeField] private float collideCheckDuration = 0.3f;
    
    private Vector3 _initialPos;
    private bool _isPushing;
    private bool _isColliding;
    [SerializeField]private int _currentPushEnergy;
    private static readonly int IsFacingLeftNorRight = Animator.StringToHash("IsFacingLeftNorRight");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
        
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce(backwardDirection * pushForce);

        _isPushing = false;
        _currentPushEnergy--;
        
        _audioSource.PlayOneShot(soundJump);
        _animator.SetTrigger(IsJumping);
        _spriteRenderer.flipX = backwardDirection.x < 0;
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
    
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            StartCoroutine(IgnoreCollisionShortly(col));
            _audioSource.PlayOneShot(soundLanded);
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
/*
khi mà bấm bắt đầu vào menu thì
{
    sound_menu.Stop();
    sound_main.Play();
}
*/