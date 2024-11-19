using System.Collections;
using UnityEngine;

public class LevelRotation : MonoBehaviour
{
    public Material skyboxMaterial; // Assign your skybox material here
    private float skyboxRotation = 0f; // Tracks the skybox rotation
    public float rotationSpeed = 30f;
    public Transform levelParent;
    public Transform antiRotatPlatforms;
    public Camera mainCamera;
    public float zoomDuration = 3f; // Time it takes to zoom in to the player
    public float toggleZoomDuration = 0.5f; // Duration for smooth zoom for toggle
    public static bool rotationPaused = false;

    private float currentRotation = 0f;
    private Vector2 lastPlayerPosition;
    private float targetRotation = 0f;
    private bool isZooming = true;
    private float fullLevelHeight;


    // For Hmove and Vmove platforms
    public Transform hMovingPlatforms;  // Reference to horizontal moving platforms
    public Transform vMovingPlatforms;  // Reference to vertical moving platforms
    // Public variables for HMove
    public float hMoveDistance = 3f;  // HMove distance
    public float hMoveSpeed = 1f;     // HMove speed
    // Public variables for VMove
    public float vMoveDistance = 3f;  // VMove distance
    public float vMoveSpeed = 1f;     // VMove speed
    private Vector3 hStartPosition;  // Start position of horizontal platforms
    private Vector3 vStartPosition;  // Start position of vertical platforms

    // Zoom variables for toggling between views
    private bool isToggleZooming = false; // To check if zoom toggle is in progress
    private bool isZoomedOutView = false; // Track the zoomed-out state
    private float playerZoom = 15f; // Zoom level for player-focused view
    private float mapZoom; // Full map zoom level

    void Start()
    {
        lastPlayerPosition = transform.position;

        // Calculate full level height and start the camera zoomed out to show the entire level
        fullLevelHeight = CalculateLevelBounds().size.y;
        mapZoom = fullLevelHeight * 0.6f; // Set map view zoom level
        mainCamera.orthographicSize = fullLevelHeight * 0.6f; // Start zoomed out (adjusted multiplier)


        // Saving local positions at start, to allow Hmove & Vmove
        if (hMovingPlatforms != null)
        {
            hStartPosition = hMovingPlatforms.localPosition;
        }

        if (vMovingPlatforms != null)
        {
            vStartPosition = vMovingPlatforms.localPosition;
        }


        // Start the coroutine to smoothly zoom in on the player
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
            RotateSkybox(horizontalInput);
            foreach (Transform platform in antiRotatPlatforms)
            {
                platform.Rotate(Vector3.forward * rotationSpeed * horizontalInput * Time.deltaTime);
            }
        }

         MoveHPlatforms();
         MoveVPlatforms();
    }

    private void RotateSkybox(float horizontalInput)
    {
        if (skyboxMaterial != null)
        {
            // **Adjust the skybox rotation based on player input**
            skyboxRotation += horizontalInput * rotationSpeed * Time.deltaTime;

            // **Update the _Rotation property in the skybox material**
            skyboxMaterial.SetFloat("_Rotation", skyboxRotation);
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

        }
        // Adjust camera to keep focus on the player
        AdjustCamera();

        
        //Toggle Zoom Settings
            // Check for 'M' key to toggle zoom
            if (Input.GetKeyDown(KeyCode.M) && !isToggleZooming)
            {
                ToggleZoom();
            }

            // Cancel zoom out if directional keys are used
            if (isZoomedOutView && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
            {
                ToggleZoom();
            }
    }

    void AdjustCamera()
    {
        // Calculate the vertical offset to place the player at 75% from the bottom
        float verticalOffset = mainCamera.orthographicSize * 0.5f;

        // Focus the camera on the player with the vertical offset applied
        mainCamera.transform.localPosition = new Vector3(transform.position.x, transform.position.y + verticalOffset, mainCamera.transform.position.z);
    }


    void MoveHPlatforms()
    {
        if (hMovingPlatforms != null)
        {
            // Move horizontal platforms left and right using a sine wave relative to time
            float offset = Mathf.Sin(Time.time * hMoveSpeed) * hMoveDistance;
            hMovingPlatforms.localPosition = new Vector3(hStartPosition.x + offset, hStartPosition.y, hStartPosition.z);
        }
    }

    void MoveVPlatforms()
    {
        if (vMovingPlatforms != null)
        {
            // Move vertical platforms up and down using a sine wave relative to time
            float offset = Mathf.Sin(Time.time * vMoveSpeed) * vMoveDistance;
            vMovingPlatforms.localPosition = new Vector3(vStartPosition.x, vStartPosition.y + offset, vStartPosition.z);
        }
    }

    void ToggleZoom()
    {
        if (isToggleZooming) return; // Prevent multiple toggles at once
        float targetZoom = isZoomedOutView ? playerZoom : mapZoom;

        // Start the zoom toggle coroutine
        StartCoroutine(ToggleZoomCoroutine(targetZoom));
    }


    IEnumerator SmoothZoomIn()
    {
        
        // Hold the zoomed-out view for 2 seconds
        yield return new WaitForSeconds(1.5f);
        
        // Zoom from the full level view to the target zoom size (15) over zoomDuration seconds
        float elapsedTime = 0f;
        float initialZoom = mainCamera.orthographicSize;
        float targetZoom = 15f; // Lock the final zoom size to 15

        while (elapsedTime < zoomDuration)
        {
            // Smoothly interpolate the orthographic size from the full level view to the target zoom
            mainCamera.orthographicSize = Mathf.Lerp(initialZoom, targetZoom, elapsedTime / zoomDuration);

            // Keep the camera focused on the player while zooming in
            AdjustCamera();

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Set the final orthographic size to the target value of 15
        mainCamera.orthographicSize = targetZoom;
        isZooming = false; // End the zooming process
    }

    public IEnumerator PauseRotationForSeconds(float duration)
    {
        rotationPaused = true;
        yield return new WaitForSeconds(duration);
        rotationPaused = false;
    }

        IEnumerator ToggleZoomCoroutine(float targetZoom)
    {
        isToggleZooming = true;
        float elapsedTime = 0f;
        float initialZoom = mainCamera.orthographicSize;

        while (elapsedTime < toggleZoomDuration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(initialZoom, targetZoom, elapsedTime / toggleZoomDuration);
            AdjustCamera();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.orthographicSize = targetZoom;
        isZoomedOutView = !isZoomedOutView; // Toggle state
        isToggleZooming = false;
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
