
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

    private enum PlayerStat { Health, Speed, WeaponDmg };
    [SerializeField]
    private PlayerStat statToIncrease = PlayerStat.Health;
    [SerializeField]
    private float increaseAmount;

    [SerializeField]
    private Text priceText;
    private GameplayManager _manager;

    private ShopManager _shopManager;

    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(BuyItem);
        _shopManager = FindObjectOfType<ShopManager>();
    }

    private void Start()
    {
        _manager = FindObjectOfType<GameplayManager>();
        priceText.text = $"${cost}";
    }

    private void BuyItem()
    {
        if(_manager.playerStats.playerMoney >= cost)
        {
            CheckForSkill();
            _manager.playerStats.playerMoney -= cost;
            PlayerPrefs.SetInt("PlayerMoney", _manager.playerStats.playerMoney);
            _shopManager.UpdateCoinsText();
            SoundManager.instance?.Play(Sound.Names.CashRegister);
            gameObject.SetActive(false);
        }
    }

    private void CheckForSkill()
    {
        if (isActiveSkill)
        {
            _manager.playerStats.upperSkill = skill;
        }
        else if (!isActiveSkill)
        {
            switch (statToIncrease)
            {
                case PlayerStat.Health:
                    _manager.playerStats.playerHealth += (int)increaseAmount;
                    break;
                case PlayerStat.Speed:
                    _manager.playerStats.playerSpeed += increaseAmount;
                    break;
                case PlayerStat.WeaponDmg:
                    _manager.playerStats.playerWeaponDamage += (int)increaseAmount;
                    break;
            }
        }
    }
}