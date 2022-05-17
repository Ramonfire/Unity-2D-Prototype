using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{

    public Sprite emptyChest;
    public int coin = 5;
    protected override void OnCollect()
    {
        if (!collected) {
            base.OnCollect();
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.Showtext("+" + coin + " coins", 25, Color.yellow, transform.position, Vector3.up * 50, 3.0f);
            GameManager.instance.coin += 5;
        }
    }
}
