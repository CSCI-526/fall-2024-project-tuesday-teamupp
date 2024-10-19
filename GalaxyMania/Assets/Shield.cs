using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        // Get the player controller
        playerController = FindObjectOfType<PlayerController>();
    }

    // Detect player collision with the power-up
    private void OnTriggerEnter2D(Collider2D other)
    {
        PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();

        if (other.CompareTag("Player") && !playerController.IsShieldActive())  // Activate only if the shield isn't already active
        {
            playerController.SetShieldActive(true);
            Destroy(gameObject);  // Destroy the shield power-up after collection
        }
        if (popUp != null)
        {
            popUp.ShowPopUp("Shield Activated!");
        }
    }
}
