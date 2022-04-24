using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartMenuControler : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadGame()
    {
        
    }

    public void HighScore()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}