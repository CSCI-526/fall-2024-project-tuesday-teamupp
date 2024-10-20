using UnityEngine;
using TMPro;

public class PowerUpPopUp : MonoBehaviour
{
    public TextMeshProUGUI popUpText; // Reference to the UI Text element
    private float displayDuration = 2f; // Time to display the text
    private float timer = 0f;
    private bool isInfoTextVisible = false; // Tracks if info text is currently displayed


    void Start()
    {
        popUpText.gameObject.SetActive(false); // Hide the text initially
    }

    public void ShowPopUp(string message)
    {
        Debug.Log("Text shown:" + message);
        popUpText.text = message; 
        popUpText.gameObject.SetActive(true); // Show the text
        timer = displayDuration; // Set the timer
    }

    // ShowInfoText for persistent display until manually hidden
    public void ShowInfoText(string message)
    {
        Debug.Log("Info text shown: " + message);
        popUpText.text = message;
        popUpText.gameObject.SetActive(true); // Show the text
        isInfoTextVisible = true; // Mark that info text is visible
    }

    // HideInfoText to manually hide persistent info text
    public void HideInfoText()
    {
        if(isInfoTextVisible)
        {
            Debug.Log("Info text hidden");
            popUpText.gameObject.SetActive(false); // Hide the text
            isInfoTextVisible = false; // Mark that info text is hidden
        }
        
    }

    void Update()
    {
        // Count down the timer and hide the text when time runs out
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                popUpText.gameObject.SetActive(false); // Hide the text
            }
        }
    }
}
