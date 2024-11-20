using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player collected the star!");
            FindObjectOfType<StarsCounter>().AddStar();
            Destroy(gameObject);
        }
    }

}