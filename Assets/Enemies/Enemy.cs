using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Enemy : MonoBehaviour
{

    protected Rigidbody2D rb2d;

    protected SpriteRenderer spriteRenderer;

    [SerializeField]
    protected int enemyHealth = 1;

    [SerializeField]
    protected int enemyDamage = 1;

    [SerializeField, Range(0,200)]
    protected float enemySpeed = 1;

    [SerializeField] 
    protected bool canShoot;

    [SerializeField, Tooltip("The Interval in seconds after the Warning before the shot starts")]
    protected float timeAfterWarning = 1f;

    [SerializeField, Tooltip("The Interval in seconds between a Warning Starts")]
    protected float timeBetweenShots = 1f;

    [Tooltip("The target the enemy automatically looks at, leave empty for no looking")]
    public Rigidbody2D target;

    public LayerMask hitLayers;

    public bool hasDeathAnimation = false;

    protected void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        if(canShoot)
            StartCoroutine("WaitForShot");
    }

    protected void Update()
    {
        UpdateSpriteOrientation();
    }

    void FixedUpdate()
    {
        Move();
    }

    protected void UpdateSpriteOrientation() 
    {
        if(!target)
            return;
        
        if(target.position.x > rb2d.position.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    protected abstract void Move();

    protected abstract void Shoot();

    protected abstract void Warn();

    public void TakeDamage(DamageDealer damageDealer)        
    {
        this.enemyHealth = this.enemyHealth - damageDealer.GetDamage();
        if(this.enemyHealth <= 0) {
            Die();
        }
    }

    public void Die()
    {
        
        //Animations are added
        if(hasDeathAnimation)
            this.GetComponent<Animator>().SetTrigger("Death");
        else
            Destroy(this.gameObject);
    }

    IEnumerator WaitForShot() 
    {
        while(true) {

            yield return new WaitForSecondsRealtime(timeBetweenShots);
            Warn();
            yield return new WaitForSecondsRealtime(timeAfterWarning);
            Shoot();
        }
    }

}
