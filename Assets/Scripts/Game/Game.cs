using System;
using System.IO;

public static class Game
{
    public static string Version => "0.23";
    public static string HighScoreFile => "highScore";
    public static string BinaryDirectory => "bin";
    public static int LevelIndex => 2;
    public static int GameSaveSlot { get; set; }
    public static string Player1 { get; set; }
    public static string Player2 { get; set; }
    public static bool IsNewLevel { get; set; }

    public static void CreateBinaryDirectory()
    {
        if (Directory.Exists(BinaryDirectory)) return;
        try
        {
            Directory.CreateDirectory(BinaryDirectory);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}