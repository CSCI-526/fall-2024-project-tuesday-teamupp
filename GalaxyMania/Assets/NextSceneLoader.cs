using UnityEngine;
using UnityEngine.SceneManagement;  // This is required to load scenes

public class NextSceneLoader : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)  // Use Collider2D for 2D trigger detection
    {
        if (other.CompareTag("Player"))  // Ensure the player has the tag "Player"
        {
            Debug.Log("Player entered trigger zone.");

            LoadNextScene();  // Call the function to load the next scene
        }
    }

    void LoadNextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;  // Get the current scene name
        PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();
        if (popUp != null)
        {
            popUp.ShowPopUp(currentSceneName + " Cleared!");
        }

        if (currentSceneName == "Level 1")  // Check if the current scene is Level 1
        {
            // Load the next level
            SceneManager.LoadScene("Level 2");
        }
        else if (currentSceneName == "Level 2")  // Check if the current scene is Level 1
        {
            // Load the next level
            SceneManager.LoadScene("Level 3");
        }
    }
}
