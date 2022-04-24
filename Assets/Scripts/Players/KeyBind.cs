using System;
using UnityEngine;

[Serializable]
public class KeyBind
{
    [SerializeField] private string horizontal;
    [SerializeField] private string vertical;
    [SerializeField] private string jump;
    [SerializeField] private string fire;

    public KeyBind(string horizontal, string vertical, string jump, string fire)
    {
        this.horizontal = horizontal;
        this.vertical = vertical;
        this.jump = jump;
        this.fire = fire;
    }

    public string Horizontal => horizontal;
    public string Vertical => vertical;
    public string Jump => jump;
    public string Fire => fire;
}