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
        UpdateCoinsText();
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
        skillText.text = "switch to\npassive skills";
        passivePanel.SetActive(false);
        activePanel.SetActive(true);
    }

    private void ActivatePassive()
    {
        skillText.text = "switch to\nactive skills";
        passivePanel.SetActive(true);
        activePanel.SetActive(false);
    }

    public void UpdateCoinsText()
    {
        moneyText.text = FindObjectOfType<GameplayManager>().playerStats.playerMoney.ToString();
    }

    private void OnDisable()
    {
        switchButton.onClick.RemoveAllListeners();
    }
}