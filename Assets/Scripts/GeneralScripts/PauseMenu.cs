using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitToMainMenu()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex - 1);
        Time.timeScale = 1;
    }
}