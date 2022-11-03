using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moth : Enemy
{

    private Vector2 m_startPosition;

    [SerializeField]
    private float m_verticalMove;

    private Vector2 m_targetPosition;

    public GameObject bullet;

    new void Start() 
    {
        base.Start();
        m_startPosition = base.rb2d.position;
    }

    override protected void Move() 
    {
        if(m_targetPosition == Vector2.zero) 
        {
            m_targetPosition = base.rb2d.position;
            m_targetPosition.y = m_startPosition.y + m_verticalMove;
        }

        if(base.rb2d.position.y >= m_startPosition.y + m_verticalMove) 
        {
            m_targetPosition.y = m_startPosition.y - m_verticalMove;
        }
        else if(base.rb2d.position.y <= m_startPosition.y - m_verticalMove)
        {
            m_targetPosition.y = m_startPosition.y + m_verticalMove;
        }

        base.rb2d.position = Vector2.MoveTowards(base.rb2d.position, m_targetPosition, base.enemySpeed * Time.fixedDeltaTime);
        
    }

    override protected void Shoot() 
    {
        GameObject newBullet = Instantiate(bullet, base.rb2d.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = (base.lookTarget.position - base.rb2d.position).normalized * 3;
    }
}
