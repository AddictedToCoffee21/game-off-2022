using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{

    protected Rigidbody2D rb2d;

    [SerializeField]
    protected int enemyHealth = 1;

    [SerializeField]
    protected int enemyDamage = 1;

    [SerializeField, Range(1,10)]
    protected float enemySpeed = 1;

    //The Target the Enemy chases
    public Rigidbody2D target;

    void Start()
    {
        this.rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected void Move()
    {
        this.rb2d.velocity = (target.position - this.rb2d.position).normalized * enemySpeed;
    }

    protected void TakeDamage(int damage)
    {
        this.enemyHealth = this.enemyHealth - damage;
        if(this.enemyHealth <= 0) {
            Die();
        }
    }

    protected void Die() {
        Destroy(this.gameObject);
    }

}
