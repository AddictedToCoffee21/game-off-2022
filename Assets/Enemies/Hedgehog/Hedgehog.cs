using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hedgehog : Enemy
{
    public enum MoveDirection {
        left = -1,
        right = 1
    };

    private Vector2 _startPosition;

    public MoveDirection moveDirection = MoveDirection.left;

    private Vector2 _targetPosition;

    public Bullet bullet;

    public int shotCount = 8;

    private bool _isWarning = false;
    
    private Animator _animator;

    [Tooltip("The sprite to be shown shortly before attack")]
    public Sprite dangerSprite;

    new void Start() 
    {
        base.Start();
        _startPosition = base.rb2d.position;
        _animator = GetComponent<Animator>();

        //Set look Direction
        if(moveDirection == MoveDirection.right) {
            base.spriteRenderer.flipX = true;
        }
    }

    override protected void Warn() 
    {
        _isWarning = true;
        _animator.SetBool("isWarning", _isWarning);
    }

    override protected void Move() 
    {
        if(!_isWarning)
            base.rb2d.velocity = new Vector2((int) moveDirection, 0) * enemySpeed * Time.fixedDeltaTime;
        else
            base.rb2d.velocity = Vector2.zero;
    }

    override protected void Shoot() 
    {
        float angleIncrement = 360f / (float) shotCount;

        for(float currentAngle = 0f; currentAngle <=360; currentAngle += angleIncrement) 
        {
            Bullet newBullet = Instantiate(bullet, base.rb2d.position, Quaternion.identity);
            Vector2 bulletDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));
            newBullet.SetVelocity(bulletDirection * 5);
        }
        _isWarning = false;
        _animator.SetBool("isWarning", _isWarning);
    }
}
