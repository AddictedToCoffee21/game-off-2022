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

    public GameObject bullet;

    public int shotCount = 8;

    new void Start() 
    {
        base.Start();
        _startPosition = base.rb2d.position;

        //Set look Direction
        if(moveDirection == MoveDirection.right) {
            base.spriteRenderer.flipX = true;
        }
    }

    override protected void Move() 
    {
        base.rb2d.velocity = new Vector2((int) moveDirection, 0) * enemySpeed * Time.fixedDeltaTime;
    }

    override protected void Shoot() 
    {

        float angleIncrement = 360f / (float) shotCount;

        for(float currentAngle = 0f; currentAngle <=360; currentAngle += angleIncrement) 
        {
            GameObject newBullet = Instantiate(bullet, base.rb2d.position, Quaternion.identity);
            Vector2 bulletDirection = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));
            newBullet.GetComponent<Rigidbody2D>().velocity = bulletDirection.normalized;
        }

    }
}
