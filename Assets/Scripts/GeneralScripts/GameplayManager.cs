using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;
    private Image _loadingScreen;
    public int playerMoney;

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

    private void Start()
    {
        playerMoney = PlayerPrefs.GetInt(PlayerMoneyString);
    }

    public void LoadLevel(int sceneIndex)
    {
        _loadingScreen.gameObject.SetActive(true);
        StartCoroutine(LoadAsyncRoutine(sceneIndex));
    }

    private IEnumerator LoadAsyncRoutine(int sceneIndex)
    {
        var loadOp =  SceneManager.LoadSceneAsync(sceneIndex);
        while (!loadOp.isDone)
        {
            yield return null;
        }
        _loadingScreen.CrossFadeAlpha(0f, .5f, false);
        yield return new WaitForSeconds(.5f);
        _loadingScreen.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(PlayerMoneyString, playerMoney);
    }
}
