using UnityEngine;

public class VerticalPlatformMover : MonoBehaviour
{
    public float moveDistance = 3f;  
    public float moveSpeed = 1f;     

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;

        transform.position = new Vector3(startPosition.x, startPosition.y + offset, startPosition.z);
    }
}
