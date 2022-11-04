using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : Enemy
{
    private Vector2 _startPosition;

    private Vector2 _targetPosition;

    public Bullet bullet;

    new void Start() 
    {
        base.Start();
        _startPosition = base.rb2d.position;
    }

    override protected void Move() 
    {
        //Moth is stationary for now
    }

    override protected void Shoot() 
    {
        //Moth does'nt shoot for now
    }

    override protected void Warn() {
        
    }
}
