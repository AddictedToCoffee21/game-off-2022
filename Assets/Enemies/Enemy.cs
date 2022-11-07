using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class Enemy : MonoBehaviour, ITakeDamage
{

    protected Rigidbody2D rb2d;

    protected SpriteRenderer spriteRenderer;

    [SerializeField]
    protected int enemyHealth = 1;

    [SerializeField]
    protected int enemyDamage = 1;

    [SerializeField, Range(0,100)]
    protected float enemySpeed = 1;

    [SerializeField, Tooltip("The Interval in seconds after the Warning before the shot starts")]
    protected float timeAfterWarning = 1f;

    [SerializeField, Tooltip("The Interval in seconds between a Warning Starts")]
    protected float timeBetweenShots = 1f;

    [Tooltip("The target the enemy automatically looks at, leave empty for no looking")]
    public Rigidbody2D lookTarget;

    protected void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();

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
        if(!lookTarget)
            return;
        
        if(lookTarget.position.x > rb2d.position.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    protected abstract void Move();

    protected abstract void Shoot();

    protected abstract void Warn();

    public void TakeDamage(IDealDamage damageDealer)
    {
        this.enemyHealth = this.enemyHealth - damageDealer.GetDamage();
        if(this.enemyHealth <= 0) {
            Die();
        }
    }

    protected void Die()
    {
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
