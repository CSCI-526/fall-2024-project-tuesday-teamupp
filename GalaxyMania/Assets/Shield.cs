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
            StartCoroutine(HideAndShow());
            playerController.SetShieldActive(true);
            if (popUp != null)
            {
                popUp.ShowPopUp("Shield Activated!");
            }
        }
    }

    IEnumerator HideAndShow()
    {
        ToggleShield(false);

        // Wait for 15 seconds
        yield return new WaitForSeconds(15);

        ToggleShield(true);
    }

    void ToggleShield(bool isVisible)
    {
        // Handle visibility (Renderer)
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = isVisible;
        }

        // Handle collision (Collider2D)
        Collider2D[] colliders = gameObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = isVisible;
        }
    }



}
