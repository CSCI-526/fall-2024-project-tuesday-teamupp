using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float shieldDuration = 5f; // Duration of the shield
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    private bool isShieldActive = false;  // To check if shield is currently active

    private void Start()
    {
        // Get the player controller and sprite renderer components
        playerController = FindObjectOfType<PlayerController>();
        spriteRenderer = playerController.GetComponent<SpriteRenderer>();
    }

    // Detect player collision with the power-up
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isShieldActive)  // Activate only if the shield isn't already active
        {
            StartCoroutine(ActivateShield(shieldDuration));  // Start the coroutine
            Destroy(gameObject);  // Destroy the shield power-up after collection
        }
    }

    // Coroutine to handle the shield duration
    private IEnumerator ActivateShield(float duration)
    {
        isShieldActive = true;  // Mark shield as active
        spriteRenderer.color = Color.blue;  // Change player color to blue
        playerController.SetShieldActive(true);  // Activate the shield in PlayerController

        // Wait for the shield duration (e.g., 10 seconds)
        Debug.Log($"Shield activated for {duration} seconds.");
        yield return new WaitForSeconds(duration);

        // After the duration, revert player color and deactivate the shield
        spriteRenderer.color = Color.white;  // Change color back to white
        playerController.SetShieldActive(false);  // Deactivate the shield in PlayerController
        isShieldActive = false;  // Mark shield as inactive
        Debug.Log("Shield deactivated.");
    }
}
