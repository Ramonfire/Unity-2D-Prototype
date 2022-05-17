using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaterMenu : MonoBehaviour
{

    //TextFields
    public Text levelText,HitpointText,CoinsText,UpgradeCostText,XpText;


    //LogicFields

    public Image weaponSprite;
    public RectTransform xpBar;


    //Weapon Upgrade
    public void OnClickUpgrade()
    {
        if (GameManager.instance.tryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    //Update character Information
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.instance.WeaponSprites[GameManager.instance.weapon.WeaponLevel];
        if (GameManager.instance.weapon.WeaponLevel == GameManager.instance.weaponPrices.Count)
        {
            UpgradeCostText.text = "MAX";
        }
        else { 
        UpgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.WeaponLevel].ToString(); 
              }

        //Meta
        HitpointText.text = GameManager.instance.player.hp.ToString();
        CoinsText.text = GameManager.instance.coin.ToString();
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();

        //XpBar
        int currentlevel = GameManager.instance.GetCurrentLevel();
        if (currentlevel==GameManager.instance.xptable.Count)
        {
            XpText.text = GameManager.instance.xp.ToString() + " Total experience points";//display Total xp
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currentlevel-1);
            int CurrLevelXp = GameManager.instance.GetXpToLevel(currentlevel);


            int Difference = CurrLevelXp - prevLevelXp;
            int CurrentxpIntoLevel =  GameManager.instance.xp - prevLevelXp;

            float completionRatio = (float)CurrentxpIntoLevel /(float) Difference;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            XpText.text = CurrentxpIntoLevel.ToString() + "/" + Difference.ToString();
        }




    }
}
