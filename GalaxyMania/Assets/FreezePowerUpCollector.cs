using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDiamondCollision : MonoBehaviour
{
    public static bool hasDiamond = false; // Tracks if the player has collected the diamond

    void Update()
    {
        // Check if the player presses the "E" key and has collected the diamond
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();

        // Check if the object we collided with is the diamond
        if (collision.collider.CompareTag("Diamond"))
        {
            Debug.Log("Diamond collected!");
            Destroy(collision.gameObject); // Remove the diamond from the scene
            hasDiamond = true; // Player has now collected the diamond

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
