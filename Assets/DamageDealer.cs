using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageDealer : MonoBehaviour
{
    public UnityEvent OnHit;
    [SerializeField] private int damage = 1;

    public int GetDamage() 
    {
        return damage;
    }

    public void SetDamage(int damage) 
    {
        this.damage = damage;
    }
}
