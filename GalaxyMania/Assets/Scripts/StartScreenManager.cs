using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level Screen");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial Screen");
    }

}
