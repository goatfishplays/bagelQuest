using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTran;
    [SerializeField] private Transform playerTran;
    [SerializeField] private float deadZoneXPos = 5f;
    [SerializeField] private float deadZoneXNeg = -5f;
    [SerializeField] private float deadZoneYPos = 10f;
    [SerializeField] private float deadZoneYNeg = -10f;
    [SerializeField] private bool deadFollowOn = false;
    private Vector2 camPos;
    private Vector2 playPos;

    // Update is called once per frame
    private void Update()
    {
        camPos = cameraTran.position;
        playPos = playerTran.position;
        
        if(deadFollowOn)
        {
            deadFollow();
        }
        else
        { 
            standardFollow();
        }

        cameraTran.position = new Vector3(camPos.x, camPos.y, -10);
    }

    private void standardFollow()
    {
        camPos = new Vector2(playPos.x, playPos.y+2);
    }

    private void deadFollow()
    {
        //Horiz
        if (playPos.x - camPos.x > deadZoneXPos)
        {
            camPos.x = playPos.x - deadZoneXPos;
        }
        else if(playPos.x - camPos.x < deadZoneXNeg)
        {
            camPos.x = playPos.x - deadZoneXNeg;
        }
        //Vert
        if (playPos.y - camPos.y > deadZoneYPos)
        {
            camPos.y = playPos.y - deadZoneYPos;
        }
        else if (playPos.y - camPos.y < deadZoneYNeg)
        {
            camPos.y = playPos.y - deadZoneYNeg;
        }
    }
}
