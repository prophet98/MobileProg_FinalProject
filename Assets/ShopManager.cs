
using System;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject passivePanel, activePanel;
    [SerializeField] private Button switchButton;
    [SerializeField] private TextMeshProUGUI moneyText;
    private void OnEnable()
    {
        switchButton.onClick.AddListener(HandlePanelView);
        moneyText.text = FindObjectOfType<GameplayManager>().playerStats.playerMoney.ToString();
    }

    private void HandlePanelView()
    {
        if (passivePanel.activeSelf)
        {
            passivePanel.SetActive(false);
            activePanel.SetActive(true);
        }
        else if (activePanel.activeSelf)
        {
            passivePanel.SetActive(true);
            activePanel.SetActive(false);
        }
    }

    private void OnDisable()
    {
        switchButton.onClick.RemoveAllListeners();
    }
}
