using UnityEngine;

public class ShowTextOnCollision : MonoBehaviour
{
    public GameObject textParent; // The parent GameObject that contains the text and arrow
    public GameObject player;

    void Start()
    {
        // Hide the text and arrow initially
        textParent.SetActive(false);
    }

    // This method is called when another object enters the trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.gameObject == player)
        {
            // Show the text and arrow by activating the parent GameObject
            textParent.SetActive(true);
        }
    }
}
