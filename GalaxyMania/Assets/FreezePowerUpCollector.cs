using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDiamondCollision : MonoBehaviour
{
    public static bool hasDiamond = false; 
    public string freezePicked;
    public static bool counter = false;
    public static bool fcounter = false;
    Send2Google send2Google;
    Shield shield;
    private PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        GameObject senderObject = GameObject.Find("Person");
        shield = FindObjectOfType<Shield>();
        if (senderObject != null)
        {
            send2Google = senderObject.GetComponent<Send2Google>();
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void Update()
    {
        string currentLevelName = GetCurrentLevelName();
        if (hasDiamond && Input.GetKeyDown(KeyCode.E))
        {
            hasDiamond = false;
            Debug.Log("Diamond powerup used!");
            if (currentLevelName == "Level 2")
            {
                send2Google.SendFreeze("Yes");
            }
            else if (currentLevelName == "Level 4")
            {
                fcounter = true;
            }
            StartCoroutine(FindObjectOfType<LevelRotation>().PauseRotationForSeconds(10f));

            HUDController hudController = FindObjectOfType<HUDController>(); 
            if (hudController != null)
            {
                hudController.UseFreezePowerUp();  
            }
            else
            {
                Debug.LogError("HUDController not found!");
            }
        }
    }

    public string GetCurrentLevelName()
    {
        return SceneManager.GetActiveScene().name;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string currentLevelName = GetCurrentLevelName();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Diamond"))
        {
            Debug.Log("Diamond collected!");
            GameObject diamond = collision.collider.gameObject;

            Debug.Log("Respawn Coroutine started!");
            StartCoroutine(playerController.RespawnPowerup(diamond, diamond.transform.parent, diamond.transform.localPosition));
            hasDiamond = true;

            PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();
            if (popUp != null)
            {
                popUp.ShowPopUp("Press E to freeze \"Level Rotation!\"");
            }

            HUDController hudController = FindObjectOfType<HUDController>(); 
            if (hudController != null)
            {
                hudController.CollectFreeze();  
            }
        }

    }

    public static void ResetDiamondState()
    {
        PlayerDiamondCollision.hasDiamond = false; 
    }
}
