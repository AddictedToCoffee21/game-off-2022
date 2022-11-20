using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


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

    [Space(10)]

    public int playerHealth = 10;
    public int playerMaxHealth = 10;
    public int playerDamage = 1;
    public float invincibleTime = 1f;
    public Color blinkColor = Color.clear;
    public float attackCooldown = 1f;
    private bool isAttackReady = true;

    [Space(10)]
    [Tooltip("The layers from which the player will take Damage")]
    public LayerMask hitLayers;

    private float _currentPlayerSpeed;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _animator;
    private ParticleSystem _particleSystem;
    [SerializeField] private Collider2D _hitboxCollider;
    [SerializeField] private Collider2D _attackCollider;

    [Space(10)]
    public Healthbar healthbar;
    public Attackbar attackbar;
    public Bullet stinger;
    public float stingerSpeed = 5; 
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();

        healthbar.UpdateHealthDisplay(playerHealth, playerMaxHealth);
    }

    private void Update()
    {
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
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

        if (playerState == PlayerState.Bee && Input.GetButtonDown("Fire2") && isAttackReady)
        {
            StartCoroutine("StartAttackCooldown");
            StartAttack();
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
            _currentPlayerSpeed = 0;

            _animator.SetFloat("AnimationSpeed", 0.7f);
        }
        else
        {
            _animator.SetFloat("AnimationSpeed", 1f);
            if(playerState == PlayerState.Butterfly)
            {
                _currentPlayerSpeed += 100 *butterflyAcceleration * Time.fixedDeltaTime;

                if (_currentPlayerSpeed >= butterflySpeed)
                {
                    _currentPlayerSpeed = butterflySpeed;
                }
            }
            else
            {
                _currentPlayerSpeed += 100 * beeAcceleration * Time.fixedDeltaTime;

                if (_currentPlayerSpeed >= beeSpeed)
                {
                    _currentPlayerSpeed = beeSpeed;
                }
            }
        }
        
        _rb.velocity = new Vector2(_horizontalMovement, _verticalMovement).normalized * _currentPlayerSpeed * Time.fixedDeltaTime * 100;
    }

    public void StartAttack() {
        
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 vecToMouse = (mousePos - (Vector2) this.transform.position).normalized;

        float rotAngle = Vector2.SignedAngle(Vector2.right, vecToMouse);

        Bullet newStinger = GameObject.Instantiate(stinger, this.transform.position + (Vector3) vecToMouse * 0.5f, Quaternion.identity);
        newStinger.SetRotation(rotAngle);
        newStinger.SetVelocity(vecToMouse * stingerSpeed);
    }

    public IEnumerator StartAttackCooldown() {
        isAttackReady = false;
        attackbar.UpdateAttackDisplay(isAttackReady);
        yield return new WaitForSecondsRealtime(attackCooldown);
        isAttackReady = true;
        attackbar.UpdateAttackDisplay(isAttackReady);
    }

    public void TakeDamage(DamageDealer damageDealer) 
    {

        playerHealth -= damageDealer.GetDamage();
        
        healthbar.UpdateHealthDisplay(playerHealth, playerMaxHealth);

        StartCoroutine("Invincibility");

        if(playerHealth <= 0)
        {
            KillPlayer();
        }

    }

    public int GetDamage() {
        return playerDamage;
    }

    public void KillPlayer() 
    {
        Debug.Log("Player Dead");
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private IEnumerator Invincibility() 
    {
        _hitboxCollider.enabled = false;
        float blinkTime = 0.1f;
        float elapsedTime = 0;

        while(elapsedTime < invincibleTime) {
            _sr.color = blinkColor;
            yield return new WaitForSecondsRealtime(blinkTime);
            _sr.color = Color.white;
            yield return new WaitForSecondsRealtime(blinkTime);
            elapsedTime += blinkTime * 2;
        }
        _hitboxCollider.enabled = true;
    }

}
