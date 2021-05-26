using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUiManager : MonoBehaviour
{

    [SerializeField] private GameObject settingsUI;
    [SerializeField] private GameObject startUI;
    // [SerializeField] private Image fadeImage;    
    private Button[] menuButtons;

    private void Awake()
    {
        menuButtons = GetComponentsInChildren<Button>();
        foreach (var VARIABLE in menuButtons)
        {
            VARIABLE.onClick.AddListener(PlaySound);
        }
    }

    void PlaySound()
    {
        SoundManager.instance?.Play(Sound.Names.UiSound);
    }
    
    public void StartGame()
    {
        GameplayManager.instance.LoadLevel(1);
    }
    public void StartDeath()
    {
        GameplayManager.instance.LoadLevel(2);
    }
    public void StartMenu()
    {
        GameplayManager.instance.LoadLevel(0);
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
