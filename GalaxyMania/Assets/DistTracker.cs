using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistTracker : MonoBehaviour
{
    public GameObject flag;
    public GameObject portal;
    public GameObject ball;
    Send2Google send2Google;


    public float portalDistanceThreshold = 7f; // Adjustable threshold for portal proximity
    public float maxPortalDistance = 20f; // Distance threshold to check if the ball is far from the portal
    public float flagDistanceThreshold = 6f; // Distance threshold to check if the ball is close to the flag

    private float timer;
    private bool conditionMet;
    private bool hasBeenNearPortal = false;
    private bool portalTracker = false;
    private bool flagTracker = false;
    //private bool newScene = false;
    //private PlayerController playerController;

    void Start()
    {
        timer = 0f;
        conditionMet = false;
        //newScene = true;

        GameObject senderObject = GameObject.Find("Person");
        // Get the Send2Google component from the GameObject
        if (senderObject != null)
        {
            send2Google = senderObject.GetComponent<Send2Google>();
        }

        //GameObject player = GameObject.Find("Person");
        //if (player != null)
        //{
        //    playerController = player.GetComponent<PlayerController>();
        //}

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        TrackDistance();

        if (!conditionMet)
        {

            timer += Time.deltaTime; // Increment the timer
        }
    }
    public string GetCurrentLevelName()
    {
        return SceneManager.GetActiveScene().name;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        timer = 0f;  // Reset the timer whenever a new scene is loaded
        conditionMet = false;  // Reset condition so tracking continues in the new level
        hasBeenNearPortal = false;  // Reset portal proximity flag
        string currentLevelName = GetCurrentLevelName();

    }
    void TrackDistance()
    {
        float distanceToFlag = Vector3.Distance(ball.transform.position, flag.transform.position);
        float distanceToPortal = Vector3.Distance(ball.transform.position, portal.transform.position);

        string message = null;
        string currentLevelName = GetCurrentLevelName();

        string selectedAnswer1 = null;
        string selectedAnswer2 = null;

        //bool isPlayerDead = playerController != null && playerController.IsGameOver();
        //newScene = false;
        if (currentLevelName == "Level 1")
        {
            if (!conditionMet && (distanceToPortal > maxPortalDistance) && (distanceToFlag < flagDistanceThreshold) && (!hasBeenNearPortal))
            {
                conditionMet = true;
                message = $"Goes directly to the flag in {currentLevelName} in {timer} seconds";
                Debug.Log(message);

                flagTracker = true;
                selectedAnswer1 = "Goes directly to the flag";
                selectedAnswer2 = null;

            }

            if (!conditionMet && distanceToPortal < portalDistanceThreshold && distanceToFlag > distanceToPortal)
            {
                conditionMet = true;
                hasBeenNearPortal = true;
                message = $"Goes to the portal in {currentLevelName} in {timer} seconds";
                Debug.Log(message);
                portalTracker = true;
                selectedAnswer1 = "Goes directly to the portal";
                selectedAnswer2 = null;

            }
            if (send2Google != null && !string.IsNullOrEmpty(selectedAnswer1))
            {
                Debug.Log("Sending Level 1 answers to Google Forms");
                send2Google.Send(selectedAnswer1, selectedAnswer2);
            }
        }

        if (currentLevelName == "Level 2")
        {
            if (portalTracker)
            {
                selectedAnswer1 = "Goes directly to the portal";
            }
            if (flagTracker)
            {
                selectedAnswer1 = "Goes directly to the flag";
            }

            if (!conditionMet && (distanceToPortal > maxPortalDistance) && (distanceToFlag < flagDistanceThreshold) && (!hasBeenNearPortal))
            {
                conditionMet = true;
                message = $"Goes directly to the flag in {currentLevelName} in {timer} seconds";
                Debug.Log(message);
                
                selectedAnswer2 = "Goes directly to the flag";
            }

            if (!conditionMet && distanceToPortal < portalDistanceThreshold && distanceToFlag > distanceToPortal)
            {
                conditionMet = true;
                hasBeenNearPortal = true;
                message = $"Goes to the portal in {currentLevelName} in {timer} seconds";
                Debug.Log(message);
                
                selectedAnswer2 = "Goes directly to the portal";
            }
            
            if (send2Google != null && !string.IsNullOrEmpty(selectedAnswer2))
            {
                Debug.Log("Sending Level 2 answers to Google Forms");
                send2Google.Send(selectedAnswer1, selectedAnswer2);
            }
        }
    }
}