using System.Collections;
using UnityEngine;

public class ResizingPlatform : MonoBehaviour
{
    [Header("Platform Resizing Settings")]
    public float targetScaleX = 2.0f;    // The target X scale the platform will expand to
    public float resizeDuration = 1.5f;  // Duration of one resize cycle (expand or contract)

    private float initialScaleX;         // The initial X scale set in the editor
    private bool isExpanding = true;     // Tracks whether the platform is expanding or contracting

    // Start is called before the first frame update
    void Start()
    {
        // Get the initial X scale from the transform set in the editor
        initialScaleX = transform.localScale.x;

        // Fix Y and Z scales to the desired values
        transform.localScale = new Vector3(initialScaleX, 0.3f, 1.0f);

        // Start the resize coroutine
        StartCoroutine(ResizePlatform());
    }

    // Coroutine to handle the resizing of the platform
    IEnumerator ResizePlatform()
    {
        while (true)
        {
            float elapsedTime = 0f;
            Vector3 startScale = transform.localScale;
            Vector3 targetScale = isExpanding
                ? new Vector3(targetScaleX, 0.3f, 1.0f)  // Expand to targetScaleX on the X axis
                : new Vector3(initialScaleX, 0.3f, 1.0f); // Contract back to initialScaleX

            // Interpolate between the current and target scales over the resizeDuration
            while (elapsedTime < resizeDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / resizeDuration;
                transform.localScale = Vector3.Lerp(startScale, targetScale, t);
                yield return null;
            }

            // Toggle between expanding and contracting
            isExpanding = !isExpanding;
            yield return new WaitForSeconds(0.1f); // Optional pause between cycles
        }
    }
}
