using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class PlayerDiamondCollision : MonoBehaviour
{
    public static bool hasDiamond = false; // Tracks if the player has collected the diamond
    public string freezePicked;
    public static bool counter = false;
    Send2Google send2Google;

    void Start()
    {
        GameObject senderObject = GameObject.Find("Person");
        // Get the Send2Google component from the GameObject
        if (senderObject != null)
        {
            send2Google = senderObject.GetComponent<Send2Google>();
        }
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void Update()
    {
        // Check if the player presses the "E" key and has collected the diamond
        //string currentLevelName = GetCurrentLevelName();
        if (hasDiamond && Input.GetKeyDown(KeyCode.E))
        {
            hasDiamond = false;
            Debug.Log("Diamond powerup used!");

            // Call the coroutine to pause the rotation for 10 seconds
            StartCoroutine(FindObjectOfType<LevelRotation>().PauseRotationForSeconds(10f));

            // Notify HUDController to update UI for Freeze power-up
            HUDController hudController = FindObjectOfType<HUDController>(); // NEW BLOCK
            if (hudController != null)
            {
                hudController.UseFreezePowerUp();  // Trigger Freeze UI update and angle color change
            }
            else
            {
                Debug.LogError("HUDController not found!");
            }
        }
        //if (!hasDiamond)
        //{
        //    freezePicked = "No";
        //    send2Google.SendFreeze(freezePicked);
        //    Debug.Log("analytics executed3");
         
        //}
        //else
        //{
        //    freezePicked = "Yes";
        //    send2Google.SendFreeze(freezePicked);
        //}
    }

    //public string GetCurrentLevelName()
    //{
    //    return SceneManager.GetActiveScene().name;
    //}
    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    string currentLevelName = GetCurrentLevelName();

    //}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object we collided with is the diamond
        if (collision.collider.CompareTag("Diamond"))
        {
            Debug.Log("Diamond collected!");
            Destroy(collision.gameObject); // Remove the diamond from the scene
            hasDiamond = true; // Player has now collected the diamond
            counter = true;
            send2Google.SendFreeze("Yes");

            PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();
            if (popUp != null)
            {
                popUp.ShowPopUp("Press E to freeze \"Level Rotation!\"");
            }

            // Notify HUDController to show Freeze UI
            HUDController hudController = FindObjectOfType<HUDController>(); // NEW LINE
            if (hudController != null)
            {
                hudController.CollectFreeze();  // NEW LINE - Trigger Freeze UI collection update
            }
        }

    }

    public static void ResetDiamondState()
    {
        PlayerDiamondCollision.hasDiamond = false; // Reset the diamond collection state
    }
}
