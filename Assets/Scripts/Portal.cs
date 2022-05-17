using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{

    public string scene;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            // saving state
            GameManager.instance.SaveState();
            //teleport player
            SceneManager.LoadScene(scene);
          

        }
    }
}
