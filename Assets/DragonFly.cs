using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFly : Enemy
{
    public float quickMoveSpeed = 800;
    public float maxQuickMoveAngle = 120;

    public float stayDistance = 6f;
    public float stayTime = 1f;

    private bool hasReachedPlayerOnce = false;
    private bool shouldQuickMove = false;
    private bool isWaiting = false;
    private float bufferDistance = 0.2f;

    private Vector2 movePoint = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    override protected void Warn() 
    {
    }

    override protected void Move() 
    {
        if(hasReachedPlayerOnce)
        {
            if(shouldQuickMove) 
            {

                if(base.rb2d.velocity == Vector2.zero) 
                {
                    float xDist = base.rb2d.position.x - target.position.x;
                    float yDist = base.rb2d.position.y - target.position.y;

                    Vector2 currentDir = new Vector2(xDist, yDist).normalized;

                    float degChange = Random.Range(-1f,1f) * maxQuickMoveAngle * Mathf.Deg2Rad;
                    float cos = Mathf.Cos(degChange);
                    float sin = Mathf.Sin(degChange);

                    float _x2 = currentDir.x * cos - currentDir.y * sin;
                    float _y2 = currentDir.x * sin + currentDir.y * cos;

                    movePoint = target.position + new Vector2(_x2, _y2) * (stayDistance - bufferDistance);

                }
                
                //Set Velocity in Direction of that Point
                base.rb2d.velocity = (movePoint - new Vector2(base.rb2d.position.x, base.rb2d.position.y)).normalized * quickMoveSpeed * Time.fixedDeltaTime;

                
                if(Vector2.Distance(base.rb2d.position, movePoint) < bufferDistance) {
                    base.rb2d.velocity = Vector2.zero;
                    shouldQuickMove = false;
                }
                
            }
            else
            {
                
                if(Vector2.Distance(target.position, new Vector2(base.rb2d.position.x, base.rb2d.position.y)) > stayDistance ) 
                {
                    shouldQuickMove = true;
                }

                if( Vector2.Distance(target.position, new Vector2(base.rb2d.position.x, base.rb2d.position.y)) <= stayDistance) {
                    base.rb2d.velocity = Vector2.zero;
                    StartCoroutine("WaitForQuickMove");
                }
            }
        }
        else
        {
            base.rb2d.velocity = (target.position - new Vector2(base.rb2d.position.x, base.rb2d.position.y)).normalized * enemySpeed * Time.fixedDeltaTime;

            if( Vector2.Distance(target.position, new Vector2(base.rb2d.position.x, base.rb2d.position.y)) <= stayDistance) {
                hasReachedPlayerOnce = true;
                base.rb2d.velocity = Vector2.zero;
            }
        }

    }

    override protected void Shoot() 
    {
    }

    IEnumerator WaitForQuickMove() {
        if(!isWaiting) 
        {
            isWaiting = true;
            yield return new WaitForSecondsRealtime(stayTime);
            shouldQuickMove = true;
            isWaiting = false;
        }   
    }

}
