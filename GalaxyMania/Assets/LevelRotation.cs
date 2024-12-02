using System.Collections;
using UnityEngine;

public class LevelRotation : MonoBehaviour
{
    public Material skyboxMaterial; 
    private float skyboxRotation = 0f; 
    public float rotationSpeed = 30f;
    public Transform levelParent;
    public Transform antiRotatPlatforms;
    public Camera mainCamera;
    public float zoomDuration = 3f; 
    public float toggleZoomDuration = 0.5f;
    public static bool rotationPaused = false;

    private float currentRotation = 0f;
    private Vector2 lastPlayerPosition;
    private float targetRotation = 0f;
    private bool isZooming = true;
    private float fullLevelHeight;


    public Transform hMovingPlatforms;  
    public Transform vMovingPlatforms;  
    public float hMoveDistance = 3f;  
    public float hMoveSpeed = 1f;     
    public float vMoveDistance = 3f;  
    public float vMoveSpeed = 1f;     
    private Vector3 hStartPosition;  
    private Vector3 vStartPosition;

    private bool isToggleZooming = false; 
    private bool isZoomedOutView = false; 
    private float playerZoom = 15f; 
    private float mapZoom; 

    void Start()
    {
        lastPlayerPosition = transform.position;

        fullLevelHeight = CalculateLevelBounds().size.y;
        mapZoom = fullLevelHeight * 0.6f; 
        mainCamera.orthographicSize = fullLevelHeight * 0.6f; 

        if (hMovingPlatforms != null)
        {
            hStartPosition = hMovingPlatforms.localPosition;
        }

        if (vMovingPlatforms != null)
        {
            vStartPosition = vMovingPlatforms.localPosition;
        }

        StartCoroutine(SmoothZoomIn());

    }

    private void FixedUpdate()
    {
        if (!rotationPaused) 
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            targetRotation += -horizontalInput * rotationSpeed * Time.deltaTime;
            currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * 5f);

            levelParent.rotation = Quaternion.Euler(0f, 0f, currentRotation);
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
            skyboxRotation += horizontalInput * rotationSpeed * Time.deltaTime;

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

            levelParent.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            foreach (Transform platform in antiRotatPlatforms)
            {
                platform.Rotate(Vector3.forward * rotationSpeed * horizontalInput * Time.deltaTime);
            }

            lastPlayerPosition = transform.position;

        }
        AdjustCamera();

        

        if (Input.GetKeyDown(KeyCode.M) && !isToggleZooming)
        {
            ToggleZoom();
        }

        if (isZoomedOutView && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            ToggleZoom();
        }
    }

    void AdjustCamera()
    {
        float verticalOffset = mainCamera.orthographicSize * 0.5f;

        mainCamera.transform.localPosition = new Vector3(transform.position.x, transform.position.y + verticalOffset, mainCamera.transform.position.z);
    }


    void MoveHPlatforms()
    {
        if (hMovingPlatforms != null)
        {
            float offset = Mathf.Sin(Time.time * hMoveSpeed) * hMoveDistance;
            hMovingPlatforms.localPosition = new Vector3(hStartPosition.x + offset, hStartPosition.y, hStartPosition.z);
        }
    }

    void MoveVPlatforms()
    {
        if (vMovingPlatforms != null)
        {
            float offset = Mathf.Sin(Time.time * vMoveSpeed) * vMoveDistance;
            vMovingPlatforms.localPosition = new Vector3(vStartPosition.x, vStartPosition.y + offset, vStartPosition.z);
        }
    }

    void ToggleZoom()
    {
        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (isToggleZooming) return; 
        if  (currentSceneName == "Level 4") 
        {
            mapZoom = fullLevelHeight * 0.2f;
        }
        float targetZoom = isZoomedOutView ? playerZoom : mapZoom;

        StartCoroutine(ToggleZoomCoroutine(targetZoom));
    }


    IEnumerator SmoothZoomIn()
    {
        
        yield return new WaitForSeconds(1.5f);
        
        float elapsedTime = 0f;
        float initialZoom = mainCamera.orthographicSize;
        float targetZoom = 15f; 

        while (elapsedTime < zoomDuration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(initialZoom, targetZoom, elapsedTime / zoomDuration);

            AdjustCamera();

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.orthographicSize = targetZoom;
        isZooming = false; 
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
        isZoomedOutView = !isZoomedOutView; 
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
