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
        GameObject level = GameObject.FindGameObjectWithTag("Level");
        if (level != null)
        {
            Collider2D levelCollider = level.GetComponent<Collider2D>();
            if (levelCollider != null)
            {

                Bounds bounds = levelCollider.bounds;
                levelBoundaryMin = new Vector2(bounds.min.x, bounds.min.y); 
                levelBoundaryMax = new Vector2(bounds.max.x, bounds.max.y); 
            }
        }
    }

    private void Update()
    {
        CheckIfOutOfBounds();
    }

    private void CheckIfOutOfBounds()
    {
        Vector2 bulletPosition = transform.position;

        if (bulletPosition.x < levelBoundaryMin.x || bulletPosition.x > levelBoundaryMax.x ||
            bulletPosition.y < levelBoundaryMin.y || bulletPosition.y > levelBoundaryMax.y)
        {
            Destroy(gameObject); 
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null && !player.IsShieldActive())
            {
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    playerRb.AddForce(direction * playerDisplacementForce, ForceMode2D.Impulse);
                    player.isHitByBullet = true;
                }
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
