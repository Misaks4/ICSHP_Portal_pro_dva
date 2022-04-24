using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Saves
{
    private const int GameSavesCount = 3;
    private static bool isLoaded;
    public static Save[] GameSaves { get; private set; }

    public static void ReadSaves()
    {
        if (isLoaded) return;

        GameSaves = new Save[GameSavesCount];
        for (var i = 0; i < GameSavesCount; i++) GameSaves[i] = new Save();
        foreach (var save in GameSaves) save.Level = 0;
        GameInfo.CreateBinaryDirectory();
        for (var i = 0; i < GameSavesCount; i++) Deserialize(i);

        isLoaded = true;
    }

    public static void WriteSave()
    {
        Serialize();
    }

    private static void Serialize()
    {
        var dataStream = new FileStream($"{GameInfo.BinaryDirectory}/saveSlot{GameInfo.GameSaveSlot}.data",
            FileMode.Create);
        try
        {
            new BinaryFormatter().Serialize(dataStream, GameSaves[GameInfo.GameSaveSlot]);
        }
        catch (Exception)
        {
            // ignored
        }

        dataStream.Close();
    }

    private static void Deserialize(int i)
    {
        var dataStream = new FileStream($"{GameInfo.BinaryDirectory}/saveSlot{i}.data", FileMode.OpenOrCreate);
        try
        {
            GameSaves[i] = new BinaryFormatter().Deserialize(dataStream) as Save;
        }
        catch (Exception)
        {
            // ignored
        }

        dataStream.Close();
    }
}