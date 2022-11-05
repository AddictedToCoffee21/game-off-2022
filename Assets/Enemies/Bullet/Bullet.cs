using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _timeToDespawn = 10f;

    private float _timeSinceAlive = 0f;

    private Rigidbody2D _rb2d;

    // Start is called before the first frame update


    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceAlive += Time.deltaTime;
        if(_timeSinceAlive >= _timeToDespawn) {
            Destroy(this.gameObject);
        }
    }

    public void SetRotation(float degree) {
        this.transform.eulerAngles = new Vector3(0,0, degree);
    }

    public void SetVelocity(Vector2 velocity) {
        _rb2d.velocity = velocity;
    }
}
