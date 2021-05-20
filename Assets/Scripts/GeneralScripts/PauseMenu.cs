using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private static bool _isPaused = false;

    public GameObject pauseUI;    
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }
    
    public void QuitToMainMenu()
    {
        var scene = SceneManager.GetActiveScene();
        // SoundManager.instance?.DisableAmbientSounds();
        SceneManager.LoadScene(scene.buildIndex - 1);
        Time.timeScale = 1;
        _isPaused = false;
    }
}

