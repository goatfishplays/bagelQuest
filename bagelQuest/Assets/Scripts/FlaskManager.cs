using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskManager : MonoBehaviour
{
    public static float shatterDmg = 5f;
    public static float[] atkComp = {};

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Shatter(collision.gameObject);
    }

    private void Shatter(GameObject temp)
    {
        print(HFuncs.StrArray(atkComp));
        if(!temp.CompareTag("Player") && !temp.CompareTag("FlaskFilter"))
        {
            if(temp.GetComponent<Entity>() != null)
            {
                temp.gameObject.GetComponent<Entity>().Hurt(shatterDmg);
            }
            else
            {

            }
            Destroy(this.gameObject);
        }
    }

    private void FlaskEff()
    {
        
    }
}
