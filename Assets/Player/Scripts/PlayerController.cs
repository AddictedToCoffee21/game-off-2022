using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;


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
    public float attackTime = 1f;

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

        /*
        if (playerState == PlayerState.Bee && Input.GetButtonDown("Fire2"))
        {
            _animator.SetTrigger("Attack");
            StartAttack();
        }
        */

        if (playerState == PlayerState.Bee && Input.GetButtonDown("Fire2"))
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            Vector2 vecToMouse = (mousePos - (Vector2) this.transform.position).normalized;

            float rotAngle = Vector2.SignedAngle(Vector2.right, vecToMouse);

            mouseHitbox.transform.rotation = Quaternion.Euler(0,0, - rotAngle);
            mouseHitbox.transform.position = (Vector2) mouseHitbox.transform.position + vecToMouse * 0.5f;

            if(isSwingAttack) 
            {
                mouseHitbox.transform.rotation = Quaternion.Euler(0,0, - rotAngle - swingAngle);
                mouseHitbox.transform.position = (Vector2) mouseHitbox.transform.position + vecToMouse * 0.2f;
                StartCoroutine("ActivateAttackMouseSwing");
            }
            else 
            {
                mouseHitbox.transform.rotation = Quaternion.Euler(0,0, - rotAngle);
                mouseHitbox.transform.position = (Vector2) mouseHitbox.transform.position + vecToMouse * 0.2f;
                if(isShooting) 
                {
                    Bullet newStinger = GameObject.Instantiate(stinger, this.transform.position, Quaternion.identity);
                    newStinger.gameObject.layer = LayerMask.NameToLayer("PlayerAttack");
                    newStinger.SetVelocity(vecToMouse * 5f);
                }
                else 
                {
                    StartCoroutine("ActivateAttackMouse");
                }
                
            }

            
        }


        if(playerState == PlayerState.Bee)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow)) 
            {
                StartCoroutine("ActivateAttack", Dir.Left);
            }
            else if(Input.GetKeyDown(KeyCode.RightArrow)) 
            {
                StartCoroutine("ActivateAttack", Dir.Right);
            }
            else if(Input.GetKeyDown(KeyCode.DownArrow)) 
            {
                StartCoroutine("ActivateAttack", Dir.Down);
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow)) 
            {
                StartCoroutine("ActivateAttack", Dir.Top);
            }
        }
    }

    public GameObject mouseHitbox;
    public Bullet stinger;

    public bool isSwingAttack = true;
    public bool isShooting = true;
    public float swingAngle = 15f;



    public enum Dir {
        Left,
        Right,
        Top,
        Down
    }

    public GameObject top,left,right,down;

    float angleReached = 0f;

    public IEnumerator ActivateAttackMouseSwing() {

        mouseHitbox.SetActive(true);

        while(Mathf.Abs(angleReached) <= Mathf.Abs(swingAngle * 2)) {
            mouseHitbox.transform.RotateAround(this.transform.position, Vector3.forward, - 200 * Time.deltaTime);
            angleReached += (-200 * Time.deltaTime);
            yield return null;
        }

        angleReached = 0f;
        mouseHitbox.SetActive(false);
        mouseHitbox.transform.rotation = Quaternion.Euler(0,0,0);
        mouseHitbox.transform.position = this.transform.position;



    }

    public IEnumerator ActivateAttackMouse() {

        mouseHitbox.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        mouseHitbox.SetActive(false);
        mouseHitbox.transform.rotation = Quaternion.Euler(0,0,0);
        mouseHitbox.transform.position = this.transform.position;
    }

    public IEnumerator ActivateAttack(Dir dir) {

        switch(dir) {
            case Dir.Left: left.SetActive(true); break;
            case Dir.Right: right.SetActive(true);  break;
            case Dir.Top: top.SetActive(true); break;
            case Dir.Down: down.SetActive(true); break;
        }
        yield return new WaitForSecondsRealtime(0.5f);

        right.SetActive(false);
        left.SetActive(false);
        top.SetActive(false);
        down.SetActive(false);
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
        //TODO: Tie Attacktime to Animation with Events
        StartCoroutine("Attack");
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

    private IEnumerator Attack() {
        _attackCollider.enabled = true;
        yield return new WaitForSecondsRealtime(attackTime);
        _attackCollider.enabled = false;
    }

}
