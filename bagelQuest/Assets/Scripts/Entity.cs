using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float health = 50;
    [SerializeField] protected float maxHealth = 50;

    public virtual void Hurt(float dmg)
    {
        health -= dmg;
        health = Mathf.Clamp(health, -1, maxHealth);
        print("hit");
        if(health <= 0.00001)
        {
            print("dead");
        }
    }

}
