using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    protected override void Start()
    {
        base.Start();
        DontDestroyOnLoad(gameObject);
    }


    private void FixedUpdate()
    {
        //Reset the move delta

        moveDelta = Vector3.zero;

        //getting input
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0)); 



    }

    internal void heal(int heals)
    {
        this.hp += heals;
        if (hp > maxhp)
        {
            hp = maxhp;
            GameManager.instance.Showtext("MAX", 25, Color.green, transform.position, Vector3.up * 20, 2.0f);
        }
        else
        {
            GameManager.instance.Showtext("+" + heals.ToString() + " hp", 25, Color.green, transform.position, Vector3.up*20, 2.0f);
        }
    }

    public void OnLevelUp()
    {
        maxhp++;
        hp = maxhp;
    }

    public void SetLevel(int level)
    {
        for(int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
}
