
using System.Collections;
using DamageScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    private Image _loadingScreen;
    private VolumeController[] _volumeControllers;
    private const string PlayerMoneyString = "PlayerMoney";

    public PlayerStats playerStats;
    private void Awake()
    {
        #region Singleton

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        _loadingScreen = GetComponentInChildren<Image>(true);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        HealthComponent.OnPlayerDeath += LoadDeathScene;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _volumeControllers = FindObjectsOfType<VolumeController>(true);
        if (scene.name == "MainMenu")
        {
            StartCoroutine(AdjustMixerAndPlayBG(scene.name));
        }
        else if (scene.name == "Dungeon_PAOLO")
        {
            StartCoroutine(AdjustMixerAndPlayBG(scene.name));
            LoadPlayerStats();
        }
        else if (scene.name == "Hub")
        {
            StartCoroutine(AdjustMixerAndPlayBG(scene.name));
            
        }
        
    }

    void LoadPlayerStats()
    {
        var Player = GameObject.FindGameObjectWithTag("Player");
        if (Player!=null)
        {
            Player.GetComponent<SkillSlotsController>().upperSlotSkill = playerStats.upperSkill;
            Player.GetComponent<HealthComponent>().maxHp = playerStats.playerHealth;
            Player.GetComponentInChildren<PlayerWeaponComponent>().weaponDamage =
                playerStats.playerWeaponDamage;
            Player.GetComponent<PlayerController>().playerSpeed = playerStats.playerSpeed;
            playerStats.playerMoney = PlayerPrefs.GetInt(PlayerMoneyString);
        }
        else
        {
            Debug.LogError("no player was found in the scene!");
        }
        
        
    }
    IEnumerator AdjustMixerAndPlayBG(string sceneName)
    {
        yield return new WaitForSeconds(.1f);
        foreach (var controller in _volumeControllers)
        {
            controller.AdjustVolumeMixer();
        }

        if (sceneName == "MainMenu")
        {
            SoundManager.instance?.Play(Sound.Names.MainMenuTheme);
        }

        else if (sceneName == "Hub")
        {
            SoundManager.instance?.Play(Sound.Names.HubTheme);
        }
        else if (sceneName == "Dungeon_PAOLO")
        {
            SoundManager.instance?.Play(Sound.Names.BattleTheme01);
        }
        
    }

    public void LoadLevel(string sceneName)
    {
        _loadingScreen.gameObject.SetActive(true);
        StartCoroutine(LoadAsyncRoutine(sceneName));
    }

    private IEnumerator LoadAsyncRoutine(string sceneName)
    {
        var loadOp =  SceneManager.LoadSceneAsync(sceneName);
        while (!loadOp.isDone)
        {
            yield return null;
        }
        _loadingScreen.CrossFadeAlpha(0f, .5f, false);
        yield return new WaitForSeconds(.5f);
        _loadingScreen.gameObject.SetActive(false);
    }

    private void LoadDeathScene()
    {
        StartCoroutine(LoadAsyncRoutine("DeathScene"));
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt(PlayerMoneyString, playerStats.playerMoney);
        HealthComponent.OnPlayerDeath -= LoadDeathScene;
    }
}
