using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private PlayerController playerController;
    Send2Google send2Google;
    //public static bool scounter = false;

    private void Start()
    {
        // Get the player controller
        playerController = FindObjectOfType<PlayerController>();
        GameObject senderObject = GameObject.Find("Person");
        // Get the Send2Google component from the GameObject
        if (senderObject != null)
        {
            send2Google = senderObject.GetComponent<Send2Google>();
        }
    }

    // Detect player collision with the power-up
    private void OnTriggerEnter2D(Collider2D other)
    {
        PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();

        if (other.CompareTag("Player") && !playerController.IsShieldActive())  // Activate only if the shield isn't already active
        {
            playerController.SetShieldActive(true);
            //shieldPicked = true; 
            //send2Google.SendShield(shieldPicked);
            Destroy(gameObject);  // Destroy the shield power-up after collection
        }
        if (popUp != null)
        {
            popUp.ShowPopUp("Shield Activated!");
        }
    }
    
}
