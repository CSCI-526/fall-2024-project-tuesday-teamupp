using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public Transform hMovingPlatforms;
    public Transform vMovingPlatforms;
    public float hMoveDistance = 3f;
    public float hMoveSpeed = 1f;
    public float vMoveDistance = 3f;
    public float vMoveSpeed = 1f;

    private Vector3 hStartPosition;
    private Vector3 vStartPosition;

    void Start()
    {
        if (hMovingPlatforms != null)
        {
            hStartPosition = hMovingPlatforms.localPosition;
        }

        if (vMovingPlatforms != null)
        {
            vStartPosition = vMovingPlatforms.localPosition;
        }
    }

    void FixedUpdate()
    {
        MoveHPlatforms();
        MoveVPlatforms();
    }

    private void MoveHPlatforms()
    {
        if (hMovingPlatforms != null)
        {
            float offset = Mathf.Sin(Time.time * hMoveSpeed) * hMoveDistance;
            hMovingPlatforms.localPosition = new Vector3(hStartPosition.x + offset, hStartPosition.y, hStartPosition.z);
        }
    }

    private void MoveVPlatforms()
    {
        if (vMovingPlatforms != null)
        {
            float offset = Mathf.Sin(Time.time * vMoveSpeed) * vMoveDistance;
            vMovingPlatforms.localPosition = new Vector3(vStartPosition.x, vStartPosition.y + offset, vStartPosition.z);
        }
    }
}
