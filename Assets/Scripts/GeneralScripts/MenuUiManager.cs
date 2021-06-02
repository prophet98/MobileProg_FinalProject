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
    private Button[] _menuButtons;

    private void Awake()
    {
        _menuButtons = GetComponentsInChildren<Button>(true);
        foreach (var menuButton in _menuButtons)
        {
            menuButton.onClick.AddListener(PlaySound);
        }
    }

    private void PlaySound()
    {
        SoundManager.instance?.Play(Sound.Names.UiSound);
    }
    
    public void StartGame()
    {
        GameplayManager.instance?.LoadLevel("Hub");
    }
    public void StartMenu()
    {
        GameplayManager.instance?.LoadLevel("MainMenu");
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

    public void UnpauseGame()
    {
        Time.timeScale = 1;
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

}
