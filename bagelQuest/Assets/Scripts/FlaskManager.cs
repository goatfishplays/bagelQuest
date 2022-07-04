using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskManager : MonoBehaviour
{
    public static float shatterDmg = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
