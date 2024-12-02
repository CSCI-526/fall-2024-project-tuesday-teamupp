using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinMessageHandler : MonoBehaviour
{
    public TextMeshProUGUI winText; 
    public Color targetColor = new Color(111f / 255f, 41f / 255f, 26f / 255f, 1f); 
    public float colorTolerance = 0.02f; 
    private bool wonGame = false;
    Send2Google send2Google;

    private void Start()
    {
\        winText.text = "";
        GameObject sendGoogleObject = GameObject.Find("Person");
        if (sendGoogleObject != null)
        {
            send2Google = sendGoogleObject.GetComponent<Send2Google>();
        }
    }

    void Update()
    {
        if (Input.anyKeyDown && wonGame)
        {
            Time.timeScale = 1f;  
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            wonGame = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer objectRenderer = collision.gameObject.GetComponent<SpriteRenderer>();

        if (objectRenderer != null)
        {
            Color objectColor = objectRenderer.color;

            if (IsColorClose(objectColor, targetColor, colorTolerance))
            {
                send2Google.SendCompleteLevelData(send2Google.timer, send2Google.numOfJump, "Level 4", CountPlatform.levelPlatforms["Level 4"].Count);
                winText.text = "You Win!";
                Time.timeScale = 0; 
                LevelRotation.rotationPaused = false;
                PlayerDiamondCollision.ResetDiamondState();
                wonGame = true;
            }
        }
    }

    private bool IsColorClose(Color color1, Color color2, float tolerance)
    {
        return Mathf.Abs(color1.r - color2.r) <= tolerance &&
               Mathf.Abs(color1.g - color2.g) <= tolerance &&
               Mathf.Abs(color1.b - color2.b) <= tolerance;
    }
}
