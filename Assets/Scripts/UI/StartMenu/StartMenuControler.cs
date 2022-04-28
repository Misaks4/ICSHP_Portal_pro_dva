using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuControler : MonoBehaviour
{
    public void HighScore()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}