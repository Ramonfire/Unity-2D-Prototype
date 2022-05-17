using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicEnemy : Mover
{
    //Experience 
    public int xpValue = 1;

    //Logic
    public float triggerLenght = 0.3f;
    public float chaseLenght = 1;
    private bool chasing;
    private bool collidingwithplayer;
    private Transform playerTransform;
    private Vector3 startingposition;


    //Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    private Animator animator;
    


    protected override void Start()
    {
        base.Start();
        //finding the player transform
        playerTransform = GameObject.Find("Player").transform;
        // playerTranform= GameManager.instance.player.transform also works

        startingposition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        hp = 2;
    }


    private void FixedUpdate()
    {
        //verifying if the player is in range
        if (Vector3.Distance(playerTransform.position, startingposition) < chaseLenght)
        {
            //if the player is close to the trigger area
            if(Vector3.Distance(playerTransform.position, startingposition) < triggerLenght)chasing=true;

            //if he is close enough
            if (chasing)
            {//moving towards the player
                //if not colliding with player we go towards him
                if (!collidingwithplayer)
                {

                    //limiting the speed of the mob
                    Vector3 position1= (playerTransform.position - transform.position).normalized;
                    position1.x *= 0.16f;
                    position1.y *= 0.16f;
                    position1.z = 0;
                    //starting the animation 
                    MovingAnimationTrigger();
                    //moving towards teh player
                    UpdateMotor(position1);
               }
            }
            else
            {//resetting position
                Vector3 position2 = startingposition - transform.position;
                position2.x *= 0.16f;
                position2.y *= 0.16f;
                position2.z = 0;
                //starting the animation 
                if (position2 != Vector3.zero) { 
                MovingAnimationTrigger();
                }
                UpdateMotor(position2);
            }
        }
        else
        {
            Vector3 position2 = startingposition - transform.position;
            position2.x *= 0.16f;
            position2.y *= 0.16f;
            position2.z = 0;
            //starting the animation 
            if (position2 != Vector3.zero)
            {
                MovingAnimationTrigger();
            }
            UpdateMotor(position2);
            chasing = false;
        }

        collidingwithplayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            else
            {
                //verifying if we colldie with the player
                if (hits[i].tag=="Fighter"&&hits[i].name=="Player")
                {
                    collidingwithplayer = true;
                }
                hits[i] = null;
            }

        }


    }



    protected override void death()
    {
        Destroy(gameObject);
        GameManager.instance.AddXp(xpValue);
        GameManager.instance.Showtext("+"+xpValue+"xp",25,Color.white,transform.position,Vector3.up,3.0f);

    }


    protected override void MovingAnimationTrigger()
    {
        animator.SetTrigger("OnMouvement");
    }

}
