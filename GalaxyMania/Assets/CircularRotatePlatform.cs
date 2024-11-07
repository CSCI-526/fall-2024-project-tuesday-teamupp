using UnityEngine;
public class CircularRotatePlatform : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private float speed = 0.3f;
    [SerializeField] private float startDelay = 4f;

    private Vector3 centerPoint;
    private float currentAngle = -Mathf.PI / 2;
    private float timer = 0f;
    private bool startRotating = false;

    private void Start()
    {
        // Store the center point as the platform's initial position
        centerPoint = transform.position;

        // Immediately move to starting position at the bottom of the circle
        float x = centerPoint.x + Mathf.Cos(currentAngle) * radius;
        float y = centerPoint.y + Mathf.Sin(currentAngle) * radius;
        transform.position = new Vector3(x, y, centerPoint.z);
        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle * Mathf.Rad2Deg + 90f);
    }

    private void Update()
    {
        if (!startRotating)
        {
            timer += Time.deltaTime;
            if (timer >= startDelay)
            {
                startRotating = true;
            }
            return;
        }

        // Only update the angle and position after the delay
        currentAngle += speed * Time.deltaTime;
        float x = centerPoint.x + Mathf.Cos(currentAngle) * radius;
        float y = centerPoint.y + Mathf.Sin(currentAngle) * radius;
        transform.position = new Vector3(x, y, centerPoint.z);
        float rotationAngle = currentAngle * Mathf.Rad2Deg + 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the circular path in the editor for visualization
        Gizmos.color = Color.yellow;
        Vector3 center = Application.isPlaying ? centerPoint : transform.position;
        const int segments = 32;
        Vector3 prevPoint = center + new Vector3(Mathf.Cos(0) * radius, Mathf.Sin(0) * radius, 0);
        for (int i = 1; i <= segments; i++)
        {
            float angle = (float)i / segments * Mathf.PI * 2;
            Vector3 nextPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            Gizmos.DrawLine(prevPoint, nextPoint);
            prevPoint = nextPoint;
        }
    }
}