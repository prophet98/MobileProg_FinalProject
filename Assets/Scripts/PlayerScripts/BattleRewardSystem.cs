
using System;
using UnityEngine;

public class BattleRewardSystem : MonoBehaviour
{
    private int CurrentMoney { get; set; }
    public bool canPassGate;
    public event Action UnlockDoors;

    public void RewardPlayer(int money)
    {
        CurrentMoney += money;
        UnlockDoors?.Invoke();
    }

    private void OnDisable()
    {
        if (GameplayManager.instance!=null)
        {
            //GameplayManager.instance.playerStats.playerMoney = CurrentMoney;
        }
        
    }
}
