using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : Enemy
{

    private Vector2 _startPosition;

    [SerializeField]
    private float _verticalMove;

    private Vector2 _targetPosition;

    public GameObject bullet;

    new void Start() 
    {
        base.Start();
        _startPosition = base.rb2d.position;
    }

    override protected void Move() 
    {
        if(_targetPosition == Vector2.zero) 
        {
            _targetPosition = base.rb2d.position;
            _targetPosition.y = _startPosition.y + _verticalMove;
        }

        if(base.rb2d.position.y >= _startPosition.y + _verticalMove) 
        {
            _targetPosition.y = _startPosition.y - _verticalMove;
        }
        else if(base.rb2d.position.y <= _startPosition.y - _verticalMove)
        {
            _targetPosition.y = _startPosition.y + _verticalMove;
        }

        base.rb2d.position = Vector2.MoveTowards(base.rb2d.position, _targetPosition, base.enemySpeed * Time.fixedDeltaTime);
        
    }

    override protected void Shoot() 
    {
        GameObject newBullet = Instantiate(bullet, base.rb2d.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = (base.target.position - base.rb2d.position).normalized * 3;
    }
}
