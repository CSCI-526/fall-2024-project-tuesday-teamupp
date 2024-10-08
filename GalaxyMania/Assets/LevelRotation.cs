using System.Collections;
using UnityEngine;

public class LevelRotation : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public Transform levelParent;
    public Transform antiRotatPlatforms;
    public Camera mainCamera;
    public float zoomDuration = 3f; // Time it takes to zoom in to the player
    public static bool rotationPaused = false;

    private float currentRotation = 0f;
    private Vector2 lastPlayerPosition;
    private float targetRotation = 0f;
    private bool isZooming = true;

    void Start()
    {
        lastPlayerPosition = transform.position;

        // Ensure the camera size is always 15 for consistent behavior across levels
        mainCamera.orthographicSize = 15f;

        // Start the coroutine to smoothly zoom in on the player (if you want to keep smooth zoom)
        StartCoroutine(SmoothZoomIn());
    }

    private void FixedUpdate()
    {
        if (!rotationPaused) // Check if rotation is paused
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            targetRotation += -horizontalInput * rotationSpeed * Time.deltaTime;
            currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * 5f);

            // Apply rotation to the level parent
            levelParent.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            // Apply the opposite rotation to the AntiRotatPlatforms
            foreach (Transform platform in antiRotatPlatforms)
            {
                platform.Rotate(Vector3.forward * rotationSpeed * horizontalInput * Time.deltaTime);
            }
        }
    }

    void Update()
    {
        if (!isZooming && !rotationPaused)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            targetRotation += -horizontalInput * rotationSpeed * Time.deltaTime;
            currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * 5f);

            // Apply rotation to the level parent
            levelParent.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            // Apply the opposite rotation to the AntiRotatPlatforms
            foreach (Transform platform in antiRotatPlatforms)
            {
                platform.Rotate(Vector3.forward * rotationSpeed * horizontalInput * Time.deltaTime);
            }

            // Update last player position
            lastPlayerPosition = transform.position;

            // Adjust camera to keep focus on the player
        }
        AdjustCamera();
    }

    void AdjustCamera()
    {
        // Calculate the vertical offset to place the player at 75% from the bottom
        float verticalOffset = mainCamera.orthographicSize * 0.5f;

        // Focus the camera on the player with the vertical offset applied
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y + verticalOffset, mainCamera.transform.position.z);
    }

    IEnumerator SmoothZoomIn()
    {
        // Zoom to a fixed size of 15 over zoomDuration seconds
        float elapsedTime = 0f;
        float initialZoom = mainCamera.orthographicSize;
        float fixedZoom = 15f; // Ensure the target zoom is fixed at 15

        while (elapsedTime < zoomDuration)
        {
            // Smoothly interpolate the orthographic size to the fixed value of 15
            mainCamera.orthographicSize = Mathf.Lerp(initialZoom, fixedZoom, elapsedTime / zoomDuration);

            AdjustCamera(); // Keep adjusting the camera position as needed

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final orthographic size is set to 15
        mainCamera.orthographicSize = fixedZoom;
        isZooming = false; // End the zooming process
    }

    public IEnumerator PauseRotationForSeconds(float duration)
    {
        rotationPaused = true;
        yield return new WaitForSeconds(duration);
        rotationPaused = false;
    }

    Bounds CalculateLevelBounds()
    {
        Bounds bounds = new Bounds(levelParent.position, Vector3.zero);
        Renderer[] renderers = levelParent.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        return bounds;
    }
}
