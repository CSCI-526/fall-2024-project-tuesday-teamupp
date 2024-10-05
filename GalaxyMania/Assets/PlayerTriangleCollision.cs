using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriangleCollision : MonoBehaviour
{
    public static bool collectTriangle = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object we collided with is the triangle
        if (collision.collider.CompareTag("Triangle"))
        {
            Debug.Log("Collision detected with the triangle!");
            Destroy(collision.gameObject);
            collectTriangle = true;
        }
    }
}
