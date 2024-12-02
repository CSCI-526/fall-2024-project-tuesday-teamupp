using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private PlayerController playerController;
    Send2Google send2Google;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        GameObject senderObject = GameObject.Find("Person");
        if (senderObject != null)
        {
            send2Google = senderObject.GetComponent<Send2Google>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        PowerUpPopUp popUp = FindObjectOfType<PowerUpPopUp>();

        if (other.CompareTag("Player") && !playerController.IsShieldActive())  
        {
            StartCoroutine(HideAndShow());
            playerController.SetShieldActive(true);
            if (popUp != null)
            {
                popUp.ShowPopUp("Shield Activated!");
            }
        }
    }

    IEnumerator HideAndShow()
    {
        ToggleShield(false);

        yield return new WaitForSeconds(15);

        ToggleShield(true);
    }

    void ToggleShield(bool isVisible)
    {
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = isVisible;
        }

        Collider2D[] colliders = gameObject.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = isVisible;
        }
    }

}
