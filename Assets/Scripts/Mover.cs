using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D wallhit;
    protected float yspeed = 1.0f;
    protected float xspeed = 1.0f;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

  

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x*xspeed,input.y*yspeed,0);


        //swap sprite direction , facing the right direction depending on input
        if (moveDelta.x > 0) transform.localScale = Vector3.one;
        else if (moveDelta.x < 0) transform.localScale = new Vector3(-1, 1, 1);

        //Add push vector if any
        moveDelta += pushdirection;

        //reduce the push verctor very frame
        pushdirection = Vector3.Lerp(pushdirection, Vector3.zero, pushrecoveryspeed);



        //movement
        /***************************************************************************************************************************/

        //testing colliding on Y axis
        wallhit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y)
                                    , Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        //no wall then we can move
        if (wallhit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        /**********************************************************************************************************************************/
        //testing colliding on x axis
        wallhit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0)
                                    , Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));


        //no wall then we can move
        if (wallhit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }


    protected virtual void MovingAnimationTrigger()
    {

    }
}
