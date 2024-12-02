using UnityEngine;

public class PlatformAutoJump : MonoBehaviour
{
    public float jumpForce = 20f;  

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.y, jumpForce);  
                Debug.Log("Player landed on platform, auto-jumping!");
            }
        }
    }
}
