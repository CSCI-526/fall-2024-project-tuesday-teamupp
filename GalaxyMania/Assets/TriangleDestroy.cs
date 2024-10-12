using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCollect : MonoBehaviour
{
    // This method is called when another collider enters the trigger collider attached to the triangle
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerTriangleCollision.collectTriangle = false;
        // Check if the other collider has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // Optionally, you can add code to update the player's score or inventory here

            Debug.Log("Triangle collected!");
            // Set the static variable to indicate a triangle has been collected
            PlayerTriangleCollision.collectTriangle = true;

            // Destroy this triangle GameObject
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (PlayerTriangleCollision.collectTriangle)
        {
            Destroy(gameObject);
            PlayerTriangleCollision.collectTriangle = false;
        }
    }
}
