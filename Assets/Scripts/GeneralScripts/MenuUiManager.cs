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
    private Button[] _menuButtons;

    private void Awake()
    {
        _menuButtons = GetComponentsInChildren<Button>();
        foreach (var menuButton in _menuButtons)
        {
            menuButton.onClick.AddListener(PlaySound);
        }
    }

    void PlaySound()
    {
        SoundManager.instance?.Play(Sound.Names.UiSound);
    }
    
    public void StartGame()
    {
        GameplayManager.instance.LoadLevel("Dungeon_PAOLO");
    }
    public void StartDeath()
    {
        GameplayManager.instance.LoadLevel("DeathScene");
    }
    public void StartMenu()
    {
        GameplayManager.instance.LoadLevel("MainMenu");
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