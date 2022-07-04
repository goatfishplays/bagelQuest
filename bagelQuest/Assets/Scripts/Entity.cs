using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;

    public void hurt(float dmg)
    {
        health -= dmg;
        health = Mathf.Clamp(health, -1, maxHealth);
        if(health <= 0.00001)
        {
            print("dead");
        }
    }

}
