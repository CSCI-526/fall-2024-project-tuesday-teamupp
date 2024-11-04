using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player; // Reference to the player object

    private RotationHandler rotationHandler;
    private MovingPlatforms platformMover;
    private CameraController cameraController;

    void Start()
    {
        // Fetch the components from the player
        rotationHandler = player.GetComponent<RotationHandler>();
        platformMover = player.GetComponent<MovingPlatforms>();
        cameraController = player.GetComponent<CameraController>();
    }

    void Update()
    {
        // Game-wide logic goes here, if needed
    }
}