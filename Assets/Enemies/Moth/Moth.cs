using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : Enemy
{   
    private float x = 0;
    private float y = 0;

    private Vector2 lastPosition = Vector2.zero;
    private Vector2 currentPosition = Vector2.zero;

    public float periodLength = 0.2f;
    public float amplitude = 1f;
    public float bulletSpeed = 1f;

    private Vector2 _startPosition;
    private Vector2 _playerTarget;

    public Bullet bullet;

    new void Start() 
    {
        base.Start();
        _startPosition = base.rb2d.position;
        
        //TODO: Replace with better Solution
        _playerTarget = GameObject.Find("Player").GetComponent<Rigidbody2D>().position;
        base.target = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    override protected void Move() 
    {
        //Moth moves in Sin Curve
        if(x > Mathf.PI * 2)
        {
            x = 0;
        } else 
        {
            x += Time.fixedDeltaTime;
        }
        y = amplitude * Mathf.Sin((1.0f/periodLength) * x);
        currentPosition = new Vector2(x,y);
        Vector2 moveDirection = currentPosition - lastPosition;

        //Vector to Player
        _playerTarget = GameObject.Find("Player").GetComponent<Rigidbody2D>().position;
        float angleToPlayer = Vector2.SignedAngle(_playerTarget - base.rb2d.position, Vector2.right);
        Debug.Log(angleToPlayer);
        moveDirection = this.rot(moveDirection, -angleToPlayer);


        base.rb2d.velocity = moveDirection.normalized * base.enemySpeed;

        lastPosition = currentPosition;
    }

    override protected void Shoot() 
    {
        Bullet newBullet = Instantiate(bullet, base.rb2d.position, Quaternion.identity);
        Vector2 bulletDirection = _playerTarget - base.rb2d.position;
        newBullet.SetVelocity(bulletDirection.normalized * bulletSpeed);
    }

    override protected void Warn() {
        
    }

    private Vector2 rot(Vector2 v, float degrees) {
         float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
         float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
         
         float tx = v.x;
         float ty = v.y;
         v.x = (cos * tx) - (sin * ty);
         v.y = (sin * tx) + (cos * ty);
         return v;
     }
}
