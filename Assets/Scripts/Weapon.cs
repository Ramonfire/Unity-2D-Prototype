using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage Structure

    public int[] damagepoint = {1, 2, 3, 4, 5, 6, 7, 6, 7};
    public float[] pushforce = { 2.0f, 2.2f, 2.4f, 2.6f, 2.7f, 2.8f, 2.9f, 3.0f, 3.0f };


    //Upgrade section


    public int WeaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //swing

    private float cooldown = 0.5f;
    private float lastswing;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        base.Start();
       
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time-lastswing>cooldown)
            {
                lastswing = Time.time;
                Swing();
            }
        }
    }


    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag=="Fighter")
        {
            if (coll.name == "Player") return;


            //creating a damage object and sending the data to the fighter who got hit
            Damage dmg = new Damage {
                damageAmount = damagepoint[WeaponLevel],
                origin = transform.position,
                pushforce = pushforce[WeaponLevel]
            };
            //sending the information to the monster to remove hps
            coll.SendMessage("ReceiveDamage", dmg);


        }
        
    }

    public void UpgradeWeapon()
    {
        WeaponLevel++;
        spriteRenderer.sprite = GameManager.instance.WeaponSprites[WeaponLevel];
        

    }

    private void Swing()
    {
        animator.SetTrigger("Swing");
    }

    public void SetWeaponLevel(int level)
    {
        WeaponLevel=level;
        spriteRenderer.sprite = GameManager.instance.WeaponSprites[WeaponLevel];
    }

}
