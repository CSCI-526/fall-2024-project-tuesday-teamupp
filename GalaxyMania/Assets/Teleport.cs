using UnityEngine;

public class Teleport : MonoBehaviour
{
    public float teleportHeightOffset = 1f;
    public float colorTolerance = 0.02f;
    public float moveThreshold = 5f;
    private bool hasTeleported = false;
    private Vector3 lastTeleportedPosition;

    // Define your target color
    Color targetColor = new Color(105f / 255f, 45f / 255f, 195f / 255f, 1f);

    void Update()
    {
        if (hasTeleported && Vector3.Distance(transform.position, lastTeleportedPosition) > moveThreshold)
        {
            hasTeleported = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasTeleported) return;

        GameObject collidedObject = collision.gameObject;
        SpriteRenderer objectRenderer = collidedObject.GetComponent<SpriteRenderer>();

        if (objectRenderer != null && collidedObject.CompareTag("TeleportSource"))
        {
            Color objectColor = objectRenderer.color;

            if (IsColorClose(objectColor, targetColor, colorTolerance))
            {
                GameObject targetObject = FindTeleportTarget();

                if (targetObject != null)
                {
                    TeleportTo(targetObject);
                }
            }
        }
    }

    void TeleportTo(GameObject targetObject)
    {
        SpriteRenderer targetRenderer = targetObject.GetComponent<SpriteRenderer>();
        if (targetRenderer != null)
        {
            Vector3 platformRightSide = targetRenderer.bounds.max;
            Vector3 platformRightDirection = targetObject.transform.right;

            Vector3 targetPosition = platformRightSide + (platformRightDirection * (targetRenderer.bounds.size.x / 2));
            targetPosition.y += teleportHeightOffset;

            transform.position = targetPosition;

            hasTeleported = true;
            lastTeleportedPosition = transform.position;
        }
    }

    GameObject FindTeleportTarget()
    {
        GameObject[] teleportTargets = GameObject.FindGameObjectsWithTag("TeleportTarget");

        foreach (GameObject target in teleportTargets)
        {
            SpriteRenderer targetRenderer = target.GetComponent<SpriteRenderer>();
            if (targetRenderer != null && IsColorClose(targetRenderer.color, targetColor, colorTolerance))
            {
                return target;
            }
        }
        return null;
    }

    bool IsColorClose(Color32 color1, Color32 color2, float tolerance)
    {
        return Mathf.Abs(color1.r - color2.r) / 255f <= tolerance &&
               Mathf.Abs(color1.g - color2.g) / 255f <= tolerance &&
               Mathf.Abs(color1.b - color2.b) / 255f <= tolerance;
    }
}