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

    private void Start()
    {
    }

    private void Update()
    {
    }

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
        GameInfo.Player1 = player1InputField.text == ""
            ? player1InputField.placeholder.GetComponent<Text>().text
            : player1InputField.text;
        GameInfo.Player2 = player2InputField.text == ""
            ? player2InputField.placeholder.GetComponent<Text>().text
            : player2InputField.text;

        Saves.GameSaves[i].Level = 1;
        Saves.GameSaves[i].Player1 = GameInfo.Player1;
        Saves.GameSaves[i].Player2 = GameInfo.Player2;
        GameInfo.GameSaveSlot = i;
        Saves.WriteSave();
        SceneManager.LoadScene(GameInfo.LevelIndex);
    }

    private void LoadGame(int i)
    {
        if (Saves.GameSaves[i].Level <= 0) return;
        GameInfo.Player1 = Saves.GameSaves[i].Player1;
        GameInfo.Player2 = Saves.GameSaves[i].Player2;
        GameInfo.GameSaveSlot = i;
        SceneManager.LoadScene(Saves.GameSaves[i].Level - 1 + GameInfo.LevelIndex);
    }

    private void ShowPanel()
    {
        for (var i = 0; i < Mathf.Min(saveSlotsTexts.Length, Saves.GameSaves.Length); i++)
        {
            var textStr = "";
            if (Saves.GameSaves[i].Level > 0)
                textStr +=
                    $"Úroveò {Saves.GameSaves[i].Level}\n {Saves.GameSaves[i].Player1} a {Saves.GameSaves[i].Player2}";
            else
                textStr += "Prázdný slot";

            saveSlotsTexts[i].text = textStr;
        }


        headerText.text = isNewGame ? "Vybrat ukladací slot" : "Naèíst hru ze slotu";

        transform.position =
            new Vector3(transform.position.x, 1f, transform.position.z);
    }
}