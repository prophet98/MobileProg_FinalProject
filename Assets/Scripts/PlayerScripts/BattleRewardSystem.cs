
using System;
using UnityEngine;

public class BattleRewardSystem : MonoBehaviour
{
    private int CurrentMoney { get; set; }
    public bool canPassGate;

    public void RewardPlayer(int money)
    {
        CurrentMoney += money;
    }

    private void OnDisable()
    {
        if (GameplayManager.instance!=null)
        {
            GameplayManager.instance.playerMoney = CurrentMoney;
        }
        
    }
}
