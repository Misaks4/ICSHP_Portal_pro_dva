using UnityEngine;
using UnityEngine.UI;

public class GameCameraControler : MonoBehaviour
{
    [SerializeField] private Text versionText;
    public void Awake()
    {
        versionText.text = $"{Game.Version}-alpha";
        Saves.ReadSaves();
        HighScores.ReadScores();
    }
}