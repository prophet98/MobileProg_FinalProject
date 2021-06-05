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
    private const string PlayerTag = "Player";
    private const float DefaultPlayerSpeed = 10;
    private const int DefaultPlayerHealth = 100;
    private const int DefaultPlayerWeaponDamage = 25;

    private const string MainMenuName = "MainMenu";
    private const string DungeonName = "Dungeon_Final";
    private const string HubName = "Hub";


    public PlayerStats playerStats;

    private void Awake() //singleton class that coordinates scene loading and player stats overriding. 
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

    private void OnEnable() //subscribe this object to different events
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        HealthComponent.OnPlayerDeath += LoadDeathScene;
        BattleRewardSystem.OnWinPoints += LoadWinScene;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _volumeControllers = FindObjectsOfType<VolumeController>(true);
        switch (scene.name)
        {
            case MainMenuName:
                StartCoroutine(AdjustMixerAndPlayBg(scene.name));
                playerStats.playerMoney = PlayerPrefs.GetInt(PlayerMoneyString);
                break;
            case HubName:
                StartCoroutine(AdjustMixerAndPlayBg(scene.name));
                playerStats.playerMoney = PlayerPrefs.GetInt(PlayerMoneyString);
                ResetPlayerStats();
                break;
            case DungeonName:
                StartCoroutine(AdjustMixerAndPlayBg(scene.name));
                LoadPlayerStats();
                break;
        }
    }

    private void LoadPlayerStats() //if player is in scene, update his instance variables with saved one. 
    {
        var player = GameObject.FindGameObjectWithTag(PlayerTag);
        if (player != null)
        {
            player.GetComponent<SkillSlotsController>().upperSlotSkill = playerStats.upperSkill;
            player.GetComponent<HealthComponent>().maxHp = playerStats.playerHealth;
            player.GetComponentInChildren<PlayerWeaponComponent>().weaponDamage =
                playerStats.playerWeaponDamage;
            player.GetComponent<PlayerController>().PlayerSpeed = playerStats.playerSpeed;
            playerStats.playerMoney = PlayerPrefs.GetInt(PlayerMoneyString);
        }
    }

    private void ResetPlayerStats() //reset player stats to default. 
    {
        playerStats.playerSpeed = DefaultPlayerSpeed;
        playerStats.playerHealth = DefaultPlayerHealth;
        playerStats.playerWeaponDamage = DefaultPlayerWeaponDamage;
        playerStats.upperSkill = null;

        LoadPlayerStats();
    }

    private IEnumerator
        AdjustMixerAndPlayBg(string sceneName) //adjust sound with a fade effect (applies only to BG music)
    {
        yield return new WaitForSeconds(.1f);
        foreach (var controller in _volumeControllers)
        {
            controller.AdjustVolumeMixer();
        }

        switch (sceneName)
        {
            case MainMenuName:
                SoundManager.instance?.Play(Sound.Names.MainMenuTheme);
                break;
            case HubName:
                SoundManager.instance?.Play(Sound.Names.HubTheme);
                break;
            case DungeonName:
                SoundManager.instance?.Play(Sound.Names.BattleTheme01);
                break;
        }
    }

    public void LoadLevel(string sceneName)
    {
        _loadingScreen.gameObject.SetActive(true);
        StartCoroutine(LoadAsyncRoutine(sceneName));
    }

    private IEnumerator LoadAsyncRoutine(string sceneName) //loads a level with a very basic loading screen.
    {
        Time.timeScale = 1;
        var loadOp = SceneManager.LoadSceneAsync(sceneName);
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

    private void LoadWinScene()
    {
        StartCoroutine(LoadAsyncRoutine("WinScene"));
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(PlayerMoneyString, playerStats.playerMoney);
        HealthComponent.OnPlayerDeath -= LoadDeathScene;
        BattleRewardSystem.OnWinPoints -= LoadWinScene;
    }
}