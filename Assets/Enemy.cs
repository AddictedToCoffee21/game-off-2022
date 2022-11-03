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

    [SerializeField, Range(0,10)]
    protected float enemySpeed = 1;

    [SerializeField]
    protected float timeBetweenShots = 1f;

    //The Target the Enemy chases
    public Rigidbody2D target;

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

    void UpdateSpriteOrientation() 
    {

        if(target.position.x > rb2d.position.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }

    protected abstract void Move();

    protected abstract void Shoot();

    protected void TakeDamage(int damage)
    {
        this.enemyHealth = this.enemyHealth - damage;
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
            Shoot();
        }
    }

}
