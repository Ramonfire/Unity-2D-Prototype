using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookat;
    public float limitx = 0.15f;
    public float limity = 0.05f;

    private void Start()
    {
        lookat = GameObject.Find("Player").transform;
    }
    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;


        float deltax = lookat.position.x - transform.position.x;
        float deltay = lookat.position.y - transform.position.y;

        //verifying wether we are wihting the camera limit
        if(deltax>limitx || deltax < -limitx)
        {
            if(transform.position.x < lookat.position.x)
            {
                delta.x = deltax - limitx;
            }
            else
            {
                delta.x = deltax + limitx;
            }
        }


        //verifying wether we are wihting the camera Y axis limit
        if (deltay > limity || deltay < -limity)
        {
            if (transform.position.y < lookat.position.y)
            {
                delta.y = deltay - limity;
            }
            else
            {
                delta.y = deltay + limity;
            }
        }


        //moving the camera

        transform.position += new Vector3(delta.x, delta.y, 0);


    }

}
