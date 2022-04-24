using UnityEngine;

public class LoadSaveControler : MonoBehaviour
{
    [SerializeField] private GameSavesPanelControler gameSavesControler;

    public void ShowPanel(bool isNewGame)
    {
        gameSavesControler.ShowPanel(isNewGame);
    }

    public void ClosePanel()
    {
        gameSavesControler.ClosePanel();
    }

    public void UseSlot(int i)
    {
        gameSavesControler.UseSlot(i);
    }
}