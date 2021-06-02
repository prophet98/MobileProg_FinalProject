using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpShop : MonoBehaviour
{
    [SerializeField]
    private int cost;
    
    [SerializeField]
    private bool isActiveSkill = false;

    [SerializeField]
    private BaseSkill skill;

    private enum playerStat { Health, Speed, WeaponDmg };
    [SerializeField]
    private playerStat statToIncrease = playerStat.Health;
    [SerializeField]
    private float increaseAmount;

    [SerializeField]
    private Text priceText;
    private GameplayManager manager;

    private ShopManager sm;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(BuyItem);
        sm = FindObjectOfType<ShopManager>();
    }

    private void Start()
    {
        manager = FindObjectOfType<GameplayManager>();
        priceText.text = "$"+cost.ToString();
    }

    private void BuyItem()
    {
        if(manager.playerStats.playerMoney >= cost)
        {
            Debug.Log("press buy");
            CheckForSkill();
            manager.playerStats.playerMoney -= cost;
            sm.UpdateCoinsText();
            Debug.Log(manager.playerStats.playerMoney);
        } else
        {
            return;
        }
    }

    private void CheckForSkill()
    {
        if (isActiveSkill)
        {
            manager.playerStats.upperSkill = skill;
            Debug.Log("Add active skill");
        }
        else if (!isActiveSkill)
        {
            switch (statToIncrease)
            {
                case playerStat.Health:
                    manager.playerStats.playerHealth += (int)increaseAmount;
                    Debug.Log("Add health" + manager.playerStats.playerHealth);
                    break;
                case playerStat.Speed:
                    manager.playerStats.playerSpeed += increaseAmount;
                    Debug.Log("Add speed" + manager.playerStats.playerSpeed);
                    break;
                case playerStat.WeaponDmg:
                    manager.playerStats.playerWeaponDamage += (int)increaseAmount;
                    Debug.Log("Add dmg" + manager.playerStats.playerWeaponDamage);
                    break;
            }
        }
    }
}