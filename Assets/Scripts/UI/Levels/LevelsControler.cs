using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsControler : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}