using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool shieldActive = false; 
    public float timer = 10f;
    private SpriteRenderer spriteRenderer;
    public GameObject gameOverCanvas;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    public float speed = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public bool isHitByBullet = false;
    public bool shieldPicked = false;
    public float jump = 20f;
    public Transform levelParent;
    private bool isGameOver = false; 
    public bool scounter = false;
    private HUDController hudController;
    DistTracker distTracker;
    Send2Google send2Google;
    private Shield shield;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        GameObject senderObject = GameObject.Find("DistTracker");
        GameObject senderGoogle = GameObject.Find("Person");
        if (senderObject != null)
        {
            distTracker = senderObject.GetComponent<DistTracker>();
        }
        if (senderGoogle != null)
        {
            send2Google = senderGoogle.GetComponent<Send2Google>();
        }

        hudController = FindObjectOfType<HUDController>();
        shield = FindObjectOfType<Shield>();

        GameObject hudPrefab = Resources.Load<GameObject>("HUDCanvas");
        if (hudPrefab != null && hudController == null)
        {
            Instantiate(hudPrefab);
            hudController = FindObjectOfType<HUDController>(); 
        }
        else if (hudController == null)
        {
            Debug.LogError("HUDController or HUDCanvas not found!");
        }
    }

    void Update()
    {
        if (!isGameOver)
        {
            Move();
            Jump();
            CheckIfGrounded();
            ApplyCustomGravity();
        }
        else
        {
            CheckForRestart();
        }
    }

    void Move()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            send2Google.numOfJump += 1;
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ThresholdBorder"))
        {
            Debug.Log("Player collided with a platform. Game Over triggered.");
            GameOver();  
        }
    }
   

    void GameOver()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if ( currentSceneName != "Level 3")
        {
            distTracker.SendLevel1();
        }
        Time.timeScale = 0f;  
        gameOverCanvas.SetActive(true);  
        isGameOver = true;  

        LevelRotation.rotationPaused = false;
        PlayerDiamondCollision.ResetDiamondState();

        if (isHitByBullet)
        {
            Debug.Log("Player died from bullet");
        } else
        {
            Debug.Log("Player died naturally");
        }
        if (currentSceneName == "Level 3" || currentSceneName == "Level 4")
        {
            send2Google.SendBulletData(isHitByBullet, currentSceneName);
        }
        isHitByBullet = false;

        if (currentSceneName == "Level 3" && IsShieldActive()==false)
        {
            shieldPicked = false;
            send2Google.SendShield(shieldPicked);
        }

        if (currentSceneName == "Level 2" )
        {
            if(PlayerDiamondCollision.counter == false)
            {
                send2Google.SendFreeze("No");
            }
            
        }
        if (currentSceneName == "Level 4")
        {
            if (PlayerDiamondCollision.fcounter == true && scounter == true)
            {
                send2Google.SendPU("Both");
            }
            else if (PlayerDiamondCollision.fcounter == true && scounter == false)
            {
                send2Google.SendPU("Freeze Rotation");
            }
            else if (PlayerDiamondCollision.fcounter == false && scounter == true)
            {
                send2Google.SendPU("Shield");
            }
            else
            {
                send2Google.SendPU("None");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Bullet"))
        {
            isHitByBullet = false;
        }
    }

    public bool IsGameOver()
    {
        return isGameOver; 
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 1.5f, groundLayer);
    }

    void ApplyCustomGravity()
    {
        Vector2 gravityDirection = (new Vector3(0, 0, 0) - levelParent.position).normalized;

        rb.AddForce(gravityDirection * Physics2D.gravity.magnitude, ForceMode2D.Force);
    }

    void CheckForRestart()
    {
        if (Input.anyKeyDown)
        {
            Time.timeScale = 1f;  

            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();

            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }


    public void SetShieldActive(bool isActive)
    {
        shieldActive = isActive;
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (isActive)
        {
            if (currentSceneName == "Level 2")
            {
                shieldPicked = true;
                send2Google.SendShield(shieldPicked);
            }
            else if (currentSceneName == "Level 4")
            {
                scounter = true;
            }

            StartCoroutine(ShieldTimer());

            if (hudController != null)
            {
                hudController.ActivateShield();  
            }
            else
            {
                Debug.LogError("HUDController reference is missing.");
            }
        }
    }

    private IEnumerator ShieldTimer()
    {
        spriteRenderer.color = Color.blue;
        yield return new WaitForSeconds(timer);
        spriteRenderer.color = Color.white;
        shieldActive = false;
        PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();
        if (popUp != null)
        {
            popUp.ShowPopUp("Shield Deactivated!");
        }

    }

    public bool IsShieldActive()
    {
        return shieldActive;
    }

    public IEnumerator RespawnPowerup(GameObject powerup, Transform parentPowerup, Vector3 powerupPosition)
    {
        powerup.SetActive(false);

        yield return new WaitForSeconds(15);

        if (parentPowerup != null)
        {
            powerup.transform.SetParent(parentPowerup); 
            powerup.transform.localPosition = powerupPosition; 
        }

        powerup.SetActive(true); 
    }

}

