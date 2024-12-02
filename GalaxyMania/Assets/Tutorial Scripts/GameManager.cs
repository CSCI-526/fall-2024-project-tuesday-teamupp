using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player; 

    private RotationHandler rotationHandler;
    private MovingPlatforms platformMover;
    private CameraController cameraController;

    void Start()
    {
        rotationHandler = player.GetComponent<RotationHandler>();
        platformMover = player.GetComponent<MovingPlatforms>();
        cameraController = player.GetComponent<CameraController>();
    }

}