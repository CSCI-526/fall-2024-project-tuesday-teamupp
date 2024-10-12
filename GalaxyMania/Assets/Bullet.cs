using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 direction;
    private float playerDisplacementForce;
    private Vector2 levelBoundaryMin;
    private Vector2 levelBoundaryMax;

    public void Initialize(Vector2 dir, float force)
    {
        direction = dir;
        playerDisplacementForce = force;
    }

    private void Start()
    {
        // Get the level boundaries
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        if (level != null)
        {
            // Get the bounds of the level's collider
            Collider2D levelCollider = level.GetComponent<Collider2D>();
            if (levelCollider != null)
            {
                Bounds bounds = levelCollider.bounds;
                levelBoundaryMin = new Vector2(bounds.min.x, bounds.min.y); // Minimum boundary
                levelBoundaryMax = new Vector2(bounds.max.x, bounds.max.y); // Maximum boundary
            }
        }
    }

    private void Update()
    {
        // Check if the bullet is out of bound
        CheckIfOutOfBounds();
    }

    private void CheckIfOutOfBounds()
    {
        // Get the bullet's position
        Vector2 bulletPosition = transform.position;

        // Check if the bullet is outside the defined boundaries
        if (bulletPosition.x < levelBoundaryMin.x || bulletPosition.x > levelBoundaryMax.x ||
            bulletPosition.y < levelBoundaryMin.y || bulletPosition.y > levelBoundaryMax.y)
        {
            Destroy(gameObject); // Destroy the bullet if it's out of bounds
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null && !player.IsShieldActive())
            {
                // Apply force to the player only if the shield is NOT active
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.AddForce(direction * playerDisplacementForce, ForceMode2D.Impulse);
                }
            }
            Destroy(gameObject);
        }
        else
        {
            // Destroy bullet on any collision
            Destroy(gameObject);
        }
    }
}
