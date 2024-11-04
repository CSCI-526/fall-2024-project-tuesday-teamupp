using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomDuration = 3f;
    public Transform player;
    public Transform levelParent;
    private bool isZooming = true;
    private float fullLevelHeight;

    void Start()
    {
        fullLevelHeight = CalculateLevelBounds().size.y;
        //mainCamera.orthographicSize = fullLevelHeight * 0.6f; // Start zoomed out
        //StartCoroutine(SmoothZoomIn());
    }

    void Update()
    {
        //if (!isZooming)
        //{
        //    AdjustCamera();
        //}
        AdjustCamera();
    }

    private void AdjustCamera()
    {
        float verticalOffset = mainCamera.orthographicSize * 0.5f;
        mainCamera.transform.localPosition = new Vector3(player.position.x, player.position.y + verticalOffset, mainCamera.transform.position.z);
    }

    private IEnumerator SmoothZoomIn()
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

    private Bounds CalculateLevelBounds()
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
