using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("GameObject Properties")]
    private Rigidbody2D _rigidbody2D;
    private AudioSource _audioSource;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private Trajectory trajectory;
    [SerializeField] private ParticleSystem launchParticles;

    [Header("Audio Clips")]
    [SerializeField] AudioClip soundJump;
    [SerializeField] AudioClip soundHitOnRock;
    [SerializeField] AudioClip soundLandedOnGrass;
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
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            InitPush();
        }

        if (_isPushing)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 backwardDirection =  (_initialPos - mousePosition).normalized;
            trajectory.SimulateTrajectory( backwardDirection * pushForce);
        }
    }

    private void ExecutePush()
    {
        if (!_isPushing) return;
        
        Vector3 mousePosition = Input.mousePosition;
        Vector3 backwardDirection =  (_initialPos - mousePosition).normalized;
        
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce(backwardDirection * pushForce, ForceMode2D.Force );

        _isPushing = false;
        _currentPushEnergy--;
        
        SoundManager.Instance.PlaySoundEffect(soundJump);
        _animator.SetTrigger(IsJumping);
        _spriteRenderer.flipX = backwardDirection.x < 0;
    }

    private void InitPush()
    {
        if (_currentPushEnergy <= 0) return;
        _initialPos = Input.mousePosition;

        _isPushing = true;
    }

    private IEnumerator OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            if(_isColliding) yield break;
            _isColliding = true;
        
            // The player collide exactly above the platform
            if (col.transform.position.y < transform.position.y)
            {
                _currentPushEnergy = maxPushEnergy;
            }

            yield return new WaitForSeconds(collideCheckDuration);
        
            _isColliding = false;
        }
    }
    
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            // The player collide exactly above the platform
            if (col.transform.position.y < transform.position.y)
            {
                _currentPushEnergy = maxPushEnergy;
                _audioSource.PlayOneShot(soundLandedOnGrass);
            }
            else
            {
                _audioSource.PlayOneShot(soundHitOnRock);
            }
            
        }
    }

    private IEnumerator IgnoreCollisionShortly(Collision2D col)
    {
        if(_isColliding) yield break;

        _isColliding = true;
        
        // The player collide exactly above the platform
        if (col.transform.position.y < transform.position.y)
        {
            _currentPushEnergy = maxPushEnergy;
            _audioSource.PlayOneShot(soundLandedOnGrass);
        }
        else
        {
            _audioSource.PlayOneShot(soundHitOnRock);
        }

        yield return new WaitForSeconds(collideCheckDuration);
        
        _isColliding = false;
    }
    
}
