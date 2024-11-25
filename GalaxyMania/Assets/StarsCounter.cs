using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class StarsCounter : MonoBehaviour
{
    public TextMeshProUGUI starsText; // Reference to the UI Text component
    public GameObject[] portals; // Array to hold multiple portal GameObjects
    public GameObject[] arrows; // Array to hold multiple portal GameObjects
    public SpriteRenderer[] starImages; // Array for the star UI Images (or Sprite Renderers if not UI)
    private int starsCount = 0; // Track the number of stars collected
    private int totalStars; // Total number of stars in the scene

    private void Start()
    {
        // Count all stars in the scene
        totalStars = GameObject.FindGameObjectsWithTag("Star").Length;

        // Update the text initially
        starsText.text = "Stars: 0 / " + totalStars;

        // Ensure all portals are initially inactive
        foreach (GameObject portal in portals)
        {
            if (portal != null)
                DisablePortalFunctionality(portal); // Disable portal functionality
        }

        foreach (GameObject arrow in arrows)
        {
            if (arrow != null)
                arrow.SetActive(false);
        }

        foreach (SpriteRenderer starImage in starImages)
        {
            if (starImage != null)
            {
                starImage.color = Color.gray; // Set a gray color to indicate inactive stars
            }
        }
    }

    // Method to add stars
    public void AddStar()
    {
        if (starsCount < starImages.Length)
        {
            // Update the corresponding star image to golden
            starImages[starsCount].color = Color.yellow;
        }

        starsCount++;
        starsText.text = "Stars: " + starsCount + " / " + totalStars;

        // Check if all stars are collected
        if (starsCount >= totalStars)
        {
            foreach (GameObject portal in portals)
            {
                if (portal != null)
                    EnablePortalFunctionality(portal); // Activate portal functionality

                string currentLevelName = SceneManager.GetActiveScene().name;
                if (currentLevelName == "Tutorial Portal")
                {
                    PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();
                    if (popUp != null)
                    {
                        popUp.ShowPopUp("Collect all stars \"to activate portals!\"");
                    }
                }
                else if (currentLevelName.Contains("Level"))
                {
                    PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();
                    if (popUp != null)
                    {
                        popUp.ShowPopUp("Portals Activated!");
                    }
                }
            }
            foreach (GameObject arrow in arrows)
            {
                if(arrow != null)
                {
                    arrow.SetActive(true);
                }
            }
        }
    }

    // Disable portal functionality
    private void DisablePortalFunctionality(GameObject portal)
    {
        // Example: Disable a collider
        Collider2D portalCollider = portal.GetComponent<Collider2D>();
        if (portalCollider != null)
        {
            portalCollider.enabled = false;
        }

        // Example: Disable a portal script
        Portal portalScript = portal.GetComponent<Portal>();
        if (portalScript != null)
        {
            portalScript.enabled = false;
        }
    }

    // Enable portal functionality
    private void EnablePortalFunctionality(GameObject portal)
    {
        // Example: Enable a collider
        Collider2D portalCollider = portal.GetComponent<Collider2D>();
        if (portalCollider != null)
        {
            portalCollider.enabled = true;
        }

        // Example: Enable a portal script
        Portal portalScript = portal.GetComponent<Portal>();
        if (portalScript != null)
        {
            portalScript.enabled = true;
        }
    }
}
