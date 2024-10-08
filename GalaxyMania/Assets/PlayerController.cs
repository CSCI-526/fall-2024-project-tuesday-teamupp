using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
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
    public float fallThresholdY = -30f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameOverCanvas.SetActive(false);  // Ensure the Game Over screen is hidden at the start
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
            CheckIfPlayerFell();
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

    void CheckIfPlayerFell()
    {
        //Debug.Log($"Player Position: {transform.position}");
        // If the player falls below a certain Y position, trigger game over
        if (transform.position.y < fallThresholdY)  // Adjust this value depending on your level's height
        {
            if (levelParent.name == "Level 2" && PlayerTriangleCollision.collectTriangle)
            {
                Time.timeScale = 1f;  // Unfreeze the game
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
                return;
            }
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;  // Freeze the game
        gameOverCanvas.SetActive(true);  // Show the Game Over screen
        isGameOver = true;  // Mark the game as over
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 2f, groundLayer);
        //Debug.Log("The ground check position is" +groundCheck.position + "and isGrounded is " + isGrounded);
    }

    void ApplyCustomGravity()
    {
        // Calculate the direction from the player downwards in world space
        Vector2 gravityDirection = (new Vector3(0,0,0) - levelParent.position).normalized;

        // Apply gravity towards the player's local down direction
        rb.AddForce(gravityDirection * Physics2D.gravity.magnitude, ForceMode2D.Force);
    }

    void CheckForRestart()
    {
        // If any key is pressed, restart the game
        if (Input.anyKeyDown)
        {
            Time.timeScale = 1f;  // Unfreeze the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reload the current scene
        }
    }
}
