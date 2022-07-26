using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technoblade : Enemy
{
    private bool canMove = true;
    [SerializeField] private int atkCooldown = 600;
    [SerializeField] private GameObject swordAtk1PreFab;



    protected override void FixedUpdate()
    {
        if(canMove)
        {
            base.FixedUpdate();
        }

        if(atkCooldown > 0)
        {
            atkCooldown--;
        }
        else
        {
            SwordAtk1();
        }
    }

    private void SwordAtk1()
    {
        GameObject atk  = Instantiate(swordAtk1PreFab, new Vector3(0.7f,0.4f,0), new Quaternion(0, 0, 0, 0), transform);
        atk.transform.localPosition = new Vector3(0.7f, 0.4f, 0);
        atkCooldown = 180;
    }
}
