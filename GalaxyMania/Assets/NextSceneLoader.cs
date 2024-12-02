using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class NextSceneLoader : MonoBehaviour
{
    DistTracker distTracker;
    Send2Google send2Google;
    void Start()
    {
        GameObject senderObject = GameObject.Find("DistTracker");
        GameObject sendGoogleObject = GameObject.Find("Person");
        if (senderObject != null)
        {
            distTracker = senderObject.GetComponent<DistTracker>();
        }
        if (sendGoogleObject != null)
        {
            send2Google = sendGoogleObject.GetComponent<Send2Google>();
        }
    }
    void OnTriggerEnter2D(Collider2D other)  
    {
        if (other.CompareTag("Player"))  
        {
            Debug.Log("Player entered trigger zone.");

            LoadNextScene();  
        }
    }

    private IEnumerator FreezeAndContinue(string currentSceneName)
    {
        Debug.Log("freeze and continue");
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.75f);
        Time.timeScale = 1;
        if (currentSceneName == "Tutorial Move")
        {
            distTracker.SendLevel1();
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            SceneManager.LoadScene("Tutorial Jump");
        }
        else if (currentSceneName == "Tutorial Jump")
        {
            distTracker.SendLevel1();
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            SceneManager.LoadScene("Tutorial Bouncy");
        }
        else if (currentSceneName == "Tutorial Bouncy")
        {
            distTracker.SendLevel1();
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            SceneManager.LoadScene("Tutorial Portal");
        }
        else if (currentSceneName == "Tutorial Portal")
        {
            distTracker.SendLevel1();
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            SceneManager.LoadScene("Tutorial Enemy");
        }
        else if (currentSceneName == "Tutorial Enemy")
        {
            distTracker.SendLevel1();
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            SceneManager.LoadScene("Level 1");
        }
        else if (currentSceneName == "Level 1")  
        {
            distTracker.SendLevel1();
            send2Google.SendCompleteLevelData(send2Google.timer, send2Google.numOfJump, "Level 1", CountPlatform.levelPlatforms["Level 1"].Count);
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            SceneManager.LoadScene("Level 2");
        }
        else if (currentSceneName == "Level 2")  
        {
            distTracker.SendLevel1();
            send2Google.SendCompleteLevelData(send2Google.timer, send2Google.numOfJump, "Level 2", CountPlatform.levelPlatforms["Level 2"].Count);
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            if (PlayerDiamondCollision.counter == false)
            {
                send2Google.SendFreeze("No");
            }
            SceneManager.LoadScene("Level 3");
        }
        else if (currentSceneName == "Level 3") 
        {
            send2Google.SendCompleteLevelData(send2Google.timer, send2Google.numOfJump, "Level 3", CountPlatform.levelPlatforms["Level 3"].Count);
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();
            if (PlayerDiamondCollision.counter == false)
            {
                send2Google.SendFreeze("No");
            }
            SceneManager.LoadScene("Level 4");
        }
    }

    void LoadNextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;  
        PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();
        if (popUp != null)
        {
            popUp.ShowPopUp(currentSceneName + " Cleared!");
            StartCoroutine(FreezeAndContinue(currentSceneName));
        }
        
    }
}
