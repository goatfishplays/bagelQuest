using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskManager : MonoBehaviour
{
    public static float shatterDmg = 5f;
    public static List<int> ingredCount = new List<int>();
    public static List<int> ingredList = new List<int>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Shatter(collision.gameObject);
    }

    private void Shatter(GameObject temp)
    {
        if(!temp.CompareTag("Player"))
        {
            if(temp.GetComponent<Entity>() != null)
            {
                temp.gameObject.GetComponent<Entity>().hurt(shatterDmg);
            }
            else
            {

            }
            Destroy(this.gameObject);
        }
    }
}
