using System.Collections;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance; // Singleton instance for easy access

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartRespawnCoroutine(GameObject diamond, Vector3 respawnPosition, float respawnTime)
    {
        // Only start the coroutine if the GameObject is still valid
        if (diamond != null)
        {
            StartCoroutine(RespawnDiamond(diamond, respawnPosition, respawnTime));
        }
    }

    private IEnumerator RespawnDiamond(GameObject diamond, Vector3 respawnPosition, float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);

        if (diamond != null)
        {
            // Reactivate and reposition the diamond
            diamond.transform.position = respawnPosition;
            diamond.SetActive(true);
            Debug.Log("Diamond respawned!");
        }
    }
}
