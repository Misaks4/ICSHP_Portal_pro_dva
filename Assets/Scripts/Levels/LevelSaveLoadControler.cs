using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSaveLoadControler : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform player1;
    [SerializeField] private Transform player2;
    [SerializeField] private Transform portal1;
    [SerializeField] private Transform portal2;

    private void Awake()
    {
        if (Game.IsNewLevel)
        {
            SaveLevel();
            Game.IsNewLevel = false;
        }
        else
        {
            LoadLevel();
        }
    }

    public void SaveLevel()
    {
        Saves.GameSaves[Game.GameSaveSlot].Time = GetComponent<TimerControler>().Time;
        Saves.GameSaves[Game.GameSaveSlot].Camera = mainCamera.transform.position;

        Saves.GameSaves[Game.GameSaveSlot].LevelName =
            (SceneManager.GetActiveScene().buildIndex - Game.LevelIndex + 1) % 4;
        Saves.GameSaves[Game.GameSaveSlot].Player1Name = Game.Player1;
        Saves.GameSaves[Game.GameSaveSlot].Player2Name = Game.Player2;
        
        Saves.GameSaves[Game.GameSaveSlot].Player1.Position = player1.position;
        Saves.GameSaves[Game.GameSaveSlot].Player1.EulerAngles = player1.eulerAngles;
        Saves.GameSaves[Game.GameSaveSlot].Player1.LocalScale = player1.localScale;

        Saves.GameSaves[Game.GameSaveSlot].Player2.Position = player2.position;
        Saves.GameSaves[Game.GameSaveSlot].Player2.EulerAngles = player2.eulerAngles;
        Saves.GameSaves[Game.GameSaveSlot].Player2.LocalScale = player2.localScale;

        Saves.GameSaves[Game.GameSaveSlot].Portal1.Position = portal1.position;
        Saves.GameSaves[Game.GameSaveSlot].Portal1.EulerAngles = portal1.eulerAngles;
        Saves.GameSaves[Game.GameSaveSlot].Portal1.LocalScale = portal1.localScale;

        Saves.GameSaves[Game.GameSaveSlot].Portal2.Position = portal2.position;
        Saves.GameSaves[Game.GameSaveSlot].Portal2.EulerAngles = portal2.eulerAngles;
        Saves.GameSaves[Game.GameSaveSlot].Portal2.LocalScale = portal2.localScale;
        Saves.WriteSave();
    }

    public void LoadLevel()
    {
        GetComponent<TimerControler>().Time = Saves.GameSaves[Game.GameSaveSlot].Time;
        mainCamera.transform.position = Saves.GameSaves[Game.GameSaveSlot].Camera;

        player1.transform.position = Saves.GameSaves[Game.GameSaveSlot].Player1.Position;
        player1.transform.eulerAngles = Saves.GameSaves[Game.GameSaveSlot].Player1.EulerAngles;
        player1.transform.localScale = Saves.GameSaves[Game.GameSaveSlot].Player1.LocalScale;

        player2.transform.position = Saves.GameSaves[Game.GameSaveSlot].Player2.Position;
        player2.transform.eulerAngles = Saves.GameSaves[Game.GameSaveSlot].Player2.EulerAngles;
        player2.transform.localScale = Saves.GameSaves[Game.GameSaveSlot].Player2.LocalScale;

        portal1.transform.position = Saves.GameSaves[Game.GameSaveSlot].Portal1.Position;
        portal1.transform.eulerAngles = Saves.GameSaves[Game.GameSaveSlot].Portal1.EulerAngles;
        portal1.transform.localScale = Saves.GameSaves[Game.GameSaveSlot].Portal1.LocalScale;

        portal2.transform.position = Saves.GameSaves[Game.GameSaveSlot].Portal2.Position;
        portal2.transform.eulerAngles = Saves.GameSaves[Game.GameSaveSlot].Portal2.EulerAngles;
        portal2.transform.localScale = Saves.GameSaves[Game.GameSaveSlot].Portal2.LocalScale;
    }
}