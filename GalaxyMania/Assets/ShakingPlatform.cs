using System.Collections;
using UnityEngine;

public class ShakingPlatform : MonoBehaviour
{
    private Vector3 originalPosition;
    public float vibrateDuration = 2f; // Duration of the vibration (2 seconds)
    public float vibrateMagnitude = 0.24f; // Intensity of the vibration
    public float disappearDuration = 2f; // Duration the platform remains invisible (5 seconds)
    private Renderer platformRenderer;
    private Collider2D platformCollider;

    private void Start()
    {
        originalPosition = transform.position; // Store the platform's original position
        Debug.Log(originalPosition.ToString());
        platformRenderer = GetComponent<Renderer>(); // Get the Renderer component
        platformCollider = GetComponent<Collider2D>(); // Get the Collider2D component
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player landed on the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(VibrateAndDisappear());
        }
    }

    private IEnumerator VibrateAndDisappear()
    {
        // Start the vibration effect
        yield return StartCoroutine(Vibrate(vibrateDuration));

        // Make the platform invisible and disable its collider
        //gameObject.SetActive(false);
        if (platformRenderer != null) platformRenderer.enabled = false;
        if (platformCollider != null) platformCollider.enabled = false;

        // Wait for disappearDuration seconds
        yield return new WaitForSeconds(disappearDuration);

        // Make the platform visible again
        //gameObject.SetActive(true);
        if (platformRenderer != null) platformRenderer.enabled = true;
        if (platformCollider != null) platformCollider.enabled = true;
        //transform.position = originalPosition; // Reset to original position
    }

    private IEnumerator Vibrate(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Create a small random offset around the original position
            Vector3 randomOffset = new Vector3(
                Random.Range(-vibrateMagnitude, vibrateMagnitude),
                Random.Range(-vibrateMagnitude, vibrateMagnitude),
                0);

            // Apply the offset to the platform's local position
            transform.localPosition = originalPosition + randomOffset;

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Reset the position to original after vibration ends
        //transform.localPosition = originalPosition;

    }

}
