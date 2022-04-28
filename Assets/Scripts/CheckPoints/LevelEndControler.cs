using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndControler : MonoBehaviour
{
    private readonly List<Collider2D> players = new List<Collider2D>();
    [SerializeField] private TimerControler timerControler;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 7) return;
        collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        players.Add(collision);

        if (players.Count != 2) return;
        var score = new Score
        {
            Player1 = Game.Player1,
            Player2 = Game.Player2,
            Time = timerControler.Stop()
        };
        HighScores.AddScore(SceneManager.GetActiveScene().buildIndex - Game.LevelIndex,
            score);
        Invoke(nameof(LoadNextLevel), 2f);
    }

    private void LoadNextLevel()
    {
        Game.IsNewLevel = true;
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }
}