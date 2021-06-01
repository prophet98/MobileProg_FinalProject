
using System;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private GameObject passivePanel, activePanel;
    [SerializeField] private Button switchButton;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI skillText;
    private void OnEnable()
    {
        switchButton.onClick.AddListener(HandlePanelView);
        ActivatePassive();
        moneyText.text = FindObjectOfType<GameplayManager>().playerStats.playerMoney.ToString(); //TODO: add event for realtime update
    }

    private void HandlePanelView()
    {
        if (passivePanel.activeSelf)
        {
            ActivateActive();
        }
        else if (activePanel.activeSelf)
        {
            ActivatePassive();
        }
    }

    private void ActivateActive()
    {
        skillText.text = "active skills";
        passivePanel.SetActive(false);
        activePanel.SetActive(true);
    }

    private void ActivatePassive()
    {
        skillText.text = "passive skills";
        passivePanel.SetActive(true);
        activePanel.SetActive(false);
    }

    private void OnDisable()
    {
        switchButton.onClick.RemoveAllListeners();
    }
}
