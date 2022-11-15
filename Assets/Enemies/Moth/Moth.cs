using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : Enemy
{   

    [Space(10)]
    public float periodLength = 0.2f;
    public float amplitude = 1f;
    public float bulletSpeed = 1f;

    private float _x = 0;
    private float _y = 0;

    private Vector2 _lastPosition = Vector2.zero;
    private Vector2 _currentPosition = Vector2.zero;

    private Vector2 _startPosition;

    public Bullet bullet;

    new void Start() 
    {
        base.Start();
        _startPosition = base.rb2d.position;
    }

    override protected void Move() 
    {
        //Moth moves in Sin Curve
        if(_x > Mathf.PI * 2)
        {
            _x = 0;
        } else 
        {
            _x += Time.fixedDeltaTime;
        }
        _y = amplitude * Mathf.Sin((1.0f/periodLength) * _x);
        _currentPosition = new Vector2(_x,_y);
        Vector2 moveDirection = _currentPosition - _lastPosition;

        float angleToPlayer = Vector2.SignedAngle(base.target.position - base.rb2d.position, Vector2.right);
        moveDirection = this.rot(moveDirection, -angleToPlayer);


        base.rb2d.velocity = moveDirection.normalized * base.enemySpeed;

        _lastPosition = _currentPosition;
    }

    override protected void Shoot() 
    {
        Bullet newBullet = Instantiate(bullet, base.rb2d.position, Quaternion.identity);
        Vector2 bulletDirection = base.target.position - base.rb2d.position;
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
