using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{

    public int heals = 1;
    public float healCoolDown=2.0f;
    public float LastHealed;

    protected override void OnCollide(Collider2D coll)
    {
        if (!coll.name.Equals("Player")) return;
        if(Time.time-LastHealed > healCoolDown)
        {
            LastHealed = Time.time;
            GameManager.instance.player.heal(heals);
        }
    }
}
