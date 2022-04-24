using UnityEngine;
using UnityEngine.UI;

public class GameCameraControler : MonoBehaviour
{
    [SerializeField] private Text versionText;
    public void Awake()
    {
        versionText.text = $"{GameInfo.Version}-alpha";
        Saves.ReadSaves();
        HighScores.ReadScores();
    }
}