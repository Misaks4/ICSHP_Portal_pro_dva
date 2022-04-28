using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUIControler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI savedText;
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveLevel()
    {
        GetComponentInParent<LevelSaveLoadControler>().SaveLevel();
        savedText.text = "Level uložen";
        Invoke(nameof(TextReset), 1f);
    }

    private void TextReset()
    {
        savedText.text = "";
    }
}