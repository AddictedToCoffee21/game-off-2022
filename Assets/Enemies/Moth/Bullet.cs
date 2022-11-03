using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float m_timeToKill = 10f;
    private float m_timeSinceAlive = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_timeSinceAlive += Time.deltaTime;
        if(m_timeSinceAlive >= m_timeToKill) {
            Destroy(this.gameObject);
        }
    }
}
