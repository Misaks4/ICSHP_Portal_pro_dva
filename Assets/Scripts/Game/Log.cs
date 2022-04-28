using System;
using System.IO;

public static class Log
{
    public static void LogError(string methodName, Exception e)
    {
        WriteLog($"{methodName} {e.Message}");
    }

    private static void WriteLog(string text)
    {
        var writer = new StreamWriter($"{Game.BinaryDirectory}/log.txt", true);
        writer.WriteLine($"{DateTime.Now} {text}");
        writer.Close();
    }
}