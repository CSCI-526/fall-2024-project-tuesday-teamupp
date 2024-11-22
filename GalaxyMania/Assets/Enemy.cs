using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootInterval = 2f;
    public float bulletSpeed = 28f;
    public float playerDisplacementForce = 500f;

    private Transform player;
    private float shootTimer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;       
        shootTimer = 1f;
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    private void Shoot()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            float spawnOffset = 3.1f;
            Vector2 spawnPosition = (Vector2)transform.position + direction * spawnOffset;

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
            bullet.transform.SetParent(transform.parent);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * bulletSpeed;
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript == null)
            {
                bulletScript = bullet.AddComponent<Bullet>();
            }
            bulletScript.Initialize(direction, playerDisplacementForce);
        }
    }
}