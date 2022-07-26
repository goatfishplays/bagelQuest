using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected float maxHVelo = 1;
    [SerializeField] protected float accelTicks = 1;
    [SerializeField] protected float moveTime = 0;
    [SerializeField] protected float noticeRange = 1;
    [SerializeField] protected float followRange = 5;
    [SerializeField] protected float stopRate = 2;
    private bool following = false;
    [SerializeField] protected float stopFollowDist = .25f;
    protected int dir = 1;
    protected int lastTickDir;


    protected Rigidbody2D rb;
    private Vector2 velo;
    static public Rigidbody2D playerRb;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        // movement
        lastTickDir = dir;
        velo = rb.velocity;
        if (Mathf.Abs(rb.position.x - playerRb.position.x) < noticeRange)
        {
            following = true;
        }
        else if(Mathf.Abs(rb.position.x - playerRb.position.x) > followRange)
        {
            following = false;
            velo.x /= stopRate;
            if (velo.x < .001)
            {
                velo.x = 0;
            }
            moveTime = 0;
        }

        if(following)
        {
            UpdateDir();
            if(dir == 1)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                transform.rotation = new Quaternion(0,180,0,0);
            }    
            TrackBasic();
        }

        rb.velocity = velo;
    }

    protected void UpdateDir()
    {
        if(rb.position.x > playerRb.position.x)
        {
            dir = -1;
        }
        else
        {
            dir = 1;
        }
    }

    protected void TrackBasic()
    {
        if(Mathf.Abs(rb.position.x - playerRb.position.x) < stopFollowDist)
        {
            velo.x /= stopRate;
            if(velo.x < .001)
            {
                velo.x = 0;
            }
            moveTime = 0;
            return;
        }
        velo.x = maxHVelo * moveTime * dir / (float)accelTicks;
        if (moveTime < accelTicks)
        {
            if (dir == lastTickDir)
            {
                moveTime++;
            }
            else
            {
                moveTime = 0;
            }
        }
    }

}
