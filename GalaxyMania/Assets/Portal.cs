using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject targetExit; // Reference to the exit portal
    public float teleportOffset = 1f; // How far above the exit portal to place the player

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player and we have a target exit
        if (collision.CompareTag("Player") && targetExit != null)
        {
            TeleportPlayer(collision.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        // Calculate teleport position above the exit portal
        Vector3 targetPosition = targetExit.transform.position;
        targetPosition.y += teleportOffset;

        // Teleport the player
        player.transform.position = targetPosition;
    }
}