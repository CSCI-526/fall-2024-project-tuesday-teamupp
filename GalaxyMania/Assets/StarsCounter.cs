using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarsCounter : MonoBehaviour
{
    //public Text starsText; // Reference to the UI Text component
    public GameObject[] portals; // Array to hold multiple portal GameObjects
    private int starsCount = 0; // Track the number of stars collected
    private int totalStars; // Total number of stars in the scene

    private void Start()
    {
        // Count all stars in the scene
        totalStars = GameObject.FindGameObjectsWithTag("Star").Length;

        // Update the text initially
        //starsText.text = "Stars: 0 / " + totalStars;

        // Ensure all portals are initially inactive
        foreach (GameObject portal in portals)
        {
            if (portal != null)
                portal.SetActive(false);
        }
    }

    // Method to add stars
    public void AddStar()
    {
        starsCount++;
        //starsText.text = "Stars: " + starsCount + " / " + totalStars;

        // Check if all stars are collected
        if (starsCount >= totalStars)
        {
            foreach (GameObject portal in portals)
            {
                if (portal != null)
                    portal.SetActive(true); // Make each portal visible

                string currentLevelName = SceneManager.GetActiveScene().name;
                if (currentLevelName == "Tutorial Portal")
                {
                    PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();
                    if (popUp != null)
                    {
                        popUp.ShowPopUp("Collect all stars \"to find hidden portals!\"");
                    }
                }
                else if(currentLevelName.Contains("Level"))
                {
                    PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();
                    if (popUp != null)
                    {
                        popUp.ShowPopUp("Portals Activated!");
                    }
                }
            }
        }
    }
}
