using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public Transform player;      // Reference to the player object
    public Transform goal;        // Reference to the goal object
    public float distanceFromPlayer = 3f;  // Distance from the player
    public float rotationSpeed = 50f; // Speed of rotation around the player

    void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.green; // Change color to red
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate the direction to the goal from the player
        Vector3 directionToGoal = goal.position - player.position;

        //Normalize the direction vector to get the angle
        directionToGoal.z = 0; // Ensure we are only working in 2D
        directionToGoal.Normalize(); // Normalize the direction vector

        //Calculate the angle in degrees to rotate towards the goal
        float goalAngle = Mathf.Atan2(directionToGoal.y, directionToGoal.x) * Mathf.Rad2Deg;

        // Set the position of the triangle relative to the player
        Vector3 trianglePosition = player.position + (directionToGoal * distanceFromPlayer);
        transform.position = trianglePosition;

        // Adjust the angle to ensure the top point of the triangle points toward the goal
        // Assuming the triangle's top point is aligned with 0 degrees in the local coordinate system
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, goalAngle - 90f));
    }
}


