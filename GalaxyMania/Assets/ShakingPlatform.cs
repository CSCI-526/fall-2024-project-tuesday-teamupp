using System.Collections;
using UnityEngine;

public class ShakingPlatform : MonoBehaviour
{
    private Vector3 originalPosition;
    public float vibrateDuration = 2f; 
    public float vibrateMagnitude = 0.24f; 
    public float disappearDuration = 2f; 
    private Renderer platformRenderer;
    private Collider2D platformCollider;

    private void Start()
    {
        originalPosition = transform.position; 
        Debug.Log(originalPosition.ToString());
        platformRenderer = GetComponent<Renderer>(); 
        platformCollider = GetComponent<Collider2D>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(VibrateAndDisappear());
        }
    }

    private IEnumerator VibrateAndDisappear()
    {
        yield return StartCoroutine(Vibrate(vibrateDuration));

        if (platformRenderer != null) platformRenderer.enabled = false;
        if (platformCollider != null) platformCollider.enabled = false;

        yield return new WaitForSeconds(disappearDuration);

        if (platformRenderer != null) platformRenderer.enabled = true;
        if (platformCollider != null) platformCollider.enabled = true;
    }

    private IEnumerator Vibrate(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-vibrateMagnitude, vibrateMagnitude),
                Random.Range(-vibrateMagnitude, vibrateMagnitude),
                0);

            transform.localPosition = originalPosition + randomOffset;

            elapsed += Time.deltaTime;

            yield return null;
        }

    }

}
