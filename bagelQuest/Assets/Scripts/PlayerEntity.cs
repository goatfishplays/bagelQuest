using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    [SerializeField] private int iFrameMax = 60;
    [SerializeField] private int iFrameCur = 0;

    public override void Hurt(float dmg)
    {
        if(iFrameCur == 0)
        {
            base.Hurt(dmg);
            iFrameCur = iFrameMax;
        }
    }

    public void HurtNoIFrames(float dmg)
    {
        base.Hurt(dmg);
    }

    private void FixedUpdate()
    {
        if (iFrameCur > 0)
        {
            iFrameCur--;
        }
    }
}
