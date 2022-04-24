using System;

[Serializable]
public class Score
{
    public string Player1 { get; set; }
    public string Player2 { get; set; }
    public TimeSpan Time { get; set; }
}