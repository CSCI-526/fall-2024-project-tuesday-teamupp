using UnityEngine;

public class HorizontalPlatformMover : MonoBehaviour
{
    public float moveDistance = 3f;  // Distance the platform will move horizontally
    public float moveSpeed = 1f;     // Speed of horizontal movement

    private Vector3 startPosition;

    void Start()
    {
        // Save the initial position of the platform
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the horizontal offset using a sine wave
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        // Apply the offset to the platform's position
        transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
    }
}
