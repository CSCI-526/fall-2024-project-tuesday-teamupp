using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    void Start()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void TogglePauseMenu()
    {
        bool isPaused = pauseMenuPanel.activeSelf;
        pauseMenuPanel.SetActive(!isPaused);

        Time.timeScale = isPaused ? 1 : 0;
    }

    public void ContinueGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1; 
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level Screen");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void TutorialScene()
    {
        try
        {
            Debug.Log("Attempting to load Tutorial Scene...");
            Time.timeScale = 1;
            SceneManager.LoadScene("Tutorial Screen");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to load Tutorial Scene: " + e.Message);
        }
        
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
