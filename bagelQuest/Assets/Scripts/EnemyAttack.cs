using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected float dmg = 10;
    [SerializeField] protected int atkTime = -1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hit(collision.gameObject);
    }

    private void Hit(GameObject temp)
    {
        if (temp.CompareTag("Player"))
        {
            if (temp.GetComponent<Entity>() != null)
            {
                temp.gameObject.GetComponent<Entity>().Hurt(dmg);
            }
            else
            {

            }
/*            Destroy(this.gameObject);*/
        }
    }

    private void FixedUpdate()
    {
        if(atkTime > 0)
        {
            atkTime--;
        }
        else if(atkTime == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
