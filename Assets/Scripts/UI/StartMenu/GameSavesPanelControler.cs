using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSavesPanelControler : MonoBehaviour
{
    [SerializeField] private Text headerText;
    [SerializeField] private InputField player1InputField;
    [SerializeField] private InputField player2InputField;
    [SerializeField] private Text[] saveSlotsTexts;
    private bool isNewGame;

    public void ShowPanel(bool isNewGame)
    {
        this.isNewGame = isNewGame;
        ShowPanel();
    }

    public void ClosePanel()
    {
        transform.position =
            new Vector3(transform.position.x, -10f, transform.position.z);
    }

    public void UseSlot(int i)
    {
        if (isNewGame)
            NewGame(i);
        else
            LoadGame(i);

    }

    private void NewGame(int i)
    {
        Game.IsNewLevel = true;
        Game.Player1 = player1InputField.text == ""
            ? player1InputField.placeholder.GetComponent<Text>().text
            : player1InputField.text;
        Game.Player2 = player2InputField.text == ""
            ? player2InputField.placeholder.GetComponent<Text>().text
            : player2InputField.text;

        Saves.GameSaves[i].LevelName = 1;
        Saves.GameSaves[i].Player1Name = Game.Player1;
        Saves.GameSaves[i].Player2Name = Game.Player2;
        Game.GameSaveSlot = i;
        SceneManager.LoadScene(Game.LevelIndex);
    }

    private void LoadGame(int i)
    {
        if (Saves.GameSaves[i].LevelName <= 0) return;
        Game.Player1 = Saves.GameSaves[i].Player1Name;
        Game.Player2 = Saves.GameSaves[i].Player2Name;
        Game.GameSaveSlot = i;
        SceneManager.LoadScene(Saves.GameSaves[i].LevelName - 1 + Game.LevelIndex);
    }

    private void ShowPanel()
    {
        for (var i = 0; i < Mathf.Min(saveSlotsTexts.Length, Saves.GameSaves.Length); i++)
        {
            var textStr = "";
            if (Saves.GameSaves[i].LevelName > 0)
                textStr +=
                    $"Úroveò {Saves.GameSaves[i].LevelName}\n {Saves.GameSaves[i].Player1Name} a {Saves.GameSaves[i].Player2Name}";
            else
                textStr += "Prázdný slot";

            saveSlotsTexts[i].text = textStr;
        }


        headerText.text = isNewGame ? "Vybrat ukladací slot" : "Naèíst hru ze slotu";

        transform.position =
            new Vector3(transform.position.x, 1f, transform.position.z);
    }
}