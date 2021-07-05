using System;
using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class BattleRewardSystem : MonoBehaviour
{
    private int CurrentMoney { get; set; }
    private float Score { get; set; }
    public bool canPassGate;
    public event Action UnlockDoors;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI moneyText;

    [SerializeField] private float scoreMultiplier = 2f;
    [SerializeField] private float pointsToReach = 10000f;
    [SerializeField] private int prize = 1000;

    public delegate void ReachMaxPoints();

    public static event ReachMaxPoints OnWinPoints;

    private void Start() //setup default values on game startup
    {
        CurrentMoney = 0;
        Score = 0;
        PlayerPrefs.SetFloat("PlayerScore", Score);
    }

    public void RewardPlayer(int money) //reward the player with given amount of money and update UI accordingly, if he won load another scene. 
    {
        CurrentMoney += money;
        Score = CurrentMoney * scoreMultiplier;
        scoreText.text = "SCORE: " + Score;
        PlayerPrefs.SetFloat("PlayerScore", Score);
        moneyText.text = $"+ {money} money";
        StartCoroutine(FadeText(moneyText, 1.5f));
        scoreText.rectTransform.DOPunchScale(new Vector3(1, 1, 1), .5f);

        UnlockDoors?.Invoke();

        if (Score >= pointsToReach)
        {
            CurrentMoney += prize;
            OnWinPoints?.Invoke();
        }
    }

    private void OnDisable() //when game or player quits, update the money value.
    {
        if (GameplayManager.instance != null)
        {
            GameplayManager.instance.playerStats.playerMoney += CurrentMoney;
        }
    }

    private IEnumerator FadeText(TextMeshProUGUI text, float duration)
    {
        text.DOFade(1.0f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        text.DOFade(0.0f, duration);
    }
}