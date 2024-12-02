using System.Collections;
using UnityEngine;

public class ResizingPlatform : MonoBehaviour
{
    [Header("Platform Resizing Settings")]
    public float targetScaleX = 2.0f;   
    public float resizeDuration = 1.5f;  

    private float initialScaleX;         
    private bool isExpanding = true;

    void Start()
    {
        initialScaleX = transform.localScale.x;

        transform.localScale = new Vector3(initialScaleX, 0.3f, 1.0f);

        StartCoroutine(ResizePlatform());
    }

    IEnumerator ResizePlatform()
    {
        while (true)
        {
            float elapsedTime = 0f;
            Vector3 startScale = transform.localScale;
            Vector3 targetScale = isExpanding
                ? new Vector3(targetScaleX, 0.3f, 1.0f)  
                : new Vector3(initialScaleX, 0.3f, 1.0f); 

            while (elapsedTime < resizeDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / resizeDuration;
                transform.localScale = Vector3.Lerp(startScale, targetScale, t);
                yield return null;
            }

            isExpanding = !isExpanding;
            yield return new WaitForSeconds(0.1f); 
        }
    }
}
