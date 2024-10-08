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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object we collided with is the diamond
        if (collision.collider.CompareTag("Diamond"))
        {
            Debug.Log("Diamond collected!");
            Destroy(collision.gameObject); // Remove the diamond from the scene
            hasDiamond = true; // Player has now collected the diamond
        }
    }
    public static void ResetDiamondState()
    {
        PlayerDiamondCollision.hasDiamond = false; // Reset the diamond collection state
    }

}
