using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // This is required to load scenes

public class NextSceneLoader : MonoBehaviour
{
    DistTracker distTracker;
    Send2Google send2Google;
    void Start()
    {
        GameObject senderObject = GameObject.Find("DistTracker");
        GameObject sendGoogleObject = GameObject.Find("Person");
        // Get the Send2Google component from the GameObject
        if (senderObject != null)
        {
            distTracker = senderObject.GetComponent<DistTracker>();
        }
        if (sendGoogleObject != null)
        {
            send2Google = sendGoogleObject.GetComponent<Send2Google>();
        }
    }
    void OnTriggerEnter2D(Collider2D other)  // Use Collider2D for 2D trigger detection
    {
        if (other.CompareTag("Player"))  // Ensure the player has the tag "Player"
        {
            Debug.Log("Player entered trigger zone.");

            LoadNextScene();  // Call the function to load the next scene
        }
    }

    private IEnumerator FreezeAndContinue(string currentSceneName)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.75f);
        Time.timeScale = 1;
        if (currentSceneName == "Tutorial Move")  // Check if the current scene is Level 1
        {
            distTracker.sendlevel1();
            // Load the next level
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            SceneManager.LoadScene("Tutorial Jump");
        }
        else if (currentSceneName == "Tutorial Jump")  // Check if the current scene is Level 1
        {
            distTracker.sendlevel1();
            // Load the next level
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            SceneManager.LoadScene("Tutorial Portal");
        }
        else if (currentSceneName == "Tutorial Portal")  // Check if the current scene is Level 1
        {
            distTracker.sendlevel1();
            // Load the next level
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            SceneManager.LoadScene("Level 1");
        }
        else if (currentSceneName == "Level 1")  // Check if the current scene is Level 1
        {
            distTracker.sendlevel1();
            send2Google.SendCompleteLevelData(send2Google.timer, send2Google.numOfJump, "Level 1", CountPlatform.levelPlatforms["Level 1"].Count);
            // Load the next level
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            SceneManager.LoadScene("Level 2");
        }
        else if (currentSceneName == "Level 2")  // Check if the current scene is Level 1
        {
            distTracker.sendlevel1();
            send2Google.SendCompleteLevelData(send2Google.timer, send2Google.numOfJump, "Level 2", CountPlatform.levelPlatforms["Level 2"].Count);
            // Load the next level
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            if (PlayerDiamondCollision.counter == false)
            {
                send2Google.SendFreeze("No");
            }
            SceneManager.LoadScene("Level 3");
        }
    }

    void LoadNextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;  // Get the current scene name
        PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();
        if (popUp != null)
        {
            popUp.ShowPopUp(currentSceneName + " Cleared!");
            StartCoroutine(FreezeAndContinue(currentSceneName));
        }
        
    }
}
