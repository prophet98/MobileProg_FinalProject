
using System;
using TMPro;
using UnityEngine;

public class BattleRewardSystem : MonoBehaviour
{
    private int CurrentMoney { get; set; }
    private float Score { get; set; }
    public bool canPassGate;
    public event Action UnlockDoors;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private float scoreMultiplier = 2f;

    public void RewardPlayer(int money)
    {
        CurrentMoney += money;
        Score = CurrentMoney * scoreMultiplier;
        scoreText.text = "SCORE: " + Score.ToString();
        UnlockDoors?.Invoke();
    }

    private void OnDisable()
    {
        if (GameplayManager.instance!=null)
        {
            GameplayManager.instance.playerStats.playerMoney += CurrentMoney;
        }
        
    }
}
