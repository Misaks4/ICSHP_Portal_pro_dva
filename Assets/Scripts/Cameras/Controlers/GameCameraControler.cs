using UnityEngine;
using UnityEngine.UI;

public class GameCameraControler : MonoBehaviour
{
    [SerializeField] private Text versionText;
    public void Awake()
    {
        versionText.text = $"V{GameInfo.Version}";
        Saves.ReadSaves();
        HighScores.ReadScores();
    }
}