using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    private PlayerInput pI;
    private BoxCollider2D bC;
    private Animator ani;
    private Mouse mou;
    private Transform cursor;
    private Inputs inputs;
    [SerializeField] private int moveDir = 1;
    [SerializeField] private float dashDist = 5.0f;
    [SerializeField] private int dashTicks = 9;
    [SerializeField] private int dashCD = 60;
    [SerializeField] private int dashCDTime = 0;
    [SerializeField] private float jump = 15.0f;
    [SerializeField] private float topSpeed = 10f;
    [SerializeField] private int accelTicks = 12;
    [SerializeField] private int moveTime = 0;
    [SerializeField] private bool groundCheck = false;
    [SerializeField] private int lastTimeGroundedCap = 30;
    [SerializeField] private int lastTimeGrounded = 0;
    [SerializeField] private float throwForce = 5f;
    private bool isDashing = false;
    private bool isJumping = false;
    private int dashTime = 0;
    private int lastTickDir = 1;
    private int movingDir = 1;
    private Vector2 mPos;
    private Vector2 inputsV = new Vector2(0,0);
    [SerializeField] private GameObject flask;
    [SerializeField] private GameObject flask2;
    public static int flaskType = 1;
    public static Color32 flaskColor = new Color32(0, 0, 0, 255);



    private void Awake()
    {
        Cursor.visible = false;

        cursor = transform.GetChild(1);
        rb = GetComponent<Rigidbody2D>();
        pI = GetComponent<PlayerInput>();
        bC = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        
        Enemy.playerRb = rb;

        //class created from the InputSystem, allows for use in code
        inputs = new Inputs();
        //This subscribes to the performed event producer, whenever performed sends out an event Movement_performed function is called
        inputs.Player.Enable();
        inputs.Player.Movement.performed += Movement_performed;
        inputs.Player.Dash.performed += Dash_performed;
        inputs.Player.Throw.performed += Throw_performed;
    }

    private void FixedUpdate()
    {

        lastTickDir = moveDir;
        //gets the Vector2 of Movement
        inputsV = inputs.Player.Movement.ReadValue<Vector2>();
        float dashI = inputs.Player.Dash.ReadValue < float>();
        Vector2 velo = rb.velocity;
        Vector2 pos = transform.position;

        //Direction & Rotation
        if (inputsV.x > 0 && !isDashing)
        {
            moveDir = 1;
        }
        else if (inputsV.x < 0 && !isDashing)
        {
            moveDir = -1;
        }
        if (moveDir == 1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            /*if (lastTickDir == -1)
            {
                pos.x -= transform.localScale.x * bC.offset.x;
            }*/
        }
        else
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            /*if (lastTickDir == 1)
            {
                pos.x += transform.localScale.x * bC.offset.x;
            }*/
        }

        //dash
        if (dashTime > 0)
        {
            isDashing = true;
        }
        else
        {
            isDashing = false;
        }
        if(isDashing && dashTime <= dashTicks)
        {
            velo.x = moveDir *  dashDist / (dashTicks / 60.0f);
            dashTime++;
        }
        if(dashI == 1 && !isDashing && dashCDTime <= 0)
        {
            dashTime++;
            velo.x = moveDir * dashDist / (dashTicks / 60.0f);
            moveTime = 0;
        }
        if (dashTime > 1 && (int)velo.x == 0)
        {
            dashTime = 0;
            dashCDTime = dashCD;
        }
        if(dashTime > dashTicks)
        {
            dashTime = 0;
            velo.x = 0;
            if((int)inputsV.x != 0)
            {
                moveTime = accelTicks;
                velo.x = topSpeed * moveDir;
            }
            dashCDTime = dashCD;
        }
        if(dashCDTime > -1)
        {
            dashCDTime--;
        }
        

        //Jump
        groundCheck = Physics2D.OverlapBox(new Vector2(pos.x + (transform.localScale.x*bC.offset.x*moveDir), pos.y + bC.offset.y*transform.localScale.y - 0.05f), new Vector2(bC.size.x - 0.025f, bC.size.y - 0.1f), 0, LayerMask.GetMask("Ground"));
        if (!groundCheck)
        {
            lastTimeGrounded++;
            if((int)inputsV.y == 0 && velo.y > 0)
            {
                // if ever need replace this probs just get a counter for time force was applied/somehow use last time grounded and multiply by -addforce jump
                velo.y = 0;
            }
        }
        else
        {
            lastTimeGrounded = 0;
        }
        if((int)inputsV.y != 1)
        {
            isJumping = false;
        }
        if ((int)inputsV.y == 1 && lastTimeGrounded < lastTimeGroundedCap && velo.y <= 0.01 && !isJumping)
        {
            isJumping = true;
            lastTimeGrounded = lastTimeGroundedCap;
            /*velo.y += jump;*/
            rb.AddForce(Vector2.up * jump);
        }

        if (!isDashing)
        {
            //horiz move
       /*     velo.x = inputsV.x * topSpeed * moveTime/(float)accelTicks;*/
            
            if(Physics2D.OverlapBox(new Vector2(pos.x + (transform.localScale.x * bC.offset.x * moveDir), pos.y + bC.offset.y * transform.localScale.y), new Vector2(bC.size.x, bC.size.y - 0.025f), 0, LayerMask.GetMask("Ground")))
            {
                moveTime = 0; 
            }


            if ((int)inputsV.x == 0 || moveDir != lastTickDir)
            {
                if(moveTime != 0)
                {
                    rb.AddForce(new Vector2(-movingDir * topSpeed * rb.mass * moveTime / (accelTicks / 60.0f), 0));
                    print("a: " + lastTickDir);
                    print("a: " + (-movingDir * topSpeed * rb.mass * moveTime / (accelTicks / 60.0f)));
                }
                moveTime = 0;
            }
            else if(moveTime < accelTicks)
            {
                if((int)inputsV.x == lastTickDir)
                { 
                    
                    moveTime++;
                    print("b: " + lastTickDir);
                    rb.AddForce(new Vector2(inputsV.x * topSpeed * rb.mass / (accelTicks / 60.0f), 0));
                    print("b: " + (moveDir * topSpeed * rb.mass / (accelTicks / 60.0f)));
                    movingDir = moveDir;

                }
                /*else
                {
                    if (moveTime != 0)
                    {
                        print("c: " + lastTickDir);
                        rb.AddForce(new Vector2(-movingDir * topSpeed * rb.mass * moveTime / (accelTicks / 60.0f), 0));
                        print("c: " + (-movingDir * topSpeed * rb.mass * moveTime / (accelTicks / 60.0f)));
                    }
                    moveTime = 0;
                }*/
                
            }
            
        }
        rb.velocity = velo;

        //Animation
        ani.SetBool("isDashing", isDashing);

        transform.position = pos;

    }

    private void Update()
    {

        //Throwing
        mPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        cursor.position = mPos;

    }






    private void Dash_performed(InputAction.CallbackContext cont)
    {

/*        rb.AddForce(new Vector2(moveDir * dash, 0), ForceMode2D.Force);*/
    }

    //callback context is the thing that gets when event, func called whenever event happen
    private void Movement_performed(InputAction.CallbackContext cont)
    {
/*        Debug.Log(cont);
        Vector2 inputsV = cont.ReadValue<Vector2>();
        if (inputsV.x > 0)
        {
            moveDir = 1;
        }
        else if (inputsV.x < 0)
        {
            moveDir = -1;
        }
        inputsV.x *= speed;
        inputsV.y *= jump;
        rb.AddForce(inputsV, ForceMode2D.Force);*/
    }

    private void Throw_performed(InputAction.CallbackContext cont)
    {
        GameObject nFlask;
        float fx = throwForce * cursor.localPosition.x / Mathf.Sqrt(Mathf.Pow(cursor.localPosition.x, 2) + Mathf.Pow(cursor.localPosition.y, 2));
        float fy = throwForce * cursor.localPosition.y / Mathf.Sqrt(Mathf.Pow(cursor.localPosition.x, 2) + Mathf.Pow(cursor.localPosition.y, 2));
        float rotation = -500f;
        Rigidbody2D nFlaskRb;
        if (transform.rotation.y != 0)
        {
            fx *= -1;
        }
        switch(flaskType)
        {
            case 2:
                nFlask = Instantiate(flask2, transform.GetChild(0).position, new Quaternion(0, 0, 0, 0));
                rotation = -65;
                fx *= 3;
                fy *= 3;
                nFlaskRb = nFlask.GetComponent<Rigidbody2D>();
                int rotDirCheck = 0;
                if(fx < 0)
                {
                    rotDirCheck = 180;
                }
                nFlaskRb.rotation = rotDirCheck + (90 +  Mathf.Atan(fy/fx) * 180 / Mathf.PI);
                break;
            default:
                nFlask = Instantiate(flask, transform.GetChild(0).position, new Quaternion(0, 0, 0, 0));
                nFlaskRb = nFlask.GetComponent<Rigidbody2D>();
                break;
        }
        
        nFlask.transform.GetChild(0).GetComponent<SpriteRenderer>().color = flaskColor;
        nFlaskRb.velocity = rb.velocity;
        nFlaskRb.AddTorque(rotation);
        nFlaskRb.AddForce(new Vector2(fx, fy));
    }

}
