using System.Collections;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void StartRespawnCoroutine(GameObject diamond, Vector3 respawnPosition, float respawnTime)
    {
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
            diamond.transform.position = respawnPosition;
            diamond.SetActive(true);
            Debug.Log("Diamond respawned!");
        }
    }
}
