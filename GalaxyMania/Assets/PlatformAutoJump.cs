using UnityEngine;

public class PlatformAutoJump : MonoBehaviour
{
    public float jumpForce = 20f;  // Force for player to auto-jump

    // Auto-jump logic when player collides with the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player has landed on the platform
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player's Rigidbody2D component
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // Make the player auto-jump by applying vertical force
                playerRb.velocity = new Vector2(playerRb.velocity.y, jumpForce);  // Apply jump force
                Debug.Log("Player landed on platform, auto-jumping!");
            }
        }
    }
}
