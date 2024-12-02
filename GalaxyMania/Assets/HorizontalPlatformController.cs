using System;
using UnityEngine;

public class HorizontalPlatformController : MonoBehaviour
{
    public float rotationThreshold = 180f; 
    private Renderer squareRenderer;
    private BoxCollider2D boxCollider;
    private Transform levelParent;

    private void Awake()
    {
        squareRenderer = GetComponent<Renderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        levelParent = transform.parent; 
    }

    void Update()
    {
        if (levelParent != null)
        {
            float currentRotation = levelParent.rotation.eulerAngles.z; 

            if (currentRotation >= -rotationThreshold && currentRotation <= rotationThreshold)
            {
                SetActiveState(true); 
            }
            else
            {
                SetActiveState(false);
            }
        }
    }

    public void SetActiveState(bool isActive)
    {
        if (isActive)
        {
            squareRenderer.material.color = new Color32(59, 180, 89, 255);
        }
        else
        {
            squareRenderer.material.color = new Color(1, 0, 0, 0.5f); 
            EnableCollider(false);
        }
    }

    private void EnableCollider(bool enable)
    {
        if (boxCollider != null)
        {
            boxCollider.enabled = enable;
        }
        else if (enable)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }
    }
}
