using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageTaker : MonoBehaviour
{
    public LayerMask hitLayers;

    [System.Serializable]
    public class DamageEvent : UnityEvent<DamageDealer> {

        public DamageEvent() {}

    }

    [SerializeField] public DamageEvent callback;

    private void OnTriggerEnter2D(Collider2D other) {

        if((hitLayers.value & (1 << other.gameObject.layer)) > 0)
        {
            DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
 
            if(damageDealer != null)
            {
                damageDealer.OnHit.Invoke();
                callback.Invoke(damageDealer);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {

        if((hitLayers.value & (1 << other.collider.gameObject.layer)) > 0)
        {
            DamageDealer damageDealer = other.collider.gameObject.GetComponent<DamageDealer>();
 
            if(damageDealer != null)
            {
                        Debug.Log("HHAHAHA");
                damageDealer.OnHit.Invoke();
                callback.Invoke(damageDealer);
            }
        }
    }
}
