
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<SkillSlotsController>().upperSlotSkill = playerStats.upperSkill;
            GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>().maxHp = playerStats.playerHealth;
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerWeaponComponent>().weaponDamage =
                playerStats.playerWeaponDamage;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerSpeed = playerStats.playerSpeed;
            
            playerStats.playerMoney = PlayerPrefs.GetInt(PlayerMoneyString);

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
            SoundManager.instance.Play(Sound.Names.MainMenuTheme);
        }
        // if (sceneName == "MainMenu")
        // {
        //     SoundManager.instance.Play(Sound.Names.MainMenuTheme);
        // }
        
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
        SoundManager.instance.StopSound(Sound.Names.MainMenuTheme);
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
