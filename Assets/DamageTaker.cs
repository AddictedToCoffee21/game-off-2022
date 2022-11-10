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
}
