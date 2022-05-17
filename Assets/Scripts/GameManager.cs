using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance!=null)
        {
            Destroy(player.gameObject);
            Destroy(floatingText.gameObject);
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }
 

    //Ressources;
    public List<Sprite> playerSprites;
    public List<Sprite> WeaponSprites;
    public List<int> weaponPrices;
    public List<int> xptable;



    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingText;

    //public Weapon weapon....

    //Logic

    public int coin;
    public int xp;



    public void Showtext(string msg, int fontsize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingText.show(msg, fontsize, color, position, motion, duration);
    }

    //Upgrade Weapon

    public bool tryUpgradeWeapon()
    {
        //is the weapon Maxed?
        if (weaponPrices.Count <= weapon.WeaponLevel)
        {
            return false;
        }
        if (coin >= weaponPrices[weapon.WeaponLevel])
        {
            coin -= weaponPrices[weapon.WeaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //Experience and levels system

    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        
            while (xp>= add)
            {
                add += xptable[r];
                r++;
            //verifying max level
            if (r == xptable.Count)
            {
                return r;
            }
        }

            return r;

        

    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r<level)
        {
            xp += xptable[r];
            r++;
        }

        return xp;
    }

    public void AddXp(int Experience)
    {
        int CurrentLevel = GetCurrentLevel();
        xp += Experience;

        if (CurrentLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        Debug.Log("LevelUp");
        player.OnLevelUp();
    }





    //saving and loading game state
    /*
     * INT hp
     * INT coins
     * INT xp
     * INT weaponLVL
     */
    public void SaveState() {
        string s = "";
        //saving weapon level
        s += weapon.WeaponLevel + "|";
        //saving coins
        s += coin.ToString() + "|";
        //saving xp
        s += xp.ToString() + "|";
        //saving hp
        s += GameManager.instance.player.hp.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s,LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        //deviding the saved data into a table where we can treat it and load it
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // changing weapon lvl 
        weapon.WeaponLevel = int.Parse(data[0]);
        GameManager.instance.weapon.SetWeaponLevel(int.Parse(data[0]));
        
        //changing coin ammount
        coin = int.Parse(data[1]);
        //chaning xp
        xp = int.Parse(data[2]);
        //setting player level for hp
        if (GetCurrentLevel() != 1) 
        { 
            player.SetLevel(GetCurrentLevel());
        }
        //changing players health
        GameManager.instance.player.hp = int.Parse(data[3]);

        player.transform.position = Vector3.zero;

    }



}
