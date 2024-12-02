using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class StarsCounter : MonoBehaviour
{
    public TextMeshProUGUI starsText; 
    public GameObject[] portals; 
    public GameObject[] arrows; 
    public SpriteRenderer[] starImages; 
    private int starsCount = 0; 
    private int totalStars; 

    private void Start()
    {
        totalStars = GameObject.FindGameObjectsWithTag("Star").Length;

        starsText.text = "Stars: 0 / " + totalStars;

        foreach (GameObject portal in portals)
        {
            if (portal != null)
                DisablePortalFunctionality(portal); 
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
                starImage.color = Color.gray; 
            }
        }
    }

    public void AddStar()
    {
        if (starsCount < starImages.Length)
        {
            starImages[starsCount].color = Color.yellow;
        }

        starsCount++;
        starsText.text = "Stars: " + starsCount + " / " + totalStars;

        if (starsCount >= totalStars)
        {
            foreach (GameObject portal in portals)
            {
                if (portal != null)
                    EnablePortalFunctionality(portal); 

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

    private void DisablePortalFunctionality(GameObject portal)
    {
        Collider2D portalCollider = portal.GetComponent<Collider2D>();
        if (portalCollider != null)
        {
            portalCollider.enabled = false;
        }

        Portal portalScript = portal.GetComponent<Portal>();
        if (portalScript != null)
        {
            portalScript.enabled = false;
        }
    }

    private void EnablePortalFunctionality(GameObject portal)
    {
        Collider2D portalCollider = portal.GetComponent<Collider2D>();
        if (portalCollider != null)
        {
            portalCollider.enabled = true;
        }

        Portal portalScript = portal.GetComponent<Portal>();
        if (portalScript != null)
        {
            portalScript.enabled = true;
        }
    }
}
