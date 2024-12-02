using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    public Transform player;      
    public Transform goal;        
    public float distanceFromPlayer = 3f;  
    public float rotationSpeed = 50f; 

    void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.green; 
    }

    void Update()
    {
        Vector3 directionToGoal = goal.position - player.position;

        directionToGoal.z = 0; 
        directionToGoal.Normalize(); 

        float goalAngle = Mathf.Atan2(directionToGoal.y, directionToGoal.x) * Mathf.Rad2Deg;

        Vector3 trianglePosition = player.position + (directionToGoal * distanceFromPlayer);
        transform.position = trianglePosition;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, goalAngle - 90f));
    }
}


