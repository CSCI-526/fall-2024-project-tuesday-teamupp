using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool shieldActive = false; // Flag to indicate if the player is shielded
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
    //public Transform player;
    public float jump = 20f;
    public Transform levelParent;
    private bool isGameOver = false; // Track if the game is over
    //private bool isBeyondThreshold = false;
    private HUDController hudController;
    //public float borderThresholdDistance = 300f;
    //private Collider2D borderCollider;  
    //private bool isCheckingDistance = false;
    DistTracker distTracker;
    Send2Google send2Google;
    private Shield shield;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        gameOverCanvas.SetActive(false);  // Ensure the Game Over screen is hidden at the start
        //GameObject senderObject = GameObject.Find("Person");
        GameObject senderObject = GameObject.Find("DistTracker");
        GameObject senderGoogle = GameObject.Find("Person");
        // Get the Send2Google component from the GameObject
        if (senderObject != null)
        {
            distTracker = senderObject.GetComponent<DistTracker>();
        }
        if (senderGoogle != null)
        {
            send2Google = senderGoogle.GetComponent<Send2Google>();
        }

        // Get the HUDController instance
        hudController = FindObjectOfType<HUDController>();
        shield = FindObjectOfType<Shield>();

        // Instantiate the HUD if needed and assign HUDController
        GameObject hudPrefab = Resources.Load<GameObject>("HUDCanvas");
        if (hudPrefab != null && hudController == null)
        {
            Instantiate(hudPrefab);
            hudController = FindObjectOfType<HUDController>(); // Find HUDController after instantiating the HUD
        }
        else if (hudController == null)
        {
            Debug.LogError("HUDController or HUDCanvas not found!");
        }



        // Get the Send2Google component from the GameObject
        //if (senderObject != null)
        //{
        //    send2Google = senderObject.GetComponent<Send2Google>();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            // Normal game behavior
            Move();
            Jump();
            CheckIfGrounded();
            ApplyCustomGravity();
            // if (isCheckingDistance && borderCollider != null)
            // {
            //     CheckDistanceFromBorder();
            // }
        }
        else
        {
            // Game is over, wait for key press to restart
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
        //Debug.Log(isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            send2Google.numOfJump += 1;
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        //if (other.CompareTag("Border"))
        if (other.CompareTag("ThresholdBorder"))
        {
            Debug.Log("Player collided with a platform. Game Over triggered.");
            GameOver();  // Trigger the Game Over sequence  
        }
    }

    // void CheckDistanceFromBorder()
    // {
    //     Calculate the distance from the player to the specific border
    //     float distanceFromBorder = Vector2.Distance(borderCollider.ClosestPoint(transform.position), transform.position);
    //     Debug.Log("Distance from border: " + distanceFromBorder + " Border threshold: " + borderThresholdDistance);

    //     if (distanceFromBorder > borderThresholdDistance)
    //     {
    //         isBeyondThreshold = true;
    //     }
    //     else
    //     {
    //         isBeyondThreshold = false;  // Reset if the player moves back inside the threshold
    //     }

    //     Check conditions based on the distance and whether the player collected the triangle
    //     if (isBeyondThreshold && PlayerTriangleCollision.collectTriangle)
    //     {
    //        Time.timeScale = 1f;  // Unfreeze the game
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
    //        isCheckingDistance = false;  // Stop checking distance
    //     }
    //     else if (isBeyondThreshold)
    //     {
    //        Debug.Log("Game Over triggered. Distance from border: " + distanceFromBorder + " Border threshold: " + borderThresholdDistance);
    //        GameOver();
    //        isCheckingDistance = false;  // Stop checking distance
    //     }

    //     if (isBeyondThreshold)
    //     {
    //         LevelRotation.rotationPaused = false;
    //         PlayerDiamondCollision.ResetDiamondState();
    //         GameOver();
    //         isCheckingDistance = false;
    //     }
    // }

    void GameOver()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if ( currentSceneName != "Level 3")
        {
            distTracker.sendlevel1();
        }
        Time.timeScale = 0f;  // Freeze the game
        gameOverCanvas.SetActive(true);  // Show the Game Over screen
        isGameOver = true;  // Mark the game as over

        // Reset rotationPaused to ensure it's not frozen after respawn
        LevelRotation.rotationPaused = false;
        PlayerDiamondCollision.ResetDiamondState();

        if (isHitByBullet)
        {
            Debug.Log("Player died from bullet");
        } else
        {
            Debug.Log("Player died naturally");
        }
        if (currentSceneName == "Level 3")
        {
            send2Google.SendBulletData(isHitByBullet);
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
        return isGameOver;  // Return whether the game is over
    }
    void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 2f, groundLayer);
        //Debug.Log("The ground check position is" +groundCheck.position + "and isGrounded is " + isGrounded);
    }

    void ApplyCustomGravity()
    {
        // Calculate the direction from the player downwards in world space
        Vector2 gravityDirection = (new Vector3(0, 0, 0) - levelParent.position).normalized;

        // Apply gravity towards the player's local down direction
        rb.AddForce(gravityDirection * Physics2D.gravity.magnitude, ForceMode2D.Force);
    }

    void CheckForRestart()
    {
        // If any key is pressed, restart the game
        if (Input.anyKeyDown)
        {
            Time.timeScale = 1f;  // Unfreeze the game

            // Reset rotationPaused before restarting the scene
            LevelRotation.rotationPaused = false;
            PlayerDiamondCollision.ResetDiamondState();

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }


    public void SetShieldActive(bool isActive)
    {
        shieldActive = isActive;
        if (isActive)
        {
            shieldPicked = true;
            send2Google.SendShield(shieldPicked);
            StartCoroutine(ShieldTimer());

            // Notify HUDController to update shield UI
            if (hudController != null)
            {
                hudController.ActivateShield();  // This updates the HUD and handles fading opacity and timer
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

    void OnApplicationQuit()
    {
        //if (send2Google != null)
        //{
        //    send2Google.Send(triangleCollectionState["Level 2"], triangleCollectionState["Level 3"]);  // Example values for getsavelevel2 and getsavelevel3
        //}
    }

    public void GameEnd()
    {
        //if (send2Google != null)
        //{
        //    send2Google.Send(triangleCollectionState["Level 2"], triangleCollectionState["Level 3"]);  // Example values for getsavelevel2 and getsavelevel3
        //}
    }
}

