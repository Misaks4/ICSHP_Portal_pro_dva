using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class HighScores
{
    private const int ScoresMaxCount = 5;
    private static bool isLoaded;
    public static List<Level> Levels { get; private set; }

    public static void ReadScores()
    {
        if (isLoaded) return;
        Levels = new List<Level>();
        for (var i = 0; i < 3; i++)
        {
            var tt = new Level();
            Levels.Add(tt);
        }

        Deserialize();
        isLoaded = true;
    }

    public static void AddScore(int i, Score score)
    {
        var j = 0;
        foreach (var levelScore in Levels[i].Scores)
        {
            if (levelScore.Time > score.Time) break;
            j++;
        }

        if (j >= ScoresMaxCount) return;
        Levels[i].Scores.Insert(j, score);
        Serialize();
    }


    private static void Serialize()
    {
        var dataStream = new FileStream($"{Game.BinaryDirectory}/{Game.HighScoreFile}",
            FileMode.Create);
        try
        {
            new BinaryFormatter().Serialize(dataStream, Levels);
        }
        catch (Exception e)
        {
            Log.LogError("HighScores.cs/Serialize()",e);
        }

        dataStream.Close();
    }

    private static void Deserialize()
    {
        var dataStream = new FileStream($"{Game.BinaryDirectory}/{Game.HighScoreFile}",
            FileMode.OpenOrCreate);
        try
        {
            Levels = new BinaryFormatter().Deserialize(dataStream) as List<Level>;
        }
        catch (Exception e)
        {
            Log.LogError("HighScores.cs/Deserialize()", e);
        }

        dataStream.Close();
    }
}