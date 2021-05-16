
using UnityEngine;

public class BattleRewardSystem : MonoBehaviour
{
    private int CurrentMoney { get; set; }
    public bool canPassGate;

    public void RewardPlayer(int money)
    {
        CurrentMoney += money;
    }
}
