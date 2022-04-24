using UnityEngine;
using UnityEngine.UI;

public class HighScoreControler : MonoBehaviour
{
    [SerializeField] private Text text;

    private void Start()
    {
        var textStr = "";
        for (var i = 0; i < HighScores.Levels.Count; i++)
        {
            textStr += $"LEVEL {i + 1}\n";
            foreach (var score in HighScores.Levels[i].Scores)
            {
                textStr += $"{score.Player1} a {score.Player2}  .... {score.Time:g}\n";
            }
        }

        text.text = textStr;
    }
}