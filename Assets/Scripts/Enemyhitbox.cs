using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhitbox : Collidable
{
    public int damage=1;
    public float pushforce = 2.0f;


    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag=="Fighter" && coll.name =="Player")
        {
            //Creating  ane damage object and sending it  to the player
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushforce = pushforce
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
}
