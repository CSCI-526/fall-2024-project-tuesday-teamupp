using UnityEngine;
using UnityEngine.SceneManagement;

public class DistTracker : MonoBehaviour
{
    public GameObject flag;
    public GameObject portal;
    public GameObject ball;
    Send2Google send2Google;
    public string selectedAnswer1 = null;
    public string selectedAnswer2 = null;
    public float portalDistanceThreshold = 7f; 
    public float maxPortalDistance = 20f; 
    public float flagDistanceThreshold = 6f; 
    private float timer;
    private bool conditionMet;
    private bool hasBeenNearPortal = false;
    private bool reachflag = false;
    private bool check2flagand2portal = false;

    void Start()
    {
        timer = 0f;
        conditionMet = false;

        GameObject senderObject = GameObject.Find("Person");
        if (senderObject != null)
        {
            send2Google = senderObject.GetComponent<Send2Google>();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        TrackDistance();

        if (!conditionMet)
        {

            timer += Time.deltaTime; 
        }
    }

    public string GetCurrentLevelName()
    {
        return SceneManager.GetActiveScene().name;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        timer = 0f;  
        conditionMet = false;  
        hasBeenNearPortal = false;  
        string currentLevelName = GetCurrentLevelName();

    }

    void TrackDistance()
    {
        float distanceToFlag = Vector3.Distance(ball.transform.position, flag.transform.position);
        float distanceToPortal = Vector3.Distance(ball.transform.position, portal.transform.position);

        string message = null;
        string currentLevelName = GetCurrentLevelName();

        if (currentLevelName == "Level 1")
        {
            if (!conditionMet && (distanceToPortal > maxPortalDistance) && (distanceToFlag < flagDistanceThreshold) && (!hasBeenNearPortal))
            {
                conditionMet = true;
                message = $"Goes directly to the flag in {currentLevelName} in {timer} seconds";
                Debug.Log(message);
                reachflag = true;
                selectedAnswer1 = "Goes directly to the flag";
                selectedAnswer2 = null;

            }

            if (!conditionMet && distanceToPortal < portalDistanceThreshold && distanceToFlag > distanceToPortal)
            {
                conditionMet = true;
                hasBeenNearPortal = true;
                message = $"Goes to the portal in {currentLevelName} in {timer} seconds";
                Debug.Log(message);
                selectedAnswer1 = "Goes directly to the portal";
                selectedAnswer2 = null;

            }

            if ((!check2flagand2portal) && reachflag && distanceToPortal < portalDistanceThreshold && distanceToFlag > distanceToPortal)
            {
                conditionMet = true;
                check2flagand2portal = true;
                hasBeenNearPortal = true;
                message = $"Goes to the flag and goes to the portal afterwards in {currentLevelName} in {timer} seconds";
                Debug.Log(message);
                selectedAnswer1 = "Goes to the flag and goes to the portal afterwards";
                selectedAnswer2 = null;

            }
        }

        if (!conditionMet && (distanceToPortal > maxPortalDistance) && (distanceToFlag < flagDistanceThreshold) && (!hasBeenNearPortal))
        {
            conditionMet = true;
            message = $"Goes directly to the flag in {currentLevelName} in {timer} seconds";
            Debug.Log(message);
            reachflag = true;
            selectedAnswer2 = "Goes directly to the flag";
            selectedAnswer1 = null;

        }

        if (!conditionMet && distanceToPortal < portalDistanceThreshold && distanceToFlag > distanceToPortal)
        {
            conditionMet = true;
            hasBeenNearPortal = true;
            message = $"Goes to the portal in {currentLevelName} in {timer} seconds";
            Debug.Log(message);
            selectedAnswer2 = "Goes directly to the portal";
            selectedAnswer1 = null;

        }

        if ((!check2flagand2portal) && reachflag && distanceToPortal < portalDistanceThreshold && distanceToFlag > distanceToPortal)
        {
            conditionMet = true;
            check2flagand2portal = true;
            hasBeenNearPortal = true;
            message = $"Goes to the flag and goes to the portal afterwards in {currentLevelName} in {timer} seconds";
            Debug.Log(message);
            selectedAnswer2 = "Goes to the flag and goes to the portal afterwards";
            selectedAnswer1 = null;

        }

    }
    public void SendLevel1()
    {
        if (send2Google != null && (!string.IsNullOrEmpty(selectedAnswer1) || !string.IsNullOrEmpty(selectedAnswer2)))
        {
            Debug.Log("Sending Level 1 or 2 answers to Google Forms");
            send2Google.SendTest(selectedAnswer1, selectedAnswer2);
        }
    }
}