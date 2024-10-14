using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTriangleCollision : MonoBehaviour
{
    //public static bool collectTriangle = false;
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // Check if the object we collided with is the triangle
    //    if (collision.collider.CompareTag("Triangle"))
    //    {
    //        //Debug.Log("Collision detected with the triangle!");
    //        //Destroy(collision.gameObject);
    //        //collectTriangle = true;

    //        string currentSceneName = SceneManager.GetActiveScene().name;

    //        // Update the dictionary in PlayerController to mark the triangle as collected
    //        if (PlayerController.triangleCollectionState.ContainsKey(currentSceneName))
    //        {
    //            PlayerController.triangleCollectionState[currentSceneName] = true; // Mark as collected
    //        }

    //        Debug.Log("Triangle collected in " + currentSceneName + "!");

    //        // Destroy this triangle GameObject
    //        Destroy(collision.collider.gameObject);
    //    }
    //}
}
