
using System.Collections;
using DamageScripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    private Image _loadingScreen;
    public int playerMoney;
    private VolumeController[] _volumeControllers;
    private const string PlayerMoneyString = "PlayerMoney";
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
        if (scene.name == "MainMenu")
        {
            _volumeControllers = FindObjectsOfType<VolumeController>(true);
            foreach (var controller in _volumeControllers)
            {
                controller.Awake();
                SoundManager.instance.Play(Sound.Names.MainMenuTheme);
            }
        }
    }

    private void Start()
    {
        playerMoney = PlayerPrefs.GetInt(PlayerMoneyString);
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
        PlayerPrefs.SetInt(PlayerMoneyString, playerMoney);
        HealthComponent.OnPlayerDeath -= LoadDeathScene;
    }
}
