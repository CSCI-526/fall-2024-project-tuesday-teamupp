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
    //public Transform player;
    public float jump = 20f;
    public Transform levelParent;
    private bool isGameOver = false; // Track if the game is over
    private bool isBeyondThreshold = false;
    private HUDController hudController;
    public float borderThresholdDistance = 300f;
    private Collider2D borderCollider;  
    private bool isCheckingDistance = false;
    public static Dictionary<string, bool> triangleCollectionState = new Dictionary<string, bool>
    {
        {"Level 2", false},
        {"Level 3", false}
    };
    //Send2Google send2Google;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        gameOverCanvas.SetActive(false);  // Ensure the Game Over screen is hidden at the start
        //GameObject senderObject = GameObject.Find("Person");

        // Get the HUDController instance
        hudController = FindObjectOfType<HUDController>();

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
            if (isCheckingDistance && borderCollider != null)
            {
                CheckDistanceFromBorder();
            }
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
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Border"))
        {
            borderCollider = other;  
            isCheckingDistance = true;  
        }
    }

    void CheckDistanceFromBorder()
    {
        // Calculate the distance from the player to the specific border
        float distanceFromBorder = Vector2.Distance(borderCollider.ClosestPoint(transform.position), transform.position);
        //Debug.Log("Distance from border: " + distanceFromBorder + " Border threshold: " + borderThresholdDistance);

        if (distanceFromBorder > borderThresholdDistance)
        {
            isBeyondThreshold = true;
        }
        else
        {
            isBeyondThreshold = false;  // Reset if the player moves back inside the threshold
        }

        // Check conditions based on the distance and whether the player collected the triangle
        //if (isBeyondThreshold && PlayerTriangleCollision.collectTriangle)
        //{
        //    Time.timeScale = 1f;  // Unfreeze the game
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
        //    isCheckingDistance = false;  // Stop checking distance
        //}
        //else if (isBeyondThreshold)
        //{
        //    Debug.Log("Game Over triggered. Distance from border: " + distanceFromBorder + " Border threshold: " + borderThresholdDistance);
        //    GameOver();
        //    isCheckingDistance = false;  // Stop checking distance
        //}

        if (isBeyondThreshold)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            string[] parts = currentSceneName.Split(' ');
            if (parts.Length != 2 || !int.TryParse(parts[1], out int currentLevelNumber))
            {
                Debug.LogError("Invalid scene name format.");
                return;
            }

            // Iterate backwards through the levels from currentLevelNumber down to the first level
            for (int level = currentLevelNumber; level >= 2; level--)
            {
                string levelName = "Level " + level;
                Debug.Log(triangleCollectionState[levelName]);
                // Check if the level exists in the dictionary
                if (triangleCollectionState.TryGetValue(levelName, out bool canReload))
                {
                    if (canReload)
                    {
                        // Reload this level
                        Debug.Log("Reloading " + levelName);
                        Time.timeScale = 1f;  // Unfreeze the game
                        SceneManager.LoadScene(levelName);
                        isCheckingDistance = false;
                        return;
                    }
                    else
                    {
                        Debug.Log(levelName + " cannot be reloaded, checking previous level.");
                    }
                }
            }

            // If no level can be reloaded, trigger game over
            Debug.Log("No levels available for reloading. Game Over.");
            GameOver();
            isCheckingDistance = false;
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;  // Freeze the game
        gameOverCanvas.SetActive(true);  // Show the Game Over screen
        isGameOver = true;  // Mark the game as over

        // Reset rotationPaused to ensure it's not frozen after respawn
        LevelRotation.rotationPaused = false;
        PlayerDiamondCollision.ResetDiamondState();

        // Call the Send method, passing the appropriate values
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
            SceneManager.LoadScene("Level 1");
        }
    }


    public void SetShieldActive(bool isActive)
    {
        shieldActive = isActive;
        if (isActive)
        {
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

