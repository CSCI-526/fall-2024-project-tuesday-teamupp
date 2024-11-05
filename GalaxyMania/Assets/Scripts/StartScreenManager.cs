using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene (replace "GameScene" with your actual scene name)
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
        // Note: This will not work in the editor; only in a build
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
