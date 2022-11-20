using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public bool destroyOnHit = true;

    [SerializeField]
    private float _timeToDespawn = 10f;
    private float _timeSinceAlive = 0f;

    private Rigidbody2D _rb2d;

    public PlayerController player = null;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //TODO: Fade out for Playerbullet
        _timeSinceAlive += Time.deltaTime;
        if(_timeSinceAlive >= _timeToDespawn) {
           DestroyBullet();
        }
    }

    public void SetRotation(float degree) {
        this.transform.eulerAngles = new Vector3(0,0, degree);
    }

    public void SetVelocity(Vector2 velocity) {
        _rb2d.velocity = velocity;
    }

    public void DestroyBullet() {
        Destroy(this.gameObject);
    }

    public void SetDespawnTime(float time) {
        _timeToDespawn = time;
    }

    public int GetDamage() {
        return damage;
    }
}
