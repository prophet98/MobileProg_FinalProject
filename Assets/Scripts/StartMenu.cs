using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject startUI;
    // [SerializeField] private Image fadeImage;

    private void Start()
    {
        // fadeImage.CrossFadeAlpha(0,0,true);
    }

    public void StartGame()
    {
        GameplayManager.instance.LoadLevel(1);
    }

    public void QuitGame()
    {
#if  UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ShowSettings()
    {
        settingsUI.SetActive(true);
        startUI.SetActive(false);
    }
    
    public void QuitSettings()
    {
        settingsUI.SetActive(false);
        startUI.SetActive(true);
    }

}
