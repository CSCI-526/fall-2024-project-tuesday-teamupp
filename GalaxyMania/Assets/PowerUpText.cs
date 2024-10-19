using UnityEngine;
using TMPro;

public class PowerUpPopUp : MonoBehaviour
{
    public TextMeshProUGUI popUpText; // Reference to the UI Text element
    private float displayDuration = 2f; // Time to display the text
    private float timer = 0f;

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
