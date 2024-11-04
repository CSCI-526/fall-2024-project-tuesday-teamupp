using UnityEngine;

public class VerticalPlatformMover : MonoBehaviour
{
    public float moveDistance = 3f;  // Distance the platform will move vertically
    public float moveSpeed = 1f;     // Speed of vertical movement

    private Vector3 startPosition;

    void Start()
    {
        // Save the initial position of the platform
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the vertical offset using a sine wave
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        // Apply the offset to the platform's position
        transform.position = new Vector3(startPosition.x, startPosition.y + offset, startPosition.z);
    }
}
