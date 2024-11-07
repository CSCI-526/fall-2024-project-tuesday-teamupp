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
        levelPlatforms["Level 4"] = new Dictionary<string, bool>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        string platformName = collision.gameObject.name;
        CheckCombinedPlatform(platformName, currentSceneName, "Stabilizer");
        CheckSinglePlatform(platformName, currentSceneName, "Normal");
        CheckSinglePlatform(platformName, currentSceneName, "Anti");
        if (currentSceneName == "Level 1")
        {
            CheckCombinedPlatform(platformName, currentSceneName, "Floor");
            CheckCombinedPlatform(platformName, currentSceneName, "PlusNormal");
        }
        if (currentSceneName == "Level 2")
        {
            CheckCombinedPlatform(platformName, currentSceneName, "Floor");
            CheckCombinedPlatform(platformName, currentSceneName, "PlusNormalTop");
            CheckCombinedPlatform(platformName, currentSceneName, "PlusNormalBottom");
            CheckCombinedPlatform(platformName, currentSceneName, "PlusAnti");
            CheckSinglePlatform(platformName, currentSceneName, "Resizing");
            CheckSinglePlatform(platformName, currentSceneName, "Curve");
        }
        if (currentSceneName == "Level 3")
        {
            CheckCombinedPlatform(platformName, currentSceneName, "FloorL");
            CheckCombinedPlatform(platformName, currentSceneName, "FloorR");
            CheckCombinedPlatform(platformName, currentSceneName, "FloorMiddle");
            CheckCombinedPlatform(platformName, currentSceneName, "FlagNormal");
            CheckCombinedPlatform(platformName, currentSceneName, "Vmoving");
            CheckSinglePlatform(platformName, currentSceneName, "AutoJump");
            CheckSinglePlatform(platformName, currentSceneName, "NoJump");
        }
        if (currentSceneName == "Level 4")
        {
            CheckCombinedPlatform(platformName, currentSceneName, "FloorL");
            CheckCombinedPlatform(platformName, currentSceneName, "FloorR");
            CheckCombinedPlatform(platformName, currentSceneName, "FloorMiddle");
            CheckCombinedPlatform(platformName, currentSceneName, "FlagNormal");
            CheckCombinedPlatform(platformName, currentSceneName, "Vmoving");
            CheckSinglePlatform(platformName, currentSceneName, "AutoJump");
            CheckSinglePlatform(platformName, currentSceneName, "NoJump");
            CheckSinglePlatform(platformName, currentSceneName, "SafeWall");
        }
    }

    void CheckCombinedPlatform(string platformName, string currentSceneName, string prefix)
    {
        if (platformName.StartsWith(prefix) && !levelPlatforms[currentSceneName].ContainsKey(prefix))
        {
            levelPlatforms[currentSceneName][prefix] = true;
            Debug.Log(currentSceneName + " : Touch the " + prefix);
        }
    }

    void CheckSinglePlatform(string platformName, string currentSceneName, string prefix)
    {
        if (platformName.StartsWith(prefix) && !levelPlatforms[currentSceneName].ContainsKey(platformName))
        {
            levelPlatforms[currentSceneName][platformName] = true;
            Debug.Log(currentSceneName + " : Touch the " + platformName);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
