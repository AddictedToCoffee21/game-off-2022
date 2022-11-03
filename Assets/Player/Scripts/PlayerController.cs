using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Butterfly = 0,
        Bee = 1
    }

    public enum Direction
    {
        Left = 0,
        Right = 1
    }
    
    public PlayerState playerState;
    public Direction playerDirection;
    
    [Space(10)]
    
    public float butterflySpeed = 5;
    public float butterflyAcceleration = 1;
    
    [Space(10)]
    
    public float beeSpeed = 3;
    public float beeAcceleration = 1;

    [Space(10)] 
    
    public RuntimeAnimatorController animatorControllerButterfly;
    public RuntimeAnimatorController animatorControllerBee;

    [SerializeField] private float _horizontalMovement;
    [SerializeField] private float _verticalMovement;
    private float _currentPlayerSpeed;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _animator;
    private ParticleSystem _particleSystem;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1"))
        {
            playerState = (PlayerState)(((int) playerState + 1) % 2);
            _particleSystem.Play();
        }

        if (playerDirection == Direction.Right)
        {
            _sr.flipX = true;
        }
        else
        {
            _sr.flipX = false;
        }

        if (playerState == PlayerState.Butterfly && _animator.runtimeAnimatorController != animatorControllerButterfly)
        {
            _animator.runtimeAnimatorController = animatorControllerButterfly;
        }
        
        if (playerState == PlayerState.Bee && _animator.runtimeAnimatorController != animatorControllerBee)
        {
            _animator.runtimeAnimatorController = animatorControllerBee;
        }

        if (playerState == PlayerState.Bee && Input.GetButtonDown("Fire2"))
        {
            _animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (_horizontalMovement > 0)
            playerDirection = Direction.Right;
        if (_horizontalMovement < 0)
            playerDirection = Direction.Left;

        if (_horizontalMovement == 0 && _verticalMovement == 0)
        {
            Debug.Log("TRUE");
            _currentPlayerSpeed = 0;

            _animator.SetFloat("AnimationSpeed", 0.7f);
        }
        else
        {
            _animator.SetFloat("AnimationSpeed", 1f);
            if(playerState == PlayerState.Butterfly)
            {
                _currentPlayerSpeed += butterflyAcceleration + Time.fixedDeltaTime;

                if (_currentPlayerSpeed >= butterflySpeed)
                {
                    _currentPlayerSpeed = butterflySpeed;
                }
            }
            else
            {
                _currentPlayerSpeed += beeAcceleration + Time.fixedDeltaTime;

                if (_currentPlayerSpeed >= beeSpeed)
                {
                    _currentPlayerSpeed = beeSpeed;
                }
            }
        }
        
        _rb.velocity = new Vector2(_horizontalMovement, _verticalMovement).normalized * _currentPlayerSpeed;
    }
}
