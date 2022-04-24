using System;
using System.Collections.Generic;

[Serializable]
public class Level
{
    public Level()
    {
        Scores = new List<Score>();
    }

    public List<Score> Scores { get; set; }
}