using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTutorialScreenManager : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level 3");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Level 4");
    }
    public void TutorialMove()
    {
        SceneManager.LoadScene("Tutorial Move");
    }
    public void TutorialJump()
    {
        SceneManager.LoadScene("Tutorial Jump");
    }
    public void TutorialPortal()
    {
        SceneManager.LoadScene("Tutorial Portal");
    }
    public void TutorialEnemy()
    {
        SceneManager.LoadScene("Tutorial Enemy");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Start Screen");
    }
}