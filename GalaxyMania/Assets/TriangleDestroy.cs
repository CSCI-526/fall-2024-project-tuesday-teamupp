using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriangleCollect : MonoBehaviour
{
    //This method is called when another collider enters the trigger collider attached to the triangle
    private void OnTriggerEnter2D(Collider2D other)
    {
        //PlayerTriangleCollision.collectTriangle = false;
        // Check if the other collider has the tag "Player"
        PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();

        if (other.CompareTag("Player"))
        {
            //// Optionally, you can add code to update the player's score or inventory here

            //Debug.Log("Triangle collected!");
            //// Set the static variable to indicate a triangle has been collected
            //PlayerTriangleCollision.collectTriangle = true;

            string currentSceneName = SceneManager.GetActiveScene().name;

            if (popUp != null)
            {
                popUp.ShowPopUp(currentSceneName + " checkpoint reached!");
            }

            // Update the dictionary in PlayerController to mark the triangle as collected
            if (PlayerController.triangleCollectionState.ContainsKey(currentSceneName))
            {
                PlayerController.triangleCollectionState[currentSceneName] = true; // Mark as collected
            }

            Debug.Log("Triangle collected in " + currentSceneName + "!");

            // Destroy this triangle GameObject
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //if (PlayerTriangleCollision.collectTriangle)
        //{
        //    Destroy(gameObject);
        //    PlayerTriangleCollision.collectTriangle = false;
        //}

        string currentSceneName = SceneManager.GetActiveScene().name;

        // If the triangle has already been collected in this scene, destroy it
        if (PlayerController.triangleCollectionState.TryGetValue(currentSceneName, out bool hasCollected) && hasCollected)
        {
            Destroy(gameObject); // Remove the triangle from the scene
            Debug.Log("Triangle was already collected in " + currentSceneName + ", so it's destroyed.");
        }
    }
}
