using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
   //stats;

    public int hp=10;
    public int maxhp=10;
    public float pushrecoveryspeed = 0.2f;


    //Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;


    //push
    protected Vector3 pushdirection;


    //All  fighter can receive damage and die

    protected virtual void ReceiveDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hp -= dmg.damageAmount;
            pushdirection = (transform.position - dmg.origin).normalized * dmg.pushforce;


            GameManager.instance.Showtext(dmg.damageAmount.ToString(), 25,Color.red, transform.position, Vector3.zero, 2.0f);
            if (hp<=0)
            {
                hp = 0;
                death();
            }
        }
    }

    protected virtual void death()
    {

    }
}
