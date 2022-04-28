using System;

[Serializable]
public class Save
{
    public Save()
    {
        Player1 = new SerializableGameObject();
        Player2 = new SerializableGameObject();
        Portal1 = new SerializableGameObject();
        Portal2 = new SerializableGameObject();
    }

    public int LevelName { get; set; }
    public string Player1Name { get; set; }
    public string Player2Name { get; set; }
    public SerializableGameObject Player1 { get; set; }
    public SerializableGameObject Player2 { get; set; }
    public SerializableGameObject Portal1 { get; set; }
    public SerializableGameObject Portal2 { get; set; }
    public SerializableVector3 Camera { get; set; }
    public float Time { get; set; }
}