using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Enemy
{
    private Vector2 _startPosition;

    public Vector3 moveDirection;

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
    }

    override protected void Warn() 
    {
    }

    override protected void Move() 
    { 
        base.rb2d.velocity = (target.position - new Vector2(rb2d.position.x, rb2d.position.y)).normalized * enemySpeed * Time.fixedDeltaTime;
    }

    override protected void Shoot() 
    {
    }
}
