using UnityEngine;

public class JumpMessage : MonoBehaviour
{
    PowerUpPopUp popUp;
    bool isJumpMessageActive = true;

    void Start()
    {
        // Find the PowerUpPopUp instance in the scene
        popUp = FindObjectOfType<PowerUpPopUp>();

        // Display the message initially
        if (popUp != null)
        {
            popUp.ShowPopUp("Spacebar"); // Replace with image
        }
    }

    void Update()
    {
        // Check if the player presses the spacebar and if the message is still active
        if (isJumpMessageActive && Input.GetKeyDown(KeyCode.Space))
        {
            // Hide the pop-up and deactivate the message
            if (popUp != null)
            {
                popUp.HideInfoText();  // Assuming you have a HidePopUp() method to hide the pop-up
            }
            isJumpMessageActive = false;
        }
    }
}
