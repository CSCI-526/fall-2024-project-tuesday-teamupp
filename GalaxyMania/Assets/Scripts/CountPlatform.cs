using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountPlatform : MonoBehaviour
{
    public static Dictionary<string, Dictionary<string, bool>> levelPlatforms = new Dictionary<string, Dictionary<string, bool>>();
    // Start is called before the first frame update
    void Start()
    {
        levelPlatforms["Level 1"] = new Dictionary<string, bool>();
        levelPlatforms["Level 2"] = new Dictionary<string, bool>();
        levelPlatforms["Level 3"] = new Dictionary<string, bool>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        string platformName = collision.gameObject.name;
        CheckCombinedPlatform(platformName, currentSceneName, "Floor");
        CheckCombinedPlatform(platformName, currentSceneName, "Stabilizer");
        CheckCombinedPlatform(platformName, currentSceneName, "PlusNormal");
        CheckSinglePlatform(platformName, currentSceneName, "Normal");
        CheckSinglePlatform(platformName, currentSceneName, "Anti");
    }

    void CheckCombinedPlatform(string platformName, string currentSceneName, string prefix)
    {
        if (platformName.StartsWith(prefix) && !levelPlatforms[currentSceneName].ContainsKey(prefix))
        {
            levelPlatforms[currentSceneName][prefix] = true;
            Debug.Log("Touch the " + prefix);
        }
    }

    void CheckSinglePlatform(string platformName, string currentSceneName, string prefix)
    {
        if (platformName.StartsWith(prefix) && !levelPlatforms[currentSceneName].ContainsKey(platformName))
        {
            levelPlatforms[currentSceneName][platformName] = true;
            Debug.Log("Touch the " + platformName);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
